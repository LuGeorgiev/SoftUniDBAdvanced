using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProductShop.DataProcessing.Dtos.Export
{
    [XmlType("user")]    
    public class UserWithSoldProductDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }
        
        [XmlArray("sold-products")]        
        public SoldProductDto[] SoldProduct { get; set; }
    }
}
