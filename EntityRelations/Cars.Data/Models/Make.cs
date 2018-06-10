using System.Collections.Generic;

namespace Cars.Data.Models
{
    public class Make
    {
        public int MakeId { get; set; }

        public string  Name{ get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
