using Stations.Models.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stations.Models
{
    public class CustomerCard
    {
        public CustomerCard()
        {
            this.BoughtTickets = new List<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required,MaxLength(128)]
        public string Name { get; set; }

        [Range(0,120)]
        public int Age { get; set; }

        public CardType Type { get; set; } 

        public virtual ICollection<Ticket> BoughtTickets { get; set; }
    }
}