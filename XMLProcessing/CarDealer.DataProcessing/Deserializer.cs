using AutoMapper;
using CarDealer.Data;
using CarDealer.DataProcessing.Dtos.Import;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataProcessing
{
    public  class Deserializer
    {
        public static void ImportSuppliers(CarDealerDbContext context, string xmlSuplyers)
        {
            var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));
            var deserializedSupliers = (SupplierDto[])serializer.Deserialize(new StringReader(xmlSuplyers));
            var validSuppliers = new List<Supplier>();
            foreach (var dto in deserializedSupliers)
            {
                if (!isValid(dto))
                {
                    continue;
                }

                var suplier = Mapper.Map<Supplier>(dto);
                validSuppliers.Add(suplier);
            }
            context.Suppliers.AddRange(validSuppliers);
            context.SaveChanges();
        }


        public static void ImportParts(CarDealerDbContext context, string xmlParts)
        {
            var serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));
            var deserializedParts = (PartDto[])serializer.Deserialize(new StringReader(xmlParts));
            var validParts = new List<Part>();
            var rnd = new Random();

            foreach (var dto in deserializedParts)
            {
                if (!isValid(dto))
                {
                    continue;
                }

                var part = Mapper.Map<Part>(dto);
                var supplierId = rnd.Next(1, 32);
                part.SupplierId = supplierId;
                validParts.Add(part);
            }
            context.Parts.AddRange(validParts);
            context.SaveChanges();
        }


        public static void ImportCars(CarDealerDbContext context, string xmlCars)
        {
            var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));
            var deserializedCars = (CarDto[])serializer.Deserialize(new StringReader(xmlCars));
            var validCars = new List<Car>();
            var rnd = new Random();

            foreach (var dto in deserializedCars)
            {
                bool isValidInt = int.TryParse(dto.TravelledDistance, out int travelled);

                if (!isValid(dto)|| !isValidInt)
                {
                    continue;
                }

                var car = Mapper.Map<Car>(dto);
                var partsCount = rnd.Next(10, 21);
                for (int i = 0; i <= partsCount; i++)
                {
                    var partId = rnd.Next(1, 132);                    
                    var partCar = new PartCar
                    {
                        Car=car,
                        PartId = partId
                    };

                    if (car.PartCars.Any(x=>x.PartId==partId))
                    {
                        continue;
                    }

                    car.PartCars.Add(partCar);
                }
                
                validCars.Add(car);
            }
            context.Cars.AddRange(validCars);
            context.SaveChanges();
        }

        public static void ImportCustomers(CarDealerDbContext context, string xmlCustomer)
        {
            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));
            var deserializedParts = (CustomerDto[])serializer.Deserialize(new StringReader(xmlCustomer));
            var validCustomers = new List<Customer>();            

            foreach (var dto in deserializedParts)
            {
                if (!isValid(dto))
                {
                    continue;
                }

                var customer = Mapper.Map<Customer>(dto);
                validCustomers.Add(customer);
                
            }
            context.Customers.AddRange(validCustomers);
            context.SaveChanges();
        }

        public static void ImportRandomSales(CarDealerDbContext context)
        {
            var discounts = new float[]{ 0, 0.05f,0.1f, 0.15f,0.2f, 0.3f, 0.4f, 0.5f  };
            var validSales = new List<Sale>();
            var rnd = new Random();
            var numberOfSales = rnd.Next(100, 242);

            var customers = context.Customers.ToArray();
            var cars = context.Cars.ToArray();

            for (int i = 0; i <= numberOfSales; i++)
            {
                var discount = discounts[rnd.Next(0, 8)];
                var carId = rnd.Next(1, 243);
                var customerId = rnd.Next(1, 31);

                if (validSales.Any(x=>x.CarId==carId))
                {
                    continue;
                }

                var isYoungDriver = customers
                    .FirstOrDefault(x => x.Id == customerId)
                    .IsYoungDriver == true;
                if (isYoungDriver)
                {
                    discount += 0.05f;
                }

                var sale = new Sale
                {
                    Discount=discount,
                    CustomerId=customerId,
                    CarId=carId
                };
                validSales.Add(sale);
            }

             context.Sales.AddRange(validSales);
            context.SaveChanges();
        }

        private static bool isValid(object obj)
        {
            var validContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validContext,validResult,true);

            return isValid;
        }
    }
}
