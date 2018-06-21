using Forum.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Services.Contracts
{
    public interface IUserService
    {
        User FindUserByID(int Id);

        User FindByUsername(string username);

        User FindByUsernameAndPAssword(string username, string password);

        User Create(string username, string password);

        void Delete(int id); 
    }
}
