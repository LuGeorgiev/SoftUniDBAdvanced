﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos.Import
{
    [XmlType("AnimalAid")]
    public class AnimalAidProcDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
