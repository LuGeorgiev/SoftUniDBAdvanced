using System.Xml.Serialization;

namespace CarDealer.DataProcessing.Dtos.Export
{
    [XmlType("part")]
    public class P4PartDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}