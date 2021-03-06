﻿

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PetClinic.Models
{
    public class Procedure
    {
        public Procedure()
        {
            this.ProcedureAnimalAids = new List<ProcedureAnimalAid>();
        }
        [Key]
        public int Id { get; set; } 

        //[ForeignKey("Animal")]
        public int AnimalId { get; set; }

        [Required]
        public Animal Animal { get; set; }

        //[ForeignKey("Vet")]
        public int VetId { get; set; }

        [Required]
        public Vet Vet { get; set; }

        public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }

        
        [NotMapped]
        public decimal Cost => this.ProcedureAnimalAids.Sum(x => x.AnimalAid.Price);

        [Required]
        public DateTime DateTime { get; set; }
    }
}
