using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using XMLProductShop.Models;

namespace XmlProductShop.DataProcessing.Dtos.Import
{
    [XmlType("product")]
    public class ProductDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        [XmlElement("name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("price")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        // needed only for my approach
        //public int? BuyerId { get; set; }   

        //public int SellerId { get; set; }        
    }
}
