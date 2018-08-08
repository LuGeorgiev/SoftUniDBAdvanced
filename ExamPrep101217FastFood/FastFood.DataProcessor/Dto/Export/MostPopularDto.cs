using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Export
{
    [XmlType("MostPopularItem")]
    public class MostPopularDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("TotalMade")]
        public decimal TotalMade { get; set; }

        [XmlElement("TimesSold")]
        public int TimeSold { get; set; }

    }
}