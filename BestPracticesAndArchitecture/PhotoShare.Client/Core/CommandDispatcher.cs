namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Commands;
    using System;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var command = commandParameters[0].ToLower();
            string result = "";

            if (command == "registeruser")
            {
                result = RegisterUserCommand.Execute(commandParameters);
            }
            else if (command == "addtown")
            {
                result = AddTownCommand.Execute(commandParameters);
            }
            else if (command == "modifyuser")
            {
                result = ModifyUserCommand.Execute(commandParameters);
            }
            else if (command == "deleteuser")
            {
                result = DeleteUser.Execute(commandParameters);
            }
            else if (command == "addtag")
            {
                result = AddTagCommand.Execute(commandParameters);
            }
            else if (command == "addfriend")
            {
                result = AddFriendCommand.Execute(commandParameters);
            }
            else if (command == "exit")
            {
                result = ExitCommand.Execute();
            }
            else
            {
                throw new InvalidOperationException($"Command {command} is not valid!");
            }

            return result;
        }
    }
}
