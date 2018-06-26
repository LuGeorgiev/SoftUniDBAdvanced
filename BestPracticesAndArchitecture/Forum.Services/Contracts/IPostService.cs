
using Forum.Models;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Services.Contracts
{
    public interface IPostService
    {
        //Post Create( string title, string content, int categoryId, int authorId);

        void Delete(int postId); //TODO

        IQueryable<TModel> All<TModel>();

        //Post FindById(int postId);

        //Implementation with Automapper
        TModel FindById<TModel>(int postId);
        TModel Create<TModel>(string title, string content, int categoryId, int authorId);

    }
}
