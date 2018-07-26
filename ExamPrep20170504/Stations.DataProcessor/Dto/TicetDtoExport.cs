using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Stations.DataProcessor.Dto
{
    [XmlType("Ticket")]
    public class TicetDtoExport
    {
        public string OriginStation { get; set; }

        public string DestinationStation { get; set; }

        public string DepartureTime { get; set; }
    }
}
