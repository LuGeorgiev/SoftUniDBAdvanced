
using System.Collections.Generic;

namespace Demo.Data.Models
{
    public class Mechanic
    {
        public Mechanic()
        {            
        }

        public int MechanicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
