
using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Visitations = new List<Visitation>();
        }

        public Doctor(string name, string spec)
            :base()
        {
            this.Name = name;
            this.Speciality = spec;
        }
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public ICollection<Visitation> Visitations { get; set; }
    }
}
