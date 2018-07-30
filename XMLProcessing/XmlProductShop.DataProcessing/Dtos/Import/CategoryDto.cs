using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace XmlProductShop.DataProcessing.Dtos.Import
{
    
    [XmlType("category")]
    public class CategoryDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        [XmlElement("name")]
        public string name { get; set; }
    }
}