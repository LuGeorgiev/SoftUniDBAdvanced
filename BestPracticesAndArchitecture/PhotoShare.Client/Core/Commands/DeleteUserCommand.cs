namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;
    using PhotoShare.Client.Core.Contracts;

    public class DeleteUserCommand:ICommand
    {
        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[1];
            using (PhotoShareContext context = new PhotoShareContext())
            {
                // P02.Extend Photo Share System refactoring
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials! Please, Login.");
                }
                var user = Session.User;

                var userInput = context.Users.FirstOrDefault(u => u.Username == username);
                if (userInput == null)
                {
                    throw new InvalidOperationException($"User with {username} was not found!");
                }

                if (!userInput.IsDeleted.Value)
                {
                    throw new InvalidOperationException($"User {username} is alreday deleted!");
                }

                if (user.Username!=userInput.Username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                userInput.IsDeleted = true;
                context.SaveChanges();

                return $"User {username} was deleted from the database!";
            }
        }
    }
}
