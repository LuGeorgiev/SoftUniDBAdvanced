using System;
using System.Linq;
using FastFood.Data;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
        private const string SuccesUpdate = "{0} Price updated from ${1:F2} to ${2:F2}";
        private const string FailUpdate = "Item {0} not found!";

	    public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	    {
            var item = context.Items
                .FirstOrDefault(x => x.Name == itemName);
            if (item==null)
            {
                return string.Format(FailUpdate, itemName);
            }
            var oldPrice = item.Price;

            item.Price = newPrice;
            context.Items.Update(item);
            context.SaveChanges();

            return string.Format(SuccesUpdate, itemName, oldPrice,newPrice);
        }
    }
}
