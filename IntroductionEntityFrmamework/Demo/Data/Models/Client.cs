
using System.Collections.Generic;

namespace Demo.Data.Models
{
    public partial class Client
    {
        public Client()
        {           
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
