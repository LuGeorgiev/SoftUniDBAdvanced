using Forum.Services.Contracts;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Forum.App
{
    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Run()
        {
            var databaseInitializerService = serviceProvider.GetService<IDbInitializerService>();
            databaseInitializerService.InitializeDatabase(); //create Migration DB

            while (true)
            {
                Console.WriteLine("Please, enter command:");
                var input = Console.ReadLine();

                var commandTokens = input.Split();
                var commandName = commandTokens[0];
                var commandArgs = commandTokens.Skip(1).ToArray();

                try
                {
                    var command = CommandParser.ParseCommand(serviceProvider,commandName);
                    var result = command.Execute(commandArgs);
                    Console.WriteLine(result);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);                    
                }
            }
        }
    }
}
