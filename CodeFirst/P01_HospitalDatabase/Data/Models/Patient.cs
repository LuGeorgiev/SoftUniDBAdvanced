
using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Visitations = new List<Visitation>();
            this.Diagnoses = new List<Diagnose>();
            this.Perscriptions = new List<PatientMedicament>();
        }

        public Patient(string firstName, string lastName, string address, string email, bool hasInsurance):base()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Email = email;
            this.HasInsurance = hasInsurance;
        }

        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool HasInsurance { get; set; }

        public ICollection<Visitation> Visitations { get; set; } //= new List<Visitation>();
        public ICollection<Diagnose> Diagnoses { get; set; } // = new List<Diagnose>();
        public ICollection<PatientMedicament> Perscriptions { get; set; } //= new List<PatientMedicament>();
    }
}
