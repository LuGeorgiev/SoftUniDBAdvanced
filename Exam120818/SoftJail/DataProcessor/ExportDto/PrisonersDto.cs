using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoners")]
    public class PrisonersDto
    {
        [XmlElement("Prisoner")]
        public PrisonerExportDto[] Prisoners { get; set; }
    }
}
