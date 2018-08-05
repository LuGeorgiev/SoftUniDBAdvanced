using JsonCarDealer.Data;
using JsonCarDealer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace JsonCarDealer
{
    class Deserializer
    {
        private readonly CarDealerContext context;
        
        public Deserializer(CarDealerContext context)
        {
            this.context = context;
        }

        internal void ImportCustomers(string jsonString)
        {
            var desirializedCustomers = JsonConvert.DeserializeObject<Customer[]>(jsonString);
            var validCustomers = new List<Customer>();

            foreach (var customer in desirializedCustomers)
            {
                if (!IsValid(customer))
                {
                    continue;
                }
                validCustomers.Add(customer);
            }

            context.Customers.AddRange(validCustomers);
            context.SaveChanges();
        }

        internal void ImportSuppliers(string jsonString)
        {
            var desirializedSuppliers = JsonConvert.DeserializeObject<Supplier[]>(jsonString);
            var validSuppliers = new List<Supplier>();

            foreach (var supplier in desirializedSuppliers)
            {
                if (!IsValid(supplier))
                {
                    continue;
                }
                validSuppliers.Add(supplier);
            }

            context.Suppliers.AddRange(validSuppliers);
            context.SaveChanges();
        }

        internal void ImportRandomSales()
        {
            var discounts = new double[] { 0, 0.05 , 0.1, 0.15 ,0.2 ,0.3 ,0.4 ,0.5 };
            var customers = context.Customers.ToArray();
            var cars = context.Cars.ToArray();
            var rnd = new Random();
            var validSales = new List<Sale>();
            var salesToAdd = rnd.Next(50, 200);
            for (int i = 0; i < salesToAdd; i++)
            {
                var carId = cars[rnd.Next(0, cars.Length)].Id;
                if (validSales.Any(x=>x.CarId==carId))
                {
                    continue;
                }

                var customer = customers[rnd.Next(0, customers.Length)];
                var isCustomerYoungDriver = customer.IsYoungDriver;
                var customerId = customer.Id;
                var discount = discounts[rnd.Next(0, 8)];
                if (isCustomerYoungDriver)
                {
                    discount += 0.5;
                }

                var sale = new Sale
                {
                    CarId=customerId,
                    CustomerId=customerId,
                    Discount=discount                    
                };

                validSales.Add(sale);
            }
            context.Sales.AddRange(validSales);
            context.SaveChanges();

        }

        internal void ImportCars(string carsJson)
        {
            var validCars = new List<Car>();
            var parts = context.Parts.ToArray();
            var partsLen = parts.Length;
            var rnd = new Random();
            var deserializedCars = JsonConvert.DeserializeObject<Car[]>(carsJson);

            foreach (var car in deserializedCars)
            {
                if (!IsValid(car))
                {
                    continue;
                }

                var partsToAdd = rnd.Next(10, 21);
                var partCarsToAdd = new List<PartCar>();
                for (int i = 0; i < partsToAdd; i++)
                {
                    var randomPart = parts[rnd.Next(0, partsLen)];
                    var randomPartId = randomPart.Id;
                    
                    var partCar = new PartCar
                    {
                        PartId = randomPartId,
                        Car=car
                    };

                    if (partCarsToAdd.Any(x=>x.PartId==randomPartId))
                    {
                        continue;
                    }
                    partCarsToAdd.Add(partCar);
                }
                car.PartCars = partCarsToAdd;
                validCars.Add(car);
            }
            context.Cars.AddRange(validCars);
            context.SaveChanges();
        }

        internal void ImportParts(string jsonString)
        {
            var deserializedParts = JsonConvert.DeserializeObject<Part[]>(jsonString);
            var validParts = new List<Part>();
            var suppliers = context.Suppliers.ToArray();
            var suppliersLength = suppliers.Length;
            var rnd = new Random();

            foreach (var part in deserializedParts)
            {
                if (!IsValid(part))
                {
                    continue;
                }

                var randomSuplier = suppliers[rnd.Next(0, suppliersLength)];
                part.Supplier = randomSuplier;
                validParts.Add(part);
            }

            context.Parts.AddRange(validParts);
            context.SaveChanges();
        }
        private bool IsValid(object obj)
        {
            var validationContect = new ValidationContext(obj);
            var validResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContect, validResults, true);

            return isValid;
        }
    }
}
