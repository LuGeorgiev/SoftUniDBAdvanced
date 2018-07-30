using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProductShop.DataProcessing.Dtos.Export
{
    [XmlRoot("users")]
    public class UsersDtoForth
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UserDtoFourProb[] Users { get; set; }
    }
}
