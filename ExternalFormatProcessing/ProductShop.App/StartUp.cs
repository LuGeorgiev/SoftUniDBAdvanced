using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ProductShop.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //ImportUsersFromXml();
            //string result =ImportCategoriesFromXML();
            //Console.WriteLine(result);

            //string result =ImportUsersFromJson();
            //Console.WriteLine(result);

            //var result = ImportProductsFromXml();
            //Console.WriteLine(result);

            ExportToXml();

            //using (var db = new ProductShopDbContext())
            {
                //P01
                //InitializeJson(db); //mine approach

                //P02.1
                //ExportProductsInRange(db, 500, 1000);

                //P02.2 Successfully Sold Products
                //SuccessfullySoldProducts(db);

                //P02.3 Categories By Products Count
                //CategoryByProducts(db);
            }

        }

        static void ExportToXml()
        {
            using (var db = new ProductShopDbContext())
            {
                var userNames = db.Users.Select(u => $"{u.FirstName} {u.LastName}").ToArray();

                var xDoc = new XDocument();
                xDoc.Add(new XElement("users")); //this is the ROOT

                foreach (var n in userNames)
                {
                    xDoc.Root.Add(new XElement("user", new XElement("name",n))); //ROOT is important and can be missed
                }


                string xmlString = xDoc.ToString();
                File.WriteAllText("UserExport.xml", xmlString);
            }
        }

        static string ImportProductsFromXml()
        {
            string path = "../../../Files/products.xml";

            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();
            var catProducts = new List<CategoryProduct>();
            using (var db = new ProductShopDbContext())
            {

                var userIds = db.Users.Select(u => u.Id).ToArray();
                var categoryIds = db.Categories.Select(u => u.Id).ToArray();
                Random rnd = new Random();

                foreach (var e in elements)
                {
                    string name = e.Element("name").Value;
                    decimal price = decimal.Parse(e.Element("price").Value);

                    int index = rnd.Next(0, userIds.Length);
                    int sellerId = userIds[index];

                    var product = new Product
                    {
                        Name=name,
                        Price=price,
                        SellerId=sellerId
                    };

                    int categoryIndex = rnd.Next(0, categoryIds.Length);
                    int categoryId = categoryIds[categoryIndex];

                    var catProd = new CategoryProduct
                    {
                        Product=product,
                        CategoryId=categoryId
                    };

                    catProducts.Add(catProd);
                }

                db.AddRange(catProducts);
                db.SaveChanges();
            }
            return $"{catProducts.Count} products were imported from XML!";
        }

        static string ImportCategoriesFromXML()
        {
            string path = "../../../Files/categories.xml";

            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();
            var categories = new List<Category>();

            foreach (var e in elements)
            {
                var category = new Category
                {
                    Name = e.Element("name").Value
                };
                categories.Add(category);
            };
            using (var db = new ProductShopDbContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }

            return $"{categories.Count()} categories were added!";
        }    

        static string ImportUsersFromXml()
        {
            string path = "../../../Files/users.xml";

            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();
            var users = new List<User>();
            foreach (var e in elements)
            {
                string firstName = e.Attribute("firstName")?.Value;
                string lastName = e.Attribute("lastName").Value;
                int? age = null;
                if (e.Attribute("age")!=null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }

                var user = new User
                {
                    FirstName = firstName,
                    LastName=lastName,
                    Age=age
                };
                users.Add(user);
            }
            using (var db = new ProductShopDbContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }

            return $"{users.Count} users were imported from XML!";
        }

        static void SetCategories()
        {
            using (var db = new ProductShopDbContext())
            {
                var productIds = db.Products
                    .Select(p => p.Id)
                    .ToArray();
                var categoryIds = db.Categories
                    .Select(c => c.Id)
                    .ToArray();

                int categoryCount = categoryIds.Length;
                Random rnd = new Random();
                var categoryPoducts = new List<CategoryProduct>();

                foreach (var p in productIds)
                {
                    
                    for (int i = 0; i < 3; i++)
                    {
                        int index = rnd.Next(0, categoryCount);
                        while (categoryPoducts.Any(cp=>cp.ProductId==p&&
                            cp.CategoryId==categoryIds[index]))
                        {
                            index = rnd.Next(0, categoryIds.Length);
                        }

                        var catPr = new CategoryProduct
                        {
                            ProductId = p,
                            CategoryId = categoryIds[index]
                        };
                        categoryPoducts.Add(catPr);
                    }                    
                }
                db.CategoryProducts.AddRange(categoryPoducts);
                db.SaveChanges();
            }
        }

        static string ImportProductsFromJson()
        {
            string path = "../../../Files/products.json";
            Random rnd = new Random();
            Product[] products = ImportJson<Product>(path);

            using (var db = new ProductShopDbContext())
            {
                int[] userIds = db.Users
                    .Select(y => y.Id)
                    .ToArray();
                foreach (var p in products)
                {
                    int index = rnd.Next(0, userIds.Length);
                    int sellerId = userIds[index];

                    int? buyId = sellerId;
                    while (buyId==sellerId)
                    {
                        int buyerIndex = rnd.Next(0, userIds.Length);
                        buyId = userIds[buyerIndex];
                    }

                    if (buyId-sellerId<8 && buyId-sellerId>0)
                    {
                        buyId = null;
                    }
                    p.SellerId = sellerId;
                    p.BuyerId = buyId;
                }

                db.Products.AddRange(products);
                db.SaveChanges();
            }
            return $"{products.Length} products were imported from file: {path}";
        }

        static string ImportCategoriesFromJson()
        {
            string path = "../../../Files/categories.json";
            Category[] categories = ImportJson<Category>(path);
            using (var db = new ProductShopDbContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }
            return $"{categories.Length} categories were imported from file: {path}";
        }

        static string ImportUsersFromJson()
        {
            string path = "../../../Files/users.json";
            User[] users = ImportJson<User>(path);
            using (var db = new ProductShopDbContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }
            return $"{users.Length} users were imported from file: {path}";
        }

        static T[] ImportJson<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
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
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName} "
                })
                .OrderBy(p=>p.price)
                .ToList();

            string outputFileName = "InRange.json";
            SerializeToJson(outputFileName, productsInRange);
            
        }

        private static void SerializeToJson(string outputFileName, IEnumerable<object> productsInRange)
        {
            var outputJson = JsonConvert.SerializeObject(productsInRange, Formatting.Indented, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore //Ignore null values
            });

            File.WriteAllText(outputFileName, outputJson);

        }


        private static void InitializeJson(ProductShopDbContext db)
        {
            new JsonImport(db);
        }
    }
}
