using JsonCarDealer.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonCarDealer
{
    class Serializer
    {
        private readonly CarDealerContext context;
        public Serializer(CarDealerContext context)
        {
            this.context = context;
        }

        internal string ExportOrderedCustomers()
        {          

            var orderedCustomers = context.Customers
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver == true)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    IsYoungDriver = x.IsYoungDriver,
                    Sales = new int[0]
                })
                .ToArray();

            var serializedToJson = JsonConvert.SerializeObject(orderedCustomers, Formatting.Indented);

            
            return serializedToJson;
        }

        internal string ExportCarsMadeBy(string make)
        {            
            var carsMakeby = context.Cars
                .Where(x => x.Make == make)
                .OrderBy(x => x.Model)
                .ThenBy(x => x.TravelledDistance)
                .Select(x => new
                {
                    Id=x.Id,
                    Make = x.Make,
                    Model=x.Model,
                    x.TravelledDistance
                })
                .ToArray();

            var result = JsonConvert.SerializeObject(carsMakeby, Formatting.Indented);
            return result;
        }

        internal string ExportLocalSupliers()
        {         

            var localSupliers = context.Suppliers
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToArray();

            var result = JsonConvert.SerializeObject(localSupliers, Formatting.Indented);
            return result;
        }

        internal string ExportCarsWithTheirParts()
        {            
            //var carsWithParts = context.Cars
            //    .Select(x => new
            //    {
            //        car = new
            //        {
            //            x.Make,
            //            x.Model,
            //            x.TravelledDistance,
            //            parts = x.PartCars
            //            .Select(z => new
            //            {
            //                Name= z.Part.Name,
            //                Price = z.Part.Price
            //            })
            //            .ToArray()
            //        }
            //    })
            //    .ToArray();

            var carsWithParts = context.Cars
                .Select(x => new
                {
                    car = new
                    {
                        x.Make,
                        x.Model,
                        x.TravelledDistance,                        
                    },
                    parts = x.PartCars
                        .Select(z => new
                        {
                            Name = z.Part.Name,
                            Price = z.Part.Price
                        })
                        .ToArray()
                })
                .ToArray();

            var result = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);
            return result;
        }

        internal string ExportTotalSaleByCustomer()
        {            
            var totalSalesByCustomer = context.Customers
                .Where(x => x.Sales.Count >= 1)
                .Select(x => new
                {
                    fullName = x.Name,
                    boughtCars = x.Sales.Count,
                    spentMoney = x.Sales
                        .Sum(c=>c.Car.PartCars
                            .Sum(p=>p.Part.Price))
                })
                .OrderByDescending(x=>x.spentMoney)
                .ThenByDescending(x=>x.boughtCars)
                .ToArray();


            var result =JsonConvert.SerializeObject(totalSalesByCustomer,Formatting.Indented);
            return result;
        }

        internal string ExportSalesWithDiscount()
        {            
            var salesWithDiscount = context.Sales
                .Select(c => new
                {
                    car = new
                    {
                        c.Car.Make,
                        c.Car.Model,
                        c.Car.TravelledDistance
                    },
                    customerName=c.Customer.Name,
                    c.Discount,
                    price = c.Car.PartCars.Sum(x=>x.Part.Price),
                    priceWithDiscount = c.Car.PartCars.Sum(x => x.Part.Price) - c.Car.PartCars.Sum(x => x.Part.Price)*decimal.Parse(c.Discount.ToString())
                })
                .ToArray();

            var result =JsonConvert.SerializeObject(salesWithDiscount, Formatting.Indented);
            return result;
        }
    }
}
