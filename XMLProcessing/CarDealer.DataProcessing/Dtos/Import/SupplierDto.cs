using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataProcessing.Dtos.Import
{
    [XmlType("supplier")]
    public class SupplierDto
    {
        [XmlAttribute("name")]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [XmlAttribute("is-importer")]
        public bool IsImporter { get; set; }
    }
}
