using FastFood.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FastFood.Models
{
    public class Order
    {
        public Order()
        {
            this.Type = OrderType.ForHere;
            this.OrderItems = new List<OrderItem>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public OrderType Type { get; set; }

        [NotMapped] //required
        public decimal TotalPrice => this.OrderItems.Sum(x => x.Quantity * x.Item.Price);

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}