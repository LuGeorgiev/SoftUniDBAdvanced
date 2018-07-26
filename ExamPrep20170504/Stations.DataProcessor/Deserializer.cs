using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using Stations.Data;
using Stations.DataProcessor.Dto;
using Stations.Models;
using System.ComponentModel.DataAnnotations;
using Stations.Models.Enumerations;
using System.Xml.Linq;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;

namespace Stations.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportStations(StationsDbContext context, string jsonString)
		{
            var sb = new StringBuilder();
            var stations = JsonConvert.DeserializeObject<StationDto[]>(jsonString);
            var stationsToAdd = new List<Station>();

            foreach (var stationDto in stations)
            {
                if (stationDto.Town==null&&stationDto.Name!=null)
                {
                    stationDto.Town = stationDto.Name;
                }

                if (stationDto.Name == null||stationDto.Name.Length>50||stationDto.Town.Length>50
                    ||stationsToAdd.Any(s=>s.Name==stationDto.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var stationToAdd = Mapper.Map<Station>(stationDto);
                stationsToAdd.Add(stationToAdd);
                sb.AppendLine(string.Format(SuccessMessage, stationDto.Name));
            }
            context.Stations.AddRange(stationsToAdd);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

		public static string ImportClasses(StationsDbContext context, string jsonString)
		{
            var sb = new StringBuilder();
            var classesDto = JsonConvert.DeserializeObject<ClassesDto[]>(jsonString);
            var classes = new List<SeatingClass>();
            foreach (var dto in classesDto)
            {
                if (dto.Abbreviation==null||dto.Name==null
                    || dto.Name.Length>30|| dto.Abbreviation.Length!=2)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (classes.Any(x=>x.Name==dto.Name
                || x.Abbreviation == dto.Abbreviation))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var classToAdd = Mapper.Map<SeatingClass>(dto);
                classes.Add(classToAdd);
                sb.AppendLine(string.Format( SuccessMessage, dto.Name));
            }

            context.SeatingClasses.AddRange(classes);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

		public static string ImportTrains(StationsDbContext context, string jsonString)
		{
            var trainsDto = JsonConvert.DeserializeObject<TrainDto[]>(jsonString, new JsonSerializerSettings
            {
                NullValueHandling=NullValueHandling.Ignore
            });
            var validTrains = new List<Train>();
            var sb = new StringBuilder();

            foreach (var trainDto in trainsDto)
            {
                if (!IsValid(trainDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var trainAlreadyExist = validTrains.Any(t => t.TrainNumber == trainDto.TrainNumber);
                if (trainAlreadyExist)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var seatsAreValid = trainDto.Seats.All(IsValid);
                if (!seatsAreValid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var seatingClassesAreValid = trainDto.Seats.All(s => context.SeatingClasses.Any(sc => sc.Name == s.Name
                                  && sc.Abbreviation == s.Abbreviation));
                if (!seatingClassesAreValid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var trainToAdd = Mapper.Map<Train>(trainDto);
                
                var seatsToAdd = trainDto.Seats.Select(s=>new TrainSeat
                {
                    SeatingClass=context.SeatingClasses.SingleOrDefault(sc=>sc.Name==s.Name && sc.Abbreviation==s.Abbreviation),
                    Quantity=s.Quantity.Value
                })
                .ToArray();

                trainToAdd.TrainSeats = seatsToAdd;
                validTrains.Add(trainToAdd);
                sb.AppendLine(string.Format(SuccessMessage,trainDto.TrainNumber));
            }
            context.Trains.AddRange(validTrains);
            context.SaveChanges();
            
            return sb.ToString().TrimEnd();
		}

		public static string ImportTrips(StationsDbContext context, string jsonString)
		{
            var deserializedTripsDto = JsonConvert.DeserializeObject<TripDto[]>(jsonString, new JsonSerializerSettings
            {
                NullValueHandling=NullValueHandling.Ignore
            });
            var validTrips = new List<Trip>();
            var sb = new StringBuilder();

            foreach (var tripDto in deserializedTripsDto)
            {
                if (!IsValid(tripDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var tripTrain = context.Trains
                    .SingleOrDefault(x => x.TrainNumber == tripDto.Train);
                var orignStation = context.Stations
                    .SingleOrDefault(x => x.Name == tripDto.OriginStation);
                var destinationStation = context.Stations
                    .SingleOrDefault(x => x.Name == tripDto.DestinationStation);
                if (tripTrain==null||orignStation==null||destinationStation==null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var departureTime = DateTime.ParseExact(tripDto.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var arrivalTime = DateTime.ParseExact(tripDto.ArrivalTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                if (departureTime>arrivalTime)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                
                bool isValidDifference=TimeSpan.TryParseExact(tripDto.TimeDifference, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan timeDiff);

                var status = Enum.Parse<TripStatus>(tripDto.Status);
                var tripToAdd = new Trip
                {
                    Train = tripTrain,
                    OriginStation = orignStation,
                    DestinationStation = destinationStation,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    TimeDifference = timeDiff,
                    Status = status                    
                };

                validTrips.Add(tripToAdd);
                sb.AppendLine($"Trip from {tripDto.OriginStation} to {tripDto.DestinationStation} imported.");
            }
            context.Trips.AddRange(validTrips);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

		public static string ImportCards(StationsDbContext context, string xmlString)
		{
            //Vladi's may
            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            var deserializedCards =(CardDto[]) serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetByteCount(xmlString)));

            var validCards = new List<CustomerCard>();
            foreach (var cardDto in deserializedCards)
            {
                if (!IsValid(cardDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var cardType = Enum.TryParse<CardType>(cardDto.Type, out var card)?card:CardType.Normal;

                var customerCard = new CustomerCard
                {
                    Name=cardDto.Name,
                    Type=cardType,
                    Age=cardDto.Age
                };
                validCards.Add(customerCard);
                sb.AppendLine(string.Format(SuccessMessage, cardDto.Name));
            }

            context.CustomerCards.AddRange(validCards);
            context.SaveChanges();

            return sb.ToString().TrimEnd();


            //My way to solve the task
            //var xmlDoc = XDocument.Parse(xmlString);
            //var elements = xmlDoc.Root.Elements();
            //var validCards = new List<CustomerCard>();
            //var sb = new StringBuilder();

            //foreach (var element in elements)
            //{
            //    string name = element.Element("Name").Value;
            //    int age = int.Parse(element.Element("Age").Value);
            //    string cardTypeStr = element.Element("CardType")?.Value;

            //    bool invalidName = name.Length > 128;
            //    bool invalidAge = age < 0 || age > 120;
            //    if (invalidAge || invalidName)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    CardType cardType;
            //    if (cardTypeStr == null)
            //    {
            //        cardType = CardType.Normal;
            //    }
            //    else
            //    {
            //        cardType = (CardType)Enum.Parse(typeof(CardType), cardTypeStr, true);
            //    }

            //    var validCard = new CustomerCard
            //    {
            //        Age = age,
            //        Name = name,
            //        Type = cardType
            //    };

            //    validCards.Add(validCard);
            //    sb.AppendLine(string.Format(SuccessMessage, name));
            //}
            //context.CustomerCards.AddRange(validCards);
            //context.SaveChanges();

            //return sb.ToString().TrimEnd();
        }

		public static string ImportTickets(StationsDbContext context, string xmlString)
		{
            //Vladi:
            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(TicketDto[]), new XmlRootAttribute("Tickets"));
            var deserializedTickets = (TicketDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetByteCount(xmlString)));

            var validTickets = new List<Ticket>();
            foreach (var ticketDto in deserializedTickets)
            {
                if (!IsValid(ticketDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var departureTime = DateTime.ParseExact(ticketDto.Trip.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var trip = context.Trips
                    .SingleOrDefault(t => t.DestinationStation.Name == ticketDto.Trip.DestinationStation
                                       &&t.OriginStation.Name==ticketDto.Trip.OriginStation
                                       &&t.DepartureTime==departureTime);
                if (trip==null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                CustomerCard card=null;
                if (ticketDto.Card!=null)
                {
                    card = context.CustomerCards
                        .SingleOrDefault(x => x.Name == ticketDto.Card.Name);
                    if (card==null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }
                }

                var seatinClassAbreviatio = ticketDto.Seat.Substring(0, 2);
                var quantity = int.Parse(ticketDto.Seat.Substring(2));

                var seatEsists = context.TrainSeats
                    .Any(s => s.SeatingClass.Abbreviation == seatinClassAbreviatio
                    && s.Quantity >= quantity);
                if (!seatEsists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var seat = ticketDto.Seat;

                var ticket = new Ticket
                {
                    Trip=trip,
                    CustomerCard=card,
                    Price=ticketDto.Price,
                    SeatingPlace=seat
                };
                validTickets.Add(ticket);
                sb.AppendLine($"Trip from{trip.OriginStation.Name} to {trip.DestinationStation.Name} departing on {trip.DepartureTime.ToString("dd/MM/yyyy")}");
            }
            context.Tickets.AddRange(validTickets);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

            //Mine approach
            //var xmlDoc = XDocument.Parse(xmlString);
            //var sb = new StringBuilder();
            //var elements = xmlDoc.Root.Elements();
            //var validTickets = new List<Ticket>();

            //foreach (var elem in elements)
            //{
            //    var price = decimal.Parse(elem.Attribute("price").Value); //not null
            //    var seat = elem.Attribute("seat").Value; //not null
            //    var originStationName = elem.Element("Trip").Element("OriginStation").Value;
            //    var destinationStationName = elem.Element("Trip").Element("DestinationStation").Value;
            //    var departureTime = DateTime.ParseExact( elem.Element("Trip").Element("DepartureTime").Value, "dd/MM/yyyy HH:mm",null);
            //    var cardName = elem.Element("Card")?.Attribute("Name").Value;

            //    if (cardName==null)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    var trip = context.Trips
            //        .FirstOrDefault(x => x.OriginStation.Name == originStationName
            //                && x.DestinationStation.Name == destinationStationName
            //                && x.DepartureTime == departureTime);
            //    var customerCard = context.CustomerCards
            //        .FirstOrDefault(x => x.Name == cardName);
            //    if (trip==null||customerCard==null)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    var classAbreviation = seat.Substring(0, 2);
            //    var seatNumber = int.Parse(seat.Substring(2));
            //    var isSeatValid = seatNumber >= 0 && trip.Train.TrainSeats.Any(x => x.SeatingClass.Abbreviation == classAbreviation && seatNumber <= x.Quantity);

            //    if (!isSeatValid)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    var ticket = new Ticket
            //    {
            //        CustomerCard=customerCard,
            //        Price=price,
            //        Trip=trip,
            //        SeatingPlace=seat
            //    };

            //    validTickets.Add(ticket);
            //    sb.AppendLine($"Ticket from {originStationName} to {destinationStationName} departing at {elem.Element("Trip").Element("DepartureTime").Value} imported.");
            //}
            //context.Tickets.AddRange(validTickets);
            //context.SaveChanges();

            //return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid=Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
	}
}