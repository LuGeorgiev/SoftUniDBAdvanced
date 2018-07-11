using BusTicketsSystem.Client.Core.Contracts;
using BusTicketsSystem.Data;
using System;
using System.Linq;
using System.Reflection;

namespace BusTicketsSystem.Client.Core
{
    public class CommandParser : IParseCommand
    {
        public string DispatchCommand(string command,string[] data)
        {
            var commandToExecute = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name.Equals(command + "Command", StringComparison.InvariantCultureIgnoreCase));
            if (commandToExecute==null)
            {
                throw new InvalidOperationException($"Command: {command} is not implemented yet!");
            }

            var instance = (ICommand)Activator.CreateInstance(commandToExecute);
            string result = null;

            using (var db = new BusTicketsSystemContext())
            {
                result = instance.Execute(db, data);
            }

            return result;
        }
    }
}
