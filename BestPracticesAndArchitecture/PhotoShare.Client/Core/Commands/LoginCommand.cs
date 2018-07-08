using PhotoShare.Client.Core.Contracts;
using PhotoShare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand : ICommand
    {
        public string Execute(string[] data)
        {
            var username = data[1];
            var password = data[2];

            using (var db = new PhotoShareContext())
            {
                if (Session.User!=null)
                {
                    throw new ArgumentException($"You should logout first!");
                }

                var user = db.Users
                    .FirstOrDefault(x => x.Username == username);
                if (user==null)
                {
                    throw new ArgumentException($"Invalid username or password!");
                }

                var isPassValid = user.Password == password;
                if (!isPassValid)
                {
                    throw new ArgumentException($"Invalid username or password!");
                }
                user.LastTimeLoggedIn = DateTime.Now;
                db.SaveChanges();
                Session.User = user;
            }

            return $"User {username} successfully logged in!";
        }
    }
}
