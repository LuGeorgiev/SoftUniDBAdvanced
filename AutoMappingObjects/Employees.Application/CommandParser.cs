using Employees.Application.Contracts;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.Application
{
    internal class CommandParser
    {
        public static ICommand Parse(IServiceProvider serviceProvider, string comandName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var commandTypes = assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ICommand)));

            var commandType = commandTypes
                .FirstOrDefault(x => x.Name.Equals($"{comandName}Command", StringComparison.InvariantCultureIgnoreCase));

            if (commandType==null)
            {
                throw new InvalidOperationException("Invalid command!");
            }
            var constructor = commandType
                .GetConstructors()
                .FirstOrDefault();

            var constructorParams = constructor
                .GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            var constructorArgs = constructorParams
                .Select(p => serviceProvider.GetService(p))
                .ToArray();

            var command = constructor.Invoke(constructorArgs);
            
            return (ICommand)commandType;
        }
    }
}
