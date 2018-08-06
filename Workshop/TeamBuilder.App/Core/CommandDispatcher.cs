using System;
using System.Linq;
using System.Reflection;
using TeamBuilder.App.Contracts;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core
{
    public class CommandDispatcher
    {
        private readonly TeamBuilderContext context;
        public CommandDispatcher(TeamBuilderContext context)
        {
            this.context = context;
        }
        public string Dispatch(string input)
        {
            string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            string[] args = inputArgs.Skip(1).ToArray();

            var assembly = Assembly.GetExecutingAssembly();
            var command = assembly.GetTypes()
                .FirstOrDefault(x => x.Name.Equals(commandName + "Command",StringComparison.InvariantCultureIgnoreCase));
            if (command==null)
            {
                throw new InvalidOperationException(ErrorMessages.CommandNotAllowed);
            }

            if (command.IsAssignableFrom(typeof(ICommand)))
            {
                throw new ArgumentException(ErrorMessages.CommandNotAllowed);

            }

            var instance = (ICommand)Activator.CreateInstance(command);
            string result = instance.Execute(context, args);

            return result;
        }
    }
}
