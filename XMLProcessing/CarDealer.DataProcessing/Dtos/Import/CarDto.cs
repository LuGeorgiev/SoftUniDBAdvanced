using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataProcessing.Dtos.Import
{
    [XmlType("car")]
    public class CarDto
    {
        [Required]
        [StringLength(50)]
        [XmlElement("make")]
        public string Make { get; set; }

        [Required]
        [MaxLength(50)]
        [XmlElement("model")]
        public string Model { get; set; }

        [Required]
        [MaxLength(10)]
        [XmlElement("travelled-distance")]
        public string TravelledDistance { get; set; }
    }
}
