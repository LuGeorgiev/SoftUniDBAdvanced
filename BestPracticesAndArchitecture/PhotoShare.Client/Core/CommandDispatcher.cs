namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Commands;
    using PhotoShare.Client.Core.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var currentCommand = commandParameters[0]+"Command";
            
            var commandType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name.Equals(currentCommand, StringComparison.InvariantCultureIgnoreCase));

            if (commandType==null)
            {
                throw new InvalidOperationException($"Command: {currentCommand} is not valid!");
            }

            var command = (ICommand)Activator.CreateInstance(commandType);

            return command.Execute(commandParameters);

            //if (command == "registeruser")
            //{
            //    result = RegisterUserCommand.Execute(commandParameters);
            //}
            //else if (command == "addtown")
            //{
            //    result = AddTownCommand.Execute(commandParameters);
            //}
            //else if (command == "modifyuser")
            //{
            //    result = ModifyUserCommand.Execute(commandParameters);
            //}
            //else if (command == "deleteuser")
            //{
            //    result = DeleteUser.Execute(commandParameters);
            //}
            //else if (command == "addtag")
            //{
            //    result = AddTagCommand.Execute(commandParameters);
            //}
            //else if (command == "addfriend")
            //{
            //    result = AddFriendCommand.Execute(commandParameters);
            //}
            //else if (command == "exit")
            //{
            //    result = ExitCommand.Execute();
            //}
            //else if (command == "exit")
            //{
            //    result = ExitCommand.Execute();
            //}
            //else if (command == "exit")
            //{
            //    result = ExitCommand.Execute();
            //}
            //else if (command == "exit")
            //{
            //    result = ExitCommand.Execute();
            //}
            //else
            //{
            //    throw new InvalidOperationException($"Command {command} is not valid!");
            //}
            //return result;
        }
    }
}
