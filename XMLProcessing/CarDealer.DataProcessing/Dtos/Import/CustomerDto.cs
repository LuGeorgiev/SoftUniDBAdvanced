using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataProcessing.Dtos.Import
{
    [XmlType("customer")]
    public class CustomerDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("birth-date")]
        public DateTime? BirthDate { get; set; }

        [XmlElement("is-young-driver")]
        public bool IsYoungDriver  {get; set; }

    }
}
