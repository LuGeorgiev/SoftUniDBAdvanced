
using Forum.App.Commands.Contracts;

namespace Forum.App.Commands
{
    public class WhoAmICommand : ICommand
    {
        public string Execute(params string[] args)
        {
            if (Session.User==null)
            {
                return "You are not logged in";
            }
            var currentUser = Session.User.Username;
            return $"{currentUser}";
        }
    }
}
