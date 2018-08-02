using AutoMapper;
using JsonProductShop.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JsonProductShop.App
{
    class Serializer
    {
        private readonly IMapper mapper;
        private readonly ProductShopContext context;

        public Serializer(IMapper mapper, ProductShopContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        internal void ProductsInRange(string exportPath, int minRange, int maxRange)
        {
            string exportFileName = "products-in-range.json";

            var products = context.Products
                .Where(x => x.Price >= minRange && x.Price <= maxRange)
                .OrderByDescending(x => x.Price)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName ?? x.Seller.LastName
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(products,Formatting.Indented);
            File.WriteAllText(exportPath + exportFileName, jsonString);
        }

        internal void UsersAndProducts(string exportPath)
        {
            string exportFileName = "users-and-products.json";

            var user = new
            {
                userCount =context.Users
                    .Where(u=>u.ProductsSold.Count>=1)
                    .Count(),
                users = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Select(u=>new
                {
                    firstName = u.FirstName,
                    lastName =u.LastName,
                    age = u.Age,
                    soldProducts  = new
                    {
                        count = u.ProductsSold.Count,
                        products = u.ProductsSold
                        .Where(x => x.Buyer != null)
                        .Select(x => new
                        {
                            name = x.Name,
                            price = x.Price
                        })
                        .ToArray()
                    }
                })
                .ToArray()
            };

            var jsonString = JsonConvert.SerializeObject(user, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });
            File.WriteAllText(exportPath + exportFileName, jsonString);
        }

        internal void CategoriesByProductCount(string exportPath)
        {
            string exportFileName = "categories-by-products.json";

            var categories = context.Categories
                .OrderByDescending(x=>x.CategoryProducts.Count)
                .Select(x=>new
                {
                    category=x.Name,
                    productCount = x.CategoryProducts.Count,
                    averagePrice = (x.CategoryProducts.Sum(z=>z.Product.Price)/x.CategoryProducts.Count).ToString("F2"),
                    totalRevenue=x.CategoryProducts.Sum(z=>z.Product.Price)
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(categories, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });
            File.WriteAllText(exportPath + exportFileName, jsonString);
        }

        internal void SuccessfullySold(string exportPath)
        {
            string exportFileName = "users-sold-products.json";

            var users = context.Users
                .Where(x => x.ProductsSold.Count >= 1)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new
                {
                    firsName = x.FirstName,
                    lastName = x.LastName,
                    soldProducts = x.ProductsSold
                        .Where(ps => ps.Buyer != null)
                        .Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price,
                            buyerFirstName = ps.Buyer.FirstName,
                            buyerLastName = ps.Buyer.LastName
                        })
                        .ToArray()
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                NullValueHandling=NullValueHandling.Ignore,
                Formatting=Formatting.Indented                
            });
            File.WriteAllText(exportPath + exportFileName, jsonString);
        }
    }
}
