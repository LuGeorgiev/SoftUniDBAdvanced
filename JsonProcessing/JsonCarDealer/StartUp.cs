using JsonCarDealer.Data;
using System;
using System.IO;

namespace JsonCarDealer
{
    class StartUp
    {
        private const string IMPORT_PATH = @"../../../Resources/";
        private const string EXPORT_PATH = @"../../../Export/";

        static void Main()        {
            
            using (var context = new CarDealerContext())
            {
                //RestartDb(context);
                //ImportFromJson(context);                
                ExportData(context);
            }
        }

        private static void ExportData(CarDealerContext context)
        {
            var serializer = new Serializer(context);

            //string orderedCustomers = serializer.ExportOrderedCustomers();
            //File.WriteAllText(EXPORT_PATH + "ordered-customers.json", orderedCustomers);

            //string carsFromMake = serializer.ExportCarsMadeBy("Toyota");
            //File.WriteAllText(EXPORT_PATH + "toyota-cars.json", carsFromMake);

            //string localSupliers = serializer.ExportLocalSupliers();
            //File.WriteAllText(EXPORT_PATH + "local-suppliers.json", localSupliers);

            //string carsWithTheirParts = serializer.ExportCarsWithTheirParts();
            //File.WriteAllText(EXPORT_PATH + "cars-and-parts.json", carsWithTheirParts);

            //string totalSaleByCustomer = serializer.ExportTotalSaleByCustomer();
            //File.WriteAllText(EXPORT_PATH + "customers-total-sales.json", totalSaleByCustomer);

            string salesWithDiscount = serializer.ExportSalesWithDiscount();
            File.WriteAllText(EXPORT_PATH + "sales-discounts.json", salesWithDiscount);

        }

        private static void ImportFromJson(CarDealerContext context)
        {
            var deserializer = new Deserializer(context);

            var customersJson = File.ReadAllText(IMPORT_PATH + "customers.json");
            deserializer.ImportCustomers(customersJson);

            var suppliersJson = File.ReadAllText(IMPORT_PATH + "suppliers.json");
            deserializer.ImportSuppliers(suppliersJson);

            var partsJson = File.ReadAllText(IMPORT_PATH + "parts.json");
            deserializer.ImportParts(partsJson);

            var carsJson = File.ReadAllText(IMPORT_PATH + "cars.json");
            deserializer.ImportCars(carsJson);

            deserializer.ImportRandomSales();
        }

        private static void RestartDb(CarDealerContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
