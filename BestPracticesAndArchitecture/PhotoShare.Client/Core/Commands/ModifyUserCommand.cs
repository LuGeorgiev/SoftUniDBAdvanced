namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Data;
    using System;
    using System.Linq;

    public class ModifyUserCommand:ICommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            var userName = data[1];
            var property = data[2].ToLower();
            var newValue = data[3];
            using (var context = new PhotoShareContext())
            {
                //P01 approach
                //var user = context.Users
                //    .FirstOrDefault(u => u.Username == userName);
                //if (user==null)
                //{
                //    throw new ArgumentException($"User: {userName} was not found!");
                //}

                //P02.Extend Photo Share System refactoring
                if (Session.User==null)
                {
                    throw new InvalidOperationException("Invalid credentials! Please, Login.");
                }
                var user = Session.User;

                string exceptionMsg = $"Value {newValue} is not valid!"+ Environment.NewLine;
                if (property=="password")
                {
                    if (!newValue.Any(c=>Char.IsLower(c))||!newValue.Any(b=>Char.IsDigit(b)))
                    {
                        throw new ArgumentException(exceptionMsg+ "Invalid Password");
                    }
                    user.Password = newValue;
                }
                else if (property == "borntown")
                {
                    var bornTown = context.Towns
                        .FirstOrDefault(t => t.Name == newValue);
                    if (bornTown==null)
                    {
                        throw new ArgumentException(exceptionMsg+"InvalidTown");
                    }
                    user.BornTown = bornTown;
                }
                else if (property == "currenttown")
                {
                    var currentTown = context.Towns
                        .FirstOrDefault(t => t.Name == newValue);
                    if (currentTown == null)
                    {
                        throw new ArgumentException(exceptionMsg + $"Town {newValue} was not found!");
                    }
                    user.CurrentTown = currentTown;
                }
                else
                {
                    throw new ArgumentException($"Property {property} not supported");
                }

                context.SaveChanges();
                return $"User {userName} {property} {newValue}";
            }
        }
    }
}
