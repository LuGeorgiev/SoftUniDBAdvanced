using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Stations.Data;
using Stations.DataProcessor.Dto;
using Stations.Models.Enumerations;

namespace Stations.DataProcessor
{
	public class Serializer
	{
        public static IFormatProvider CulturalInfo { get; private set; }

        public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
		{
            //Vladi approach
            var date = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", null);

            var delayedTrains = context.Trains
                .Where(x => x.Trips.Any(tr=>tr.Status == TripStatus.Delayed && tr.DepartureTime <= date))
                .Select(t => new
                {
                    t.TrainNumber,
                    DelayedTrips = t.Trips.Where(tr=>tr.Status==TripStatus.Delayed&& tr.DepartureTime <= date)
                    .ToArray()
                })
                .Select(t=>new TrainDtoExport
                {
                    TrainNumber=t.TrainNumber,
                    DelayedTimes = t.DelayedTrips.Count(),
                    MaxDelayTime = t.DelayedTrips.Max(tr=>tr.TimeDifference).ToString()
                })
                .OrderByDescending(t=>t.DelayedTimes)
                .ThenByDescending(t=>t.MaxDelayTime)
                .ThenBy(t=>t.TrainNumber)
                .ToArray();
            string result = JsonConvert.SerializeObject(delayedTrains, Formatting.Indented);

            //var date = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", null);

            //var delayedTrains = context.Trips
            //    .Where(x => x.Status == TripStatus.Delayed && x.DepartureTime <= date)
            //    .Select(x => new
            //    {
            //        TrainNumber = x.Train.TrainNumber,
            //        DelayedTime = x.TimeDifference
            //    })
            //    .ToList();

            //var delayedDtos = new List<DelayedTrainsDto>();
            //foreach (var train in delayedTrains)
            //{
            //    var currentTrain = delayedDtos
            //        .FirstOrDefault(x => x.TrainNumber == train.TrainNumber);
            //    if (currentTrain==null)
            //    {
            //        delayedDtos.Add(new DelayedTrainsDto
            //        {
            //            TrainNumber = train.TrainNumber,
            //            DelayedTimes = 1,
            //            MaxDelayedTime=train.DelayedTime.Value
            //        });
            //    }
            //    else
            //    {
            //        currentTrain.DelayedTimes++;
            //        if (currentTrain.MaxDelayedTime<train.DelayedTime.Value)
            //        {
            //            currentTrain.MaxDelayedTime = train.DelayedTime.Value;
            //        }
            //    }
            //}

            //var orderedDelayedTrains = delayedDtos
            //    .OrderByDescending(x => x.MaxDelayedTime)
            //    .ThenByDescending(x => x.MaxDelayedTime)
            //    .ThenBy(x => x.TrainNumber)
            //    .Select(x => new
            //    {
            //        x.TrainNumber,
            //        x.DelayedTimes,
            //        MaxDelayedTime = x.MaxDelayedTime
            //    });


            //string result = JsonConvert.SerializeObject(orderedDelayedTrains, Formatting.Indented);
            return result;
		}

		public static string ExportCardsTicket(StationsDbContext context, string cardType)
		{
            var sb = new StringBuilder();

            var cards = context.CustomerCards
                .Where(x => x.Type.ToString() == cardType && x.BoughtTickets.Count>0)
                .OrderBy(x => x.Name)
                .Select(x => new CardDtoExport
                {
                    Name = x.Name,
                    Type = x.Type.ToString(),
                    Tickets = x.BoughtTickets
                        .Select(t=>new TicetDtoExport
                        {
                            OriginStation = t.Trip.OriginStation.Name,
                            DestinationStation = t.Trip.OriginStation.Name,
                            DepartureTime = t.Trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                        })
                        .ToArray()
                })
                .OrderBy(c=>c.Name)
                .ToArray();


            var serializer = new XmlSerializer(typeof(CardDtoExport), new XmlRootAttribute("Cards"));
            serializer.Serialize(new StringWriter(sb), cards, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty}));

            var result = sb.ToString().TrimEnd();         

            return result;
		}
	}
}