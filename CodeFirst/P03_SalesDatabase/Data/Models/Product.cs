using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {
        public Product()
        {
        }

        public Product(string name, decimal price, int qty)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = qty;
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
