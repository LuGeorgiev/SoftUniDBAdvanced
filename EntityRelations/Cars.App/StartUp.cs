using Cars.Data;
using Cars.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Cars.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new CarsDbContext())
            {
                //ResetDatabase(db);

                var plates = db.LicensePlates.ToArray();
                var cars = db.Cars
                    .Include(c=>c.Engine)
                    .Include(c=>c.Make)
                    .Include(c=>c.LicensePlate)
                    .Include(c => c.CarDealerships)
                    .ThenInclude(cd => cd.Dealership)
                    .OrderBy(c=>c.ProductionYear)
                    .ToArray();

                cars[1].LicensePlate = plates.Single(p => p.LicensePlateId == 3);
                db.SaveChanges();

                foreach (var car in cars)
                {
                    Console.WriteLine($"{car.Make.Name}");
                    foreach (var carDealer in car.CarDealerships)
                    {
                        var dealership = carDealer.Dealership;
                        Console.WriteLine($"--{dealership.Name}");
                    }
                    Console.WriteLine($"---Fuel { car.Engine.FuelType}");
                    Console.WriteLine($"---Transmission: { car.Transmission}");

                    var licensePlateNum = car.LicensePlate != null ? car.LicensePlate.Name : "No Plate";
                    Console.WriteLine($"-----LicensePlate: {licensePlateNum}");
                    Console.WriteLine("-------------------");
                }

                Console.WriteLine();
            }
        }

        private static void ResetDatabase(CarsDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            Seed(context);
        }

        private static void Seed(CarsDbContext context)
        {
            var makes = new[]
            {
                new Make{Name="Ford" },
                new Make{Name="Merscedes" },
                new Make{Name="Audi" },
                new Make{Name="Лада" },
                new Make{Name="Трабант" },
                new Make{Name="АЗЛК" },
            };

            var engines = new[]
            {
                new Engine{Capacity=1.2, FuelType=FuelType.Diesel,Horsepower =95 },
                new Engine{Capacity=2.2, FuelType=FuelType.LPG,Horsepower =195 },
                new Engine{Capacity=3.2, FuelType=FuelType.Petrol,Horsepower =295 },
                new Engine{Capacity=4.2, FuelType=FuelType.Electric,Horsepower =395 },
                new Engine{Capacity=5.2, FuelType=FuelType.Petrol,Horsepower =495 },
            };

            var cars = new[] 
            {
                new Car{Engine=engines[0],Make=makes[3],Doors=4,ProductionYear = new DateTime(1959,1,14),Transmission=Transmission.Manual },
                new Car{Engine=engines[1],Make=makes[0],Doors=3,ProductionYear = new DateTime(1959,11,1),Transmission=Transmission.Automatic },
                new Car{Engine=engines[2],Make=makes[2],Doors=2,ProductionYear = new DateTime(1978,12,1),Transmission=Transmission.Manual },
                new Car{Engine=engines[3],Make=makes[1],Doors=5,ProductionYear = new DateTime(1999,1,4),Transmission=Transmission.Automatic },
                new Car{Engine=engines[4],Make=makes[4],Doors=6,ProductionYear = new DateTime(2005,3,1),Transmission=Transmission.Manual },
            };
            context.Cars.AddRange(cars);

            var dealerships = new[] 
            {
                new Dealership{Name="SoftUni-Auto" },
                new Dealership{Name="FastandFurious-Auto" },
                new Dealership{Name="Kostenurka-Auto" },
            };
            context.Dealerships.AddRange(dealerships);

            var carDealerships = new[]
            {
                new CarDealership{Car =cars[0],Dealership= dealerships[0]},
                new CarDealership{Car =cars[2],Dealership= dealerships[1]},
                new CarDealership{Car =cars[0],Dealership= dealerships[2]},
                new CarDealership{Car =cars[1],Dealership= dealerships[0]},
                new CarDealership{Car =cars[2],Dealership= dealerships[2]},
            };
            context.CarDealerships.AddRange(carDealerships);

            var licensePlate = new[]
            {
                new LicensePlate {Name="CB5667AA" },
                new LicensePlate {Name="PK5667AA" },
                new LicensePlate {Name="A5667AA" },
                new LicensePlate {Name="CA5667AA" },
            };
            context.LicensePlates.AddRange(licensePlate);

            context.SaveChanges();
        }
    }
}
