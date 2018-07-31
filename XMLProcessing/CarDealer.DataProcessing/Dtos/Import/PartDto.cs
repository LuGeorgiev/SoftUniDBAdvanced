using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataProcessing.Dtos.Import
{
    [XmlType("part")]
    public class PartDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [Required]
        [XmlAttribute("price")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        [XmlAttribute("quantity")]
        [Range(1, 2000000)]
        public int Quantity { get; set; }        
    }
}
