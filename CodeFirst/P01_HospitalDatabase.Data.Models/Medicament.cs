﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Medicament
    {
        public Medicament()
        {
            this.Perscriptions = new List<PatientMedicament>();
        }

        public Medicament(string name):base()
        {
            this.Name = name;
        }

        public int MedicamentId { get; set; }
        public string Name { get; set; }

        public ICollection<PatientMedicament> Perscriptions { get; set; } //= new List<PatientMedicament>();
    }
}
