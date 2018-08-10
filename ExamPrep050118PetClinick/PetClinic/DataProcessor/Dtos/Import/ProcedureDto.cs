using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos.Import
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [Required]
        [XmlElement("Vet")]
        public string Vet { get; set; }

        [Required]
        [XmlElement("Animal")]
        public string Animal { get; set; }

        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public AnimalAidProcDto[] AnimalAids { get; set; }
    }
}
