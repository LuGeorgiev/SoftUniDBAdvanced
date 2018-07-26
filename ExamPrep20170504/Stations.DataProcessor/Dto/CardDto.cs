using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Stations.DataProcessor.Dto
{
    [XmlType("Card")]
    public class CardDto
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Range(0, 120)]
        [Required]
        public int Age { get; set; }

        public string Type { get; set; } = "Normal";
    }
}
