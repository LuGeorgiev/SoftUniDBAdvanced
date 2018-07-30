using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProductShop.DataProcessing.Dtos.Export
{
    [XmlType("sold-products")]
    public class SoldProductForth
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public ProductDtoForth[] ProductDto { get; set; }
    }
}
