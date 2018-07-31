using System.Xml.Serialization;

namespace CarDealer.DataProcessing.Dtos.Export
{
    [XmlType("car")]
    public class P6Car
    {
        [XmlAttribute("make")]
        public string Make { get; set; }
        
        [XmlAttribute("model")]
        public string Model { get; set; }
        
        [XmlAttribute("travelled-distance")]
        public int TravelledDistance { get; set; }

        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        [XmlElement("discount")]
        public string Discount { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal DiscountedPrice { get; set; }

    }
}