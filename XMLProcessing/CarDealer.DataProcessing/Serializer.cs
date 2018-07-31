using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.DataProcessing.Dtos.Export;

namespace CarDealer.DataProcessing
{
    public class Serializer
    {
        public static string ExportCarsWithDistance(CarDealerDbContext context, int minimumTraveledDistance)
        {
            var sb = new StringBuilder();
            var cars = context.Cars
                .Where(c => c.TravelledDistance >= minimumTraveledDistance)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Select(c => new P1CarDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToArray();

            var xmlNamspaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(P1CarDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, xmlNamspaces);

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ExportCarsMadeBy(CarDealerDbContext context, string make)
        {
            var sb = new StringBuilder();
            var cars = context.Cars
                .Where(c => c.Make == make)
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new P2CarDto
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToArray();

            var xmlNamspaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(P2CarDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, xmlNamspaces);

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ExportLocalSuppliers(CarDealerDbContext context)
        {
            var sb = new StringBuilder();

            var suppliers = context.Suppliers
                .Where(c => c.IsImporter == false)
                .OrderBy(s => s.Id)
                .ThenBy(s=>s.Name)
                .Select(s => new P3Supplier
                {
                    Id=s.Id,
                    Name=s.Name,
                    PartsCount=s.Parts.Count                    
                })
                .ToArray();

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var ser = new XmlSerializer(typeof(P3Supplier[]), new XmlRootAttribute("suppliers"));
            ser.Serialize(new StringWriter(sb), suppliers, xmlNamespaces);
            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ExportCarsAndParts(CarDealerDbContext context)
        {
            var sb = new StringBuilder();

            var carsWithParts = context.Cars
                .Select(c => new P4CarDto
                {
                    Make=c.Make,
                    Model=c.Model,
                    TravelledDistance=c.TravelledDistance,
                    Part = c.PartCars
                    .Select(p=> new P4PartDto
                    {
                        Name=p.Part.Name,
                        Price=p.Part.Price
                    })
                    .ToArray()                    
                })
                .ToArray();                

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var ser = new XmlSerializer(typeof(P4CarDto[]), new XmlRootAttribute("cars"));
            ser.Serialize(new StringWriter(sb), carsWithParts, xmlNamespaces);
            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ExportTotalSalesBycustomer(CarDealerDbContext context)
        {
            var sb = new StringBuilder();

            var customer = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new P5CustomerDto
                {
                    Name=c.Name,
                    BoughtCars=c.Sales.Count,
                    SpentMoney=c.Sales.Sum(x=>x.Car
                        .PartCars
                        .Sum(z=>z.Part.Price))
                })
                .OrderByDescending(x=>x.SpentMoney)
                .ThenByDescending(x=>x.BoughtCars)                
                .ToArray();
                

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var ser = new XmlSerializer(typeof(P5CustomerDto[]), new XmlRootAttribute("customers"));
            ser.Serialize(new StringWriter(sb), customer, xmlNamespaces);
            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ExportSalesWithDiscount(CarDealerDbContext context)
        {
            var sb = new StringBuilder();

            var sales = new P6Sale
            {
                Cars=context.Sales
                .Select(c=>new P6Car
                {
                    Make=c.Car.Make,
                    Model=c.Car.Model,
                    TravelledDistance=c.Car.TravelledDistance,
                    CustomerName=c.Customer.Name,
                    Discount=c.Discount.ToString("F2"),
                    Price=c.Car.PartCars.Sum(p=>p.Part.Price),
                    DiscountedPrice = c.Car.PartCars.Sum(p => p.Part.Price)
                })
                .ToArray()
            };

            foreach (var car in sales.Cars)
            {
                decimal discount = 1-decimal.Parse(car.Discount);
                car.DiscountedPrice *= discount;
            }

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var ser = new XmlSerializer(typeof(P6Sale), new XmlRootAttribute("sales"));
            ser.Serialize(new StringWriter(sb), sales, xmlNamespaces);
            var result = sb.ToString().TrimEnd();

            return result;
        }
    }
}
