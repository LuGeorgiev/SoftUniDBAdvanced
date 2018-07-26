using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Stations.DataProcessor.Dto
{
    [XmlType("Card")]
    public class CardDtoExport
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        public TicetDtoExport[] Tickets { get; set; }
    }
}
