using BusTicketsSystem.Client.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Client.Core
{
    class ConsoleInOut : IWriter, IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
