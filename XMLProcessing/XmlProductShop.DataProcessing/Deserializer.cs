using System;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using XmlProductShop.DataProcessing.Dtos.Import;
using XMLProductShop.Data;
using XMLProductShop.Models;
using System.Linq;

namespace XmlProductShop.DataProcessing
{
    public class Deserializer
    {
        public static string ImportCategories(XmlProductShopContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            var deserializedCategories = (CategoryDto[])serializer.Deserialize(new StringReader(xmlString));
            var validCategories = new List<Category>();
            foreach (var categotyDto in deserializedCategories)
            {
                if (!IsValid(categotyDto))
                {
                    continue;
                }
                var category = Mapper.Map<Category>(categotyDto);
                validCategories.Add(category);
                sb.AppendLine($"Category {category.Name} was added!");
            }

            //var xmlDoc = XDocument.Parse(xmlString);
            //var elements = xmlDoc.Root.Elements();
            //foreach (var element in elements)
            //{                
            //    string name = element.Element("name").Value;
            //    if (name.Length<3||name.Length>15)
            //    {
            //        sb.AppendLine("Invalid category.");
            //        continue;
            //    }
            //    var category = new Category
            //    {
            //        Name = name
            //    };

            //    validCategories.Add(category);
            //    sb.AppendLine($"Categoty: {name} was added.");
            //}
            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }


        public static string ImportUsers(XmlProductShopContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var ser = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            var deserializedUsers = (UserDto[])ser.Deserialize(new StringReader(xmlString));
            var validUsers = new List<User>();

            foreach (var userDto in deserializedUsers)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }
                var user = Mapper.Map<User>(userDto);
                validUsers.Add(user);
                sb.AppendLine($"User {user.LastName} was added");
            }

            //var xmlDoc = XDocument.Parse(xmlString);
            //var elements = xmlDoc.Root.Elements();
            //foreach (var element in elements)
            //{
            //    int? age=null;
            //    if (element.Attribute("age")?.Value != null)
            //    {
            //        age = int.Parse(element.Attribute("age").Value);
            //    }
            //    UserDto userDto = new UserDto
            //    {
            //        FirstName = element.Attribute("firstName")?.Value,
            //        LastName = element.Attribute("lastName")?.Value,
            //        Age = age
            //    };

            //    if (!IsValid(userDto))
            //    {
            //        sb.AppendLine("Invalid input");
            //        continue;
            //    }

            //    var user = Mapper.Map<User>(userDto);
            //    validUsers.Add(user);
            //    sb.AppendLine($"User: {userDto.LastName} was added!");
            //}

            context.Users.AddRange(validUsers);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportProducts(XmlProductShopContext context, string xmlString)
        {
            var sb = new StringBuilder();
            User[] users = context.Users.ToArray();
            int usersCount = users.Length;
            var rnd = new Random();
            var validProducts = new List<Product>();

            var ser = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            var desProducts = (ProductDto[])ser.Deserialize(new StringReader(xmlString));
            int counter = 0;
            foreach (var prodDto in desProducts)
            {
                if (!IsValid(prodDto))
                {
                    continue;
                }
                var product = Mapper.Map<Product>(prodDto);
                var buyerId = rnd.Next(1, 30);
                var sellerId = rnd.Next(31, 56);

                product.BuyerId = buyerId;
                product.SellerId = sellerId;
                if (counter++ == 4)
                {
                    product.BuyerId = null;
                    counter = 0;
                }
                counter++;

                Category[] categories = context.Categories.ToArray();
                int categoryCount = categories.Length;
                for (int i = 0; i < 3; i++)
                {
                    int index = rnd.Next(0, categoryCount);
                    var categoryId = categories[index].Id;
                    if (product.CategoryProducts.Any(x => x.CategoryId == categoryId))
                    {
                        continue;
                    }
                    product.CategoryProducts.Add(new CategoryProduct
                    {
                        CategoryId = categoryId
                    });
                }

                validProducts.Add(product);
                sb.AppendLine($"product {product.Name} was added");
            }

            //var xmlDoc = XDocument.Parse(xmlString);
            //var elements = xmlDoc.Root.Elements();
            //foreach (var element in elements)
            //{
            //    var name = element.Element("name").Value;
            //    var price = decimal.Parse(element.Element("price").Value);
            //    var seller = users[rnd.Next(0, usersCount)];
            //    var buyer = users[rnd.Next(0, usersCount)];
            //    if (Math.Abs(buyer.Id-seller.Id)<5)
            //    {
            //        buyer = null;
            //    }

            //    var productDto = new ProductDto
            //    {
            //        Name=name,
            //        Price=price,
            //        SellerId=seller.Id,
            //        BuyerId=buyer?.Id
            //    };

            //    if (!IsValid(productDto))
            //    {
            //        sb.AppendLine("Invalid Input.");
            //        continue;
            //    }

            //    var product = Mapper.Map<Product>(productDto);
            //    Category[] categories = context.Categories.ToArray();
            //    int categoryCount = categories.Length;
            //    for (int i = 0; i < 3; i++)
            //    {
            //        int index = rnd.Next(0, categoryCount );
            //        var categoryId = categories[index].Id;
            //        if (product.CategoryProducts.Any(x=>x.CategoryId==categoryId))
            //        {
            //            continue;
            //        }
            //        product.CategoryProducts.Add(new CategoryProduct
            //        {
            //            CategoryId = categoryId
            //        });
            //    }

            //    validProducts.Add(product);
            //    sb.AppendLine($"Product{product.Name} was added.");
            //}
            context.Products.AddRange(validProducts);
            context.SaveChanges();



            var result = sb.ToString().TrimEnd();
            return result;
        }

        private static bool IsValid(object obj)
        {
            var validContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validContext, validResults, true);

            return isValid;
        }
    }
}
