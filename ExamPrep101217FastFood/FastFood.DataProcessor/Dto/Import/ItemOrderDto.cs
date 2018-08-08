using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Item")]
    public class ItemOrderDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)] //Unique
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [Range(1, 2147483647)]
        [XmlElement("Quantity")]
        public int Quantity { get; set; }
    }
}
