using BusTicketsSystem.Data;
using BusTicketsSystem.Models;
using BusTicketsSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Client.Utility
{
    class InitialDbSeed
    {
        public void Seed(BusTicketsSystemContext db)
        {
            var towns = new List<Town>()
            {
                    new Town {Name="Sofia",Country="Bulgaria" },
                    new Town {Name="Varna",Country="Bulgaria" },
                    new Town {Name="Burgas",Country="Bulgaria" },
                    new Town {Name="Plovdiv",Country="Bulgaria" },
                    new Town {Name="Vraca",Country="Bulgaria" },
                    new Town {Name="Pernik",Country="Bulgaria" },
                    new Town {Name="Bansko",Country="Bulgaria" } };
            foreach (var town in towns)
            {
                db.Towns.Add(town);
            }
            db.SaveChanges();

            var busStations = new List<BusStation>()
            {
                new BusStation {Name="Central Station",TownId=1},
                new BusStation {Name="Only Station",TownId=2},
                new BusStation {Name="North Station",TownId=1},
                new BusStation {Name="No Station At All",TownId=3},
                new BusStation {Name="South Station",TownId=1}
            };
            foreach (var station in busStations)
            {
                db.BusStations.Add(station);
            }
            db.SaveChanges();

            var accounts = new List<BankAccount>()
            {
                    new BankAccount {AccountNumber ="3456456854",Balance=5000 },
                    new BankAccount {AccountNumber ="568926232",Balance=200 },
                    new BankAccount {AccountNumber ="4621122324" },
                    new BankAccount {AccountNumber ="659532413",Balance=2000 },
                    new BankAccount {AccountNumber ="235643232",Balance=160 }                    
            };
            foreach (var account in accounts)
            {
                db.BankAccounts.Add(account);
            }
            db.SaveChanges();

            var customers = new List<Customer>()
            {
                new Customer {BankAccountId=1,FirstName="Ivan",LastName="Ivanov", Gender="Male",HomeTownId=2},
                new Customer {BankAccountId=2,FirstName="Petkan",LastName="Petkov", Gender="Male",HomeTownId=2},
                new Customer {BankAccountId=3,FirstName="Martin",LastName="Martinov", Gender="Male",HomeTownId=1},
                new Customer {BankAccountId=4,FirstName="Ivana",LastName="Ivanova", Gender="Female",HomeTownId=3},
                new Customer {BankAccountId=5,FirstName="Marina",LastName="Marinova", Gender="Female",HomeTownId=4}
            };
            foreach (var customer in customers)
            {
                db.Customers.Add(customer);
            }
            db.SaveChanges();

            var companies = new List<BusCompany>()
            {
                    new BusCompany { Name = "FineTravels" },
                    new BusCompany { Name = "Don'tLookBack" },
                    new BusCompany { Name = "FlyAway" },
                    new BusCompany { Name = "FastAndFurious" },
                    new BusCompany { Name = "LateButSave" }
            };
            foreach (var company in companies)
            {
                db.BusCompanies.Add(company);
            }
            db.SaveChanges();

            var trips = new List<Trip>()
            {
                    new Trip{BusCompanyId=5,DestinationStationId=2,OriginStationId=4,ArrivalTime="13:56",DepertureTime="22:16",Status=TripStatus.Arrived},
                    new Trip{BusCompanyId=4,DestinationStationId=5,OriginStationId=2,ArrivalTime="3:56",DepertureTime="13:16",Status=TripStatus.Cancelled },
                    new Trip{BusCompanyId=3,DestinationStationId=2,OriginStationId=5,ArrivalTime="13:56",DepertureTime="22:16",Status=TripStatus.Delayed },
                    new Trip{BusCompanyId=2,DestinationStationId=3,OriginStationId=5,ArrivalTime="11:56",DepertureTime="15:06" ,Status=TripStatus.Departed},
                    new Trip{BusCompanyId=1,DestinationStationId=2,OriginStationId=5,ArrivalTime="12:56",DepertureTime="20:16",Status=TripStatus.Cancelled },
                    new Trip{BusCompanyId=3,DestinationStationId=4,OriginStationId=2,ArrivalTime="6:56",DepertureTime="23:16",Status=TripStatus.Delayed },
                    new Trip{BusCompanyId=2,DestinationStationId=2,OriginStationId=1,ArrivalTime="13:56",DepertureTime="12:16",Status=TripStatus.Arrived },
                    new Trip{BusCompanyId=4,DestinationStationId=5,OriginStationId=3,ArrivalTime="15:06",DepertureTime="2:16",Status=TripStatus.Departed }
            };
            foreach (var trip in trips)
            {
                db.Trips.Add(trip);
            }
            db.SaveChanges();

            var tickets = new List<Ticket>()
            {
                    new Ticket {TripId=1,CustomerId=2,Price=98.76m,Seat=2 },
                    new Ticket {TripId=2,CustomerId=1,Price=34,Seat=1 },
                    new Ticket {TripId=3,CustomerId=3,Price=9.76m,Seat=3 },
                    new Ticket {TripId=4,CustomerId=2,Price=89.76m,Seat=1 },
                    new Ticket {TripId=1,CustomerId=1,Price=58.76m,Seat=1 },
                    new Ticket {TripId=2,CustomerId=5,Price=9.76m,Seat=1 },
                    new Ticket {TripId=1,CustomerId=4,Price=34.76m,Seat=2 },
                    new Ticket {TripId=3,CustomerId=2,Price=23.76m,Seat=4 },
                    new Ticket {TripId=4,CustomerId=3,Price=16.76m,Seat=1 }
            };
            foreach (var ticket in tickets)
            {
                db.Tickets.Add(ticket);
            }
            db.SaveChanges();

            var reviews = new List<Review>()
            {
                    new Review { CustomerId=1,Grade=2.3f,BusCompanyId=2, Content= "Auful trip" },
                    new Review { CustomerId=2,Grade=6.3f,BusCompanyId=2, Content= "Good trip" },
                    new Review { CustomerId=1,Grade=8.3f,BusCompanyId=2, Content= "GREATTTTT trip" },
                    new Review { CustomerId=3,Grade=2.9f,BusCompanyId=1, Content= "blah blah" },
                    new Review { CustomerId=4,Grade=9.3f,BusCompanyId=1, Content= "Nice trip" },
                    new Review { CustomerId=1,Grade=8.3f,BusCompanyId=2, Content= "Perfect trip" },
                    new Review { CustomerId=5,Grade=6.3f,BusCompanyId=4, Content= "Last trip" },
                    new Review { CustomerId=1,Grade=9.3f,BusCompanyId=2, Content= "I reccomend this trip" },
                    new Review { CustomerId=2,Grade=5.3f,BusCompanyId=3, Content= "It was OK" }
            };
            foreach (var review in reviews)
            {
                db.Reviews.Add(review);
            }
            db.SaveChanges();
        }
    }
}
