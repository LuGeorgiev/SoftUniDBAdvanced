using System;
using System.Linq;

namespace Employees.Application
{
    class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal void Run()
        {
            while (true)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split();

                string commandName = tokens[0];
                string[] commandArgs = tokens.Skip(1).ToArray();

                var command = CommandParser.Parse(this.serviceProvider, commandName);

                var result = command.Execute(commandArgs);


                Console.WriteLine(result);
            }
        }
    }
}
