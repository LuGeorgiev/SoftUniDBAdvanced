using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Stations.DataProcessor.Dto
{
    [XmlType("Ticket")]
    public class TicketDto
    {
        [Required]
        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [Required]
        [XmlAttribute("seat")]
        [RegularExpression(@"^[A-Z]{2}\d{1,6}$")]
        public string Seat { get; set; }

        [Required]
        public TicketTripDto Trip { get; set; }

        public TicketCardDto Card { get; set; }
    }
}
