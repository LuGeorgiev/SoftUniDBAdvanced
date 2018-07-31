using System.Xml.Serialization;

namespace CarDealer.DataProcessing.Dtos.Export
{
    [XmlType("sale")]
    public class P6Sale
    {
        [XmlArray("sale")]
        public P6Car[] Cars { get; set; }

    }
}
