using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductShop.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new ProductShopDbContext())
            {
                //P01
                //InitializeJson(db);

                //P02.1
                //ExportProductsInRange(db, 500, 1000);

                //P02.2 Successfully Sold Products
                //SuccessfullySoldProducts(db);

                //P02.3 Categories By Products Count
                CategoryByProducts(db);


            }

        }

        private static void CategoryByProducts(ProductShopDbContext db)
        {
            var categoriesByProduct = db.Categories
                .Where(x=>x.CategoryProducts.Count>0)
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    category = x.Name,
                    productsCount= x.CategoryProducts.Count,
                    averagePrice = x.CategoryProducts.Average(z=>z.Product.Price),
                    totalRevenoue =x.CategoryProducts.Sum(z=>z.Product.Price)
                })
                .ToList();

            var outputFileName = "CategoyByProduct.json";
            SerializeToJson(outputFileName, categoriesByProduct);
        }

        private static void SuccessfullySoldProducts(ProductShopDbContext db)
        {
            var successfullySold = db.Users
                .Where(p => p.ProductsForSale.Any(x => x.Buyer != null))
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(p => new
                {
                    firstName = p.FirstName,
                    lastName = p.LastName,
                    soldProducts = p.ProductsForSale
                    .Where(pr=>pr.Buyer!=null)
                    .Select(pr=>new
                    {
                        name=pr.Name,
                        price=pr.Price,
                        buyerFisrtName = pr.Buyer.FirstName,
                        buyerLastName = pr.Buyer.LastName,
                    })
                })
                .ToList();

            string outputFileName = "SuccessfullySold.json";
            SerializeToJson(outputFileName,successfullySold);
        }

        private static void ExportProductsInRange(ProductShopDbContext db, int v1, int v2)
        {
            var productsInRange = db.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,                    
                    seller = p.Seller.LastName //cannot find way to importfirst name and ommit nulls
                })
                .OrderBy(p=>p.price)
                .ToList();

            string outputFileName = "InRange.json";
            SerializeToJson(outputFileName, productsInRange);
            
        }

        private static void SerializeToJson(string outputFileName, IEnumerable<object> productsInRange)
        {
            var outputJson = JsonConvert.SerializeObject(productsInRange, Formatting.Indented);
            File.WriteAllText(outputFileName, outputJson);
        }

        private static void InitializeJson(ProductShopDbContext db)
        {
            new JsonImport(db);
        }
    }
}
