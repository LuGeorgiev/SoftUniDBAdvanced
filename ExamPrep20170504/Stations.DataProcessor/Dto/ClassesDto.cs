using System;
using System.Collections.Generic;
using System.Text;

namespace Stations.DataProcessor.Dto
{
    public class ClassesDto
    {
        //[Required, MaxLength(30)] UNIQUE
        public string Name { get; set; }
        
        //[StringLength(2, MinimumLength = 2)] UNIQUE
        public string Abbreviation { get; set; }
    }
}
