using Forum.App.Commands.Contracts;
using System;

namespace Forum.App.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Environment.Exit(0);

            return string.Empty;
        }
    }
}
