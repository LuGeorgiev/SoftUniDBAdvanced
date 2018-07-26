using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.DataProcessor.Dto
{
    public class TripDto
    {        
        [Required]
        public string OriginStation { get; set; }
                
        [Required]
        public string DestinationStation { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public string ArrivalTime { get; set; }  // TODO to be after DepartureTime
                
        [Required]
        public string Train { get; set; }

        [RegularExpression(@"^[0-2]{1}[0-9]{1}:[0-5]{1}[0-9]{1}$")]
        public string TimeDifference { get; set; }

        public string Status { get; set; } = "OnTime";
    }
}
