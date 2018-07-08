namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using Utilities;
    using PhotoShare.Client.Core.Contracts;
    using System;

    public class AddTagCommand:ICommand
    {
        // AddTag <tag>
        public string Execute(string[] data)
        {
            string tag = data[1].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                //P02.Extend Photo Share System refactoring
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials! Please, Login.");
                }
                
                context.Tags.Add(new Tag
                {
                    Name = tag
                });

                context.SaveChanges();
            }

            return tag + " was added successfully to database!";
        }
    }
}
