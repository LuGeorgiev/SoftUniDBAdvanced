using Forum.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Services.Contracts
{
    public interface IUserService
    {
        //User FindUserByID(int Id);

        //User FindByUsername(string username);

        //User FindByUsernameAndPassword(string username, string password);

        //User Create(string username, string password);

        //void Delete(int id);

        //Automapper Implementation
        TModel FindUserByID<TModel>(int Id);

        TModel FindByUsername<TModel>(string username);

        TModel FindByUsernameAndPassword<TModel>(string username, string password);

        TModel Create<TModel>(string username, string password);

        void Delete(int id);
    }
}
