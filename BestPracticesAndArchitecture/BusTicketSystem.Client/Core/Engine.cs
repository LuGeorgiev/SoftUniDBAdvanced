using BusTicketsSystem.Client.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusTicketsSystem.Client.Core
{
    public class Engine
    {
        private readonly IParseCommand commandParser;

        public Engine(IParseCommand comand)
        {
            this.commandParser = comand;
        }

        public void Run()
        {
            var consoleInterface = new ConsoleInOut();
            string input= "";

            while ((input=consoleInterface.ReadLine())!="exit")
            {

                try
                {
                    var commandTokens = input.Split();
                    var command = commandTokens[0].Replace("-","");
                    var data = commandTokens.Skip(1).ToArray();

                    string result = commandParser.DispatchCommand(command, data);
                    consoleInterface.WriteLine(result);
                }
                catch (Exception e)
                {
                    consoleInterface.WriteLine(e.Message);
                }
            }


        }
    }
}
