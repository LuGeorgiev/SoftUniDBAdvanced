using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Sale
    {
        public Sale()
        {
        }

        public Sale(DateTime date, Product product, Store store)
        {
            this.Date = date;
            this.Product = product;
            this.Store = store;
        }

        public int SaleId { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int StoreId  { get; set; }
        public Store Store { get; set; }
    }
}
