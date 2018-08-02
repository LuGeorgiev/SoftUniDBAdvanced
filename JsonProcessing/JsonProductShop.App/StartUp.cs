using System;

namespace JsonProductShop.App
{
    using AutoMapper;
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();
            using (var context = new ProductShopContext())
            {
                //InitializeDb(context, mapper);

                var serializer = new Serializer(mapper, context);
                string exportPath = @"../../../ExportJson/";

                //serializer.ProductsInRange(exportPath,500, 1000);
                //serializer.SuccessfullySold(exportPath);
                //serializer.CategoriesByProductCount(exportPath);
                serializer.UsersAndProducts(exportPath);
            } 

        }

        private static void InitializeDb(ProductShopContext context, IMapper mapper)
        {
            string path = @"../../../Json/";
            DeserializeUsers(context, path);
            DeserializeProducts(context, path);
            DeserializeCategories(context, path);
        }

        private static void DeserializeCategories(ProductShopContext context, string path)
        {
            string jsonCategories = File.ReadAllText(path + "categories.json");
            var deserializedCategories = JsonConvert.DeserializeObject<Category[]>(jsonCategories);
            var validCategories = new List<Category>();

            foreach (var category in deserializedCategories)
            {
                if (!IsValid(category))
                {
                    continue;
                }
                validCategories.Add(category);
            }
            var products = context.Products.ToArray();
            var rnd = new Random();
            var categolyLength = validCategories.Count;
            var categoryProducts = new List<CategoryProduct>();

            for (int i = 0; i < products.Length; i++)
            {
                var categoriesForProduct = rnd.Next(1, 4);
                for (int j = 0; j < categoriesForProduct; j++)
                {
                    var category = validCategories[rnd.Next(0, categolyLength)];
                    var categoryProduct = new CategoryProduct
                    {
                        Category=category,
                        Product=products[i]
                    };

                    if (categoryProducts.Any(x=>x.Category==category &&x.Product==products[i]))
                    {
                        continue;
                    }
                    categoryProducts.Add(categoryProduct);
                }
            }

            context.Categories.AddRange(validCategories);
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        private static void DeserializeProducts(ProductShopContext context, string path)
        {
            string jsonProducts = File.ReadAllText(path + "products.json");
            var deserializedProducts = JsonConvert.DeserializeObject<Product[]>(jsonProducts);
            var validProducts = new List<Product>();
            var users = context.Users.ToArray();
            var usersLength = users.Length;
            var rnd = new Random();

            foreach (var product in deserializedProducts)
            {
                if (!IsValid(product))
                {
                    continue;
                }

                var seller = users[rnd.Next(0, usersLength)];
                product.Seller = seller;
                var buyer = users[rnd.Next(0, usersLength)];
                if (!(Math.Abs(seller.Id-buyer.Id)<5))
                {
                    product.Buyer = buyer;
                }
                validProducts.Add(product);
            }
            context.AddRange(validProducts);
            context.SaveChanges();
        }

        private static void DeserializeUsers(ProductShopContext context, string path)
        {
            string jsonUsers = File.ReadAllText(path + "users.json");
            var deserializedUsers = JsonConvert.DeserializeObject<User[]>(jsonUsers);
            var validUsers = new List<User>();

            foreach (var user in deserializedUsers)
            {
                if (!IsValid(user))
                {
                    continue;
                }
                validUsers.Add(user);
            }
            context.Users.AddRange(validUsers);
            context.SaveChanges();
        }

        private static bool IsValid(object obj)
        {
            var validContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validContext, validResults, true);

            return isValid;
        }
    }
}
