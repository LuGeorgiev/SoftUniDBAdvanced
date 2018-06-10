
using System.Collections.Generic;

namespace Cars.Data.Models
{
    public class Engine
    {
        public int EngineId { get; set; }        

        public FuelType FuelType { get; set; }

        public double Capacity { get; set; }

        public int Horsepower { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
