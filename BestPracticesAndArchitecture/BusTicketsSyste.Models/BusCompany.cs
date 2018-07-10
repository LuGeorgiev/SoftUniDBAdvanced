using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketsSystem.Models
{
    public class BusCompany
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public float Rating
        {
            get
            {
                int counter = 0;
                float gradeSum = 0f;
                foreach (var review in this.Reviews)
                {
                    gradeSum += review.Grade;
                    counter++;
                }
                return (float)gradeSum / counter;
            }
         }

        public virtual ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
