using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
		{
            //var ordersByEmployee = context
            //    .Employees
            //    .Where(x => x.Name == employeeName && x.Orders.Any(z=>z.Type== Enum.Parse<OrderType>(orderType)))
            //    .Select(x => new
            //    {
            //        Name = x.Name,
            //        Orders = x.Orders
            //        .Select(o=> new
            //        {
            //            Customer=o.Customer,
            //            Items = o.OrderItems
            //            .Select(i=>new
            //            {
            //                Name=i.Item.Name,
            //                Price=i.Item.Price,
            //                Quantity =i.Quantity
            //            }).ToArray(),
            //            TotalPrice = o.OrderItems.Sum(i=>i.Quantity*i.Item.Price)
            //        })
            //        .OrderByDescending(z=>z.TotalPrice)
            //        .ThenByDescending(z=>z.Items.Count())
            //        .ToArray(),
            //        TotalMade = x.Orders.Sum(o=>o.OrderItems.Sum(i=>i.Quantity*i.Item.Price))
            //    });

            var type = Enum.Parse<OrderType>(orderType);
            var ordersByEmployee = context
                .Employees
                .ToArray()
                .Where(x => x.Name == employeeName)         
                .Select(x => new
                {
                    Name = x.Name,
                    Orders = x.Orders
                    .Where(s=>s.Type==type)
                    .Select(o => new
                    {
                        Customer = o.Customer,
                        Items = o.OrderItems
                        .Select(i => new
                        {
                            Name = i.Item.Name,
                            Price = i.Item.Price,
                            Quantity = i.Quantity
                        }).ToArray(),
                        TotalPrice = o.OrderItems.Sum(i => i.Quantity * i.Item.Price) //Or o.TotalPrice
                    })
                    .OrderByDescending(z => z.TotalPrice)
                    .ThenByDescending(z => z.Items.Count())
                    .ToArray(),
                    TotalMade = x.Orders
                        .Where(z=>z.Type==type)
                        .Sum(o => o.OrderItems.Sum(i => i.Quantity * i.Item.Price))
                })
                .FirstOrDefault();

            var jsonString = JsonConvert.SerializeObject(ordersByEmployee, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
		}

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            var sb = new StringBuilder();
            var categories = categoriesString.Split(',').ToArray();

            var xmlCategories = context.Categories
                .Where(x => categories.Contains(x.Name))
                //.OrderByDescending(x=>x.Items.Sum(z=>z.Price*z.OrderItems.Sum(y=>y.Quantity)))
                //.Take(1)
                .Select(x => new CategoryDto
                {
                    Name=x.Name,
                    MostPopularItem = x.Items
                    .Select (z=> new MostPopularDto
                    {
                         Name=z.Name,
                         TimeSold=z.OrderItems.Sum(i=>i.Quantity),
                         TotalMade=z.OrderItems.Sum(i=>i.Item.Price*i.Quantity)
                    })
                    .OrderByDescending(z=>z.TotalMade)
                    .ThenByDescending(z=>z.TimeSold)
                    .FirstOrDefault()                    
                })
                .OrderByDescending(x=>x.MostPopularItem.TotalMade)
                .ThenByDescending(x=>x.MostPopularItem.TimeSold)
                .ToArray();
            var xmlNamspaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty});
            var ser = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));
            ser.Serialize(new StringWriter(sb), xmlCategories,xmlNamspaces);

            var result = sb.ToString().TrimEnd();
            return result;
		}
	}
}