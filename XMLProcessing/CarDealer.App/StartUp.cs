using AutoMapper;
using CarDealer.Data;
using System;
using System.IO;
using CarDealer.DataProcessing;

namespace CarDealer.App
{

    class StartUp
    {

        static void Main()
        {
            using (var context = new CarDealerDbContext())
            {
                //ResetDatabase(context);
                Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());
                //ImportFromXml(context);
                ExportToXml(context);
            }
        }

        private static void ExportToXml(CarDealerDbContext context)
        {
            const string exportDir = @"..\..\..\ExportResults\";

            //string ferrariCars = Serializer.ExportCarsMadeBy(context, "Ferrari");
            //File.WriteAllText(exportDir + "ferrari-cars.xml", ferrariCars);

            //string xmlCarsWithDistance = Serializer.ExportCarsWithDistance(context, 2000000);            
            //File.WriteAllText(exportDir + "cars.xml", xmlCarsWithDistance);


            //string localSuppliers = Serializer.ExportLocalSuppliers(context);
            //File.WriteAllText(exportDir + "local-suppliers.xml", localSuppliers);

            //string carsAndParts = Serializer.ExportCarsAndParts(context);
            //File.WriteAllText(exportDir + "cars-and-parts.xml", carsAndParts);

            //string totalSalesByCustomer = Serializer.ExportTotalSalesBycustomer(context);
            //File.WriteAllText(exportDir + "customers-total-sales.xml", totalSalesByCustomer);

            string salesWithDiscount = Serializer.ExportSalesWithDiscount(context);
            File.WriteAllText(exportDir + "sales-discounts.xml", salesWithDiscount);
        }

        private static void ImportFromXml(CarDealerDbContext context)
        {
            string inputPath = @"../../../XmlFiles/";

            string xmlSuplyers = File.ReadAllText(inputPath + "suppliers.xml");
            Deserializer.ImportSuppliers(context, xmlSuplyers);

            string xmlParts = File.ReadAllText(inputPath + "parts.xml");
            Deserializer.ImportParts(context, xmlParts);

            string xmlCars = File.ReadAllText(inputPath + "cars.xml");
            Deserializer.ImportCars(context, xmlCars);

            string xmlCustomer = File.ReadAllText(inputPath + "customers.xml");
            Deserializer.ImportCustomers(context, xmlCustomer);

            Deserializer.ImportRandomSales(context);
        }

        private static void ResetDatabase(CarDealerDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
