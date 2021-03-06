﻿using System;

namespace P01_HospitalDatabase.Data.Models
{
    public class Visitation
    {
        public Visitation()
        {
        }

        public Visitation(string comments, DateTime date)
        {
            this.Comments = comments;
            this.Date = date;
        }
        public int VisitationId { get; set; }
        public string Comments{ get; set; }
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
        public Patient Patient{ get; set; }

        //Because there will be Visitations without doctors in Initial Migration we use INT?
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
