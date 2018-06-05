
using System.Collections.Generic;

namespace Demo.Data.Models
{
    public class PartModel
    {
        public PartModel()
        {            
        }

        public int ModelId { get; set; }
        public string Name { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
