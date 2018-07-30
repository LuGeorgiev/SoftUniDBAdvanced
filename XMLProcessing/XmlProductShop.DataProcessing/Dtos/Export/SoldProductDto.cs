using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProductShop.DataProcessing.Dtos.Export
{
    [XmlType("product")]
    public class SoldProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
