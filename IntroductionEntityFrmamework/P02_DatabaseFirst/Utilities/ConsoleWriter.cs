using P02_DatabaseFirst.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace P02_DatabaseFirst.Utilities
{
    public class ConsoleWriter : IWriter
    {
        public void WiteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
