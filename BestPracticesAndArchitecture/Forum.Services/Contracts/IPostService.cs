
using Forum.Models;
using System.Collections.Generic;

namespace Forum.Services.Contracts
{
    public interface IPostService
    {
        Post Create( string title, string content, int categoryId, int authorId);

        void Delete(int postId);

        IEnumerable<Post> All();

        Post FindById(int postId);
        
    }
}
