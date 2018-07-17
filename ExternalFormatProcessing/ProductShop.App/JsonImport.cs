using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductShop.App
{
    class JsonImport
    {
        private ProductShopDbContext db;
        public JsonImport(ProductShopDbContext db)
        {
            this.db = db;
            Initialize();
        }

        private void Initialize()
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));
            db.AddRange(users);
            db.SaveChanges();

            var categories = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText("categories.json"));
            db.AddRange(categories);
            db.SaveChanges();

            var products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("products.json"));

            var usersCount = users.Count;
            var categoriesCount = categories.Count;

            var rnd = new Random();

            foreach (var product in products)
            {
                int sellerId = GenerateId(usersCount,rnd);
                product.SellerId = sellerId;

                int buyerId = GenerateId(usersCount, rnd);                

                if (Math.Abs(buyerId - sellerId)<=2)
                {
                    product.BuyerId = null; //some null values
                }
                else
                {
                    product.BuyerId = buyerId;
                }

                product.CategoryProducts
                    .Add(new CategoryProduct { CategoryId = GenerateId(categoriesCount,rnd) });

                db.Products.Add(product);
                db.SaveChanges();
            }

        }

        private int GenerateId(int usersCount, Random rnd)
        {
            int id = rnd.Next(1, usersCount);

            return id;
        }
        
    }
}
