using AutoMapper;
using System;
using System.IO;
using XMLProductShop.Data;

namespace XmlProductShop.Application
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new XmlProductShopContext())
            {
                ResedDatabase(context);
                Mapper.Initialize(cfg => cfg.AddProfile<XmlProduckShopProfile>());
                //var config = new MapperConfiguration(cfg=> 
                //{
                //    cfg.AddProfile<XmlProduckShopProfile>();
                //});
                //var mapper = config.CreateMapper();

                ImportEntities(context);
                ExportEntities(context);
            }
        }


        private static void ImportEntities(XmlProductShopContext context, string baseDir = @"..\..\..\Resources\")
        {
            const string exportDir= @"..\..\..\Resources\ImporResults\";

            string users = DataProcessing.Deserializer.ImportUsers(context, File.ReadAllText(baseDir + "users.xml"));
            ExportToFile(users, exportDir + "Users.txt");

            string categories = DataProcessing.Deserializer.ImportCategories(context, File.ReadAllText(baseDir + "categories.xml"));
            ExportToFile(categories, exportDir + "Categories.txt");


            string products = DataProcessing.Deserializer.ImportProducts(context, File.ReadAllText(baseDir + "products.xml"));
            ExportToFile(products, exportDir + "Products.txt");

        }


        private static void ExportEntities(XmlProductShopContext context)
        {
            const string exportDir = @"..\..\..\Resources\ExportResults\";

            string xmlProductInRange = DataProcessing.Serializer.ProductInRange(context, 1000m, 2000m);
            Console.WriteLine(xmlProductInRange);
            File.WriteAllText(exportDir + "products-in-range.xml", xmlProductInRange);

            string usersWithSoldProducts = DataProcessing.Serializer.UsersWithSoldProducts(context);
            Console.WriteLine(usersWithSoldProducts);
            File.WriteAllText(exportDir + "uers-sold-products-V3.xml", usersWithSoldProducts);

            string categoryByCount = DataProcessing.Serializer.CategoryByProductCount(context);
            Console.WriteLine(categoryByCount);
            File.WriteAllText(exportDir + "categories-by-products.xml", categoryByCount);

            string usersAndProducts = DataProcessing.Serializer.UsersWithSoldProducts(context, 1);
            Console.WriteLine(usersAndProducts);
            File.WriteAllText(exportDir + "users-and-products.xml", usersAndProducts);
        }

        private static void ExportToFile(string toExport, string outputPath)
        {
            Console.WriteLine(toExport);
            File.WriteAllText(outputPath, toExport.TrimEnd());
        }

        private static void ResedDatabase(XmlProductShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
