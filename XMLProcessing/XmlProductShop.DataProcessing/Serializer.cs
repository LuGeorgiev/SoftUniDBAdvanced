using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using XmlProductShop.DataProcessing.Dtos.Export;
using XMLProductShop.Data;

namespace XmlProductShop.DataProcessing
{
    public class Serializer
    {
        public static string ProductInRange(XmlProductShopContext context, decimal minPrice, decimal maxPrice)
        {
            var sb = new StringBuilder();

            var products = context.Products
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Buyer != null)
                .OrderByDescending(x => x.Price)
                .Select(x => new ProductRangeDto
                {
                    Name = x.Name,
                    Price = x.Price.ToString("F2"),
                    Buyer = ($"{x.Buyer.FirstName} {x.Buyer.LastName}").Trim()                    
                })
                .ToArray();


            var serializer = new XmlSerializer(typeof(ProductRangeDto[]), new XmlRootAttribute("products"));
            serializer.Serialize(new StringWriter(sb), products, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string UsersWithSoldProducts(XmlProductShopContext context)
        {
            var sb = new StringBuilder();

            var users = context.Users
                .Where(x => x.ProductsToSell.Count > 0)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new UserWithSoldProductDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProduct = x.ProductsToSell
                        .Select(s=> new SoldProductDto
                        {
                            Name=s.Name,
                            Price=s.Price
                        })
                        .ToArray()
                })
                .ToArray();


            var serializer = new XmlSerializer(typeof(UserWithSoldProductDto[]), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string CategoryByProductCount(XmlProductShopContext context)
        {
            var sb = new StringBuilder();
            var categories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new CategoryCountDto
                {
                    Name=c.Name,
                    Count=c.CategoryProducts.Count,
                    TotalRevenue=c.CategoryProducts.Sum(x=>x.Product.Price).ToString("F2"),
                    AvgPrice=c.CategoryProducts.Average(x=>x.Product.Price)
                    // If category is empty will throw exception TO BE 
                    //.....CategoryProducts.Select(s=>s.Product.Price).DefaultIfEmpty(0).Average()
                })
                .ToArray();

            var ser = new XmlSerializer(typeof(CategoryCountDto[]), new XmlRootAttribute("categories"));
            ser.Serialize(new StringWriter(sb), categories, new XmlSerializerNamespaces(new [] { XmlQualifiedName.Empty }));

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string UsersWithSoldProducts(XmlProductShopContext context, int productSold)
        {
            var sb = new StringBuilder();

            var users = new UsersDtoForth
            {
                Count = context.Users.Count(),
                Users = context.Users
                .Where(x=>x.ProductsToSell.Count>=1)
                .Select(x => new UserDtoFourProb
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age.ToString(),
                    SoldProduct= new SoldProductForth
                    {
                        Count=x.ProductsToSell.Count(),
                        ProductDto=x.ProductsToSell.Select(k=> new ProductDtoForth
                        {
                            Name=k.Name,
                            Price=k.Price
                        })
                        .ToArray()
                    }                   
                })
                .ToArray()
            };

            var xmlNamspaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(UsersDtoForth), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, xmlNamspaces);

            var result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
