using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProductShop.DataProcessing.Dtos.Export
{
    [XmlType("category")]
    public class CategoryCountDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("products-count")]
        public int Count { get; set; }

        [XmlElement("average-price")]
        public decimal AvgPrice { get; set; }

        [XmlElement("total-revenue")]
        public string TotalRevenue { get; set; }

    }
}
