using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.ModelsDto
{
    public class UserDto
    {
        private string username;
        private string password;
        private string profilePicture;

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                //if (string.IsNullOrWhiteSpace(value)||value.Length>30)
                //{
                //    throw new ArgumentException();
                //}
                this.username = value;
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                //if (string.IsNullOrWhiteSpace(value) || value.Length > 20)
                //{
                //    throw new ArgumentException();
                //}
                this.password = value;
            }
        }
        public string ProfilePicture
        {
            get
            {
                return this.profilePicture;
            }
            set
            {
                //if (string.IsNullOrWhiteSpace(value))
                //{
                //    throw new ArgumentException();
                //}
                this.profilePicture = value;
            }
        }
    }
}
