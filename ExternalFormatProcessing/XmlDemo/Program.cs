using System;
using System.IO;
using System.Xml.Linq;

namespace XmlDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlString = File.ReadAllText("XmlFile.xml");

            var xml = XDocument.Parse(xmlString); //or Load instead of PArse
            Console.WriteLine();
        }
    }
}
