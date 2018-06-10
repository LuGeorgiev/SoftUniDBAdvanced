using System;
using System.Collections.Generic;

namespace Cars.Data.Models
{
    public class Car
    {
        public int CarId { get; set; }

        public int MakeId { get; set; }

        public Make Make { get; set; }

        public int EngineId { get; set; }

        public Engine Engine { get; set; }

        public int Doors { get; set; }

        

        public Transmission Transmission { get; set; }

        public DateTime ProductionYear { get; set; }

        public int LicensePlateId { get; set; }
        public LicensePlate LicensePlate { get; set; }

        public ICollection<CarDealership> CarDealerships { get; set; } = new List<CarDealership>();
    }
}
