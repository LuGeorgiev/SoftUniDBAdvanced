using Cars.Data;
using Cars.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cars.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new CarsDbContext())
            {
                ResetDatabase(db);
               
            }
        }

        private static void ResetDatabase(CarsDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

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
                new Car{Engine=engines[0],Make=makes[3],Doors=4,ProductionYear = new DateTime(1859,1,1) }
            };
        }
    }
}
