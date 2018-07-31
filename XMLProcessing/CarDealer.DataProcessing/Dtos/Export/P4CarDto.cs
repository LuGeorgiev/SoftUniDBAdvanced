using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DataProcessing.Dtos.Export
{
    [XmlType("car")]
    public class P4CarDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public int TravelledDistance { get; set; }

        [XmlArray("parts")]
        public P4PartDto[] Part { get; set; }
    }
}
