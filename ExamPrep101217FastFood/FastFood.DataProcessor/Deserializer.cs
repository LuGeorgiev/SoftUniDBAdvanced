using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";
		private const string SuccessMessageOrder = "Order for {0} on {1} added";

		public static string ImportEmployees(FastFoodDbContext context, string jsonString)
		{
            var desEmpl = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);
            var sb = new StringBuilder();
            var validEmployees = new List<Employee>();
            var validPositions = new List<Position>();
            foreach (var employee in desEmpl)
            {
                if (!IsValid(employee))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var entry = new Employee
                {
                    Age=employee.Age,
                    Name=employee.Name
                };

                var position = validPositions.FirstOrDefault(x => x.Name == employee.Position);
                if (position==null)
                {
                    var newPosition = new Position
                    {
                        Name=employee.Position
                    };
                    validPositions.Add(newPosition);
                    position = newPosition;
                }

                entry.Position = position;
                validEmployees.Add(entry);
                sb.AppendLine(string.Format(SuccessMessage,employee.Name));
            }
            context.Employees.AddRange(validEmployees);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd() ;
            return result;
		}

		public static string ImportItems(FastFoodDbContext context, string jsonString)
		{
            var desItems = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);
            var sb = new StringBuilder();
            var validItems = new List<Item>();
            var validCategories = new List<Category>();

            foreach (var item in desItems)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                //not unique
                if (validItems.Any(x=>x.Name==item.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var entry = new Item
                {
                    Name=item.Name,
                    Price=item.Price
                };

                var category = validCategories.FirstOrDefault(x => x.Name == item.Category);
                if (category==null)
                {
                    var newCategory = new Category
                    {
                        Name = item.Category
                    };
                    validCategories.Add(newCategory);
                    category = newCategory;
                }
                entry.Category = category;
                validItems.Add(entry);
                sb.AppendLine(string.Format(SuccessMessage, item.Name));
            }
            context.Items.AddRange(validItems);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
		}

		public static string ImportOrders(FastFoodDbContext context, string xmlString)
		{
            var sb = new StringBuilder();
            var ser = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));
            var desOrders = (OrderDto[])ser.Deserialize(new StringReader(xmlString));
            var validOrders = new List<Order>();
            var existingItems = context.Items.ToArray();
            var existingemployees = context.Employees.ToArray();

            foreach (var dtoOrder in desOrders)
            {
                if (!IsValid(dtoOrder))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var employee = existingemployees.FirstOrDefault(x => x.Name == dtoOrder.Employee);
                if (employee==null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var dateTime = DateTime.ParseExact(dtoOrder.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                
                var validType = Enum.TryParse<OrderType>(dtoOrder.Type,  out OrderType type);
                if (!validType)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var newOrder = new Order
                {
                    Customer=dtoOrder.Customer,
                    DateTime=dateTime,
                    EmployeeId=employee.Id,
                    Type=type
                };

                bool isItemExisting = true;
                foreach (var itemDto in dtoOrder.Items)
                {
                    var currentItem =existingItems.FirstOrDefault(x=>x.Name==itemDto.Name);
                    if (currentItem==null)
                    {
                        sb.AppendLine(FailureMessage);
                        isItemExisting = false;
                        break;
                    }

                    newOrder
                        .OrderItems
                        .Add(new OrderItem
                        {
                            Quantity =itemDto.Quantity,
                            ItemId=currentItem.Id
                        });
                }
                if (!isItemExisting)
                {
                    continue;
                }

                sb.AppendLine(string.Format(SuccessMessageOrder, dtoOrder.Customer, dtoOrder.DateTime));
                validOrders.Add(newOrder);
            }
            context.Orders.AddRange(validOrders);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
		}

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return result;
        }
	}
}