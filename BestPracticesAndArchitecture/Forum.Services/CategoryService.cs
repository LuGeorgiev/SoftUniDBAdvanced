using System;
using System.Linq;
using Forum.Data;
using Forum.Models;
using Forum.Services.Contracts;

namespace Forum.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ForumDbContext context;

        public CategoryService(ForumDbContext context)
        {
            this.context = context;
        }


        public Category Create(string name)
        {
            var category = new Category()
            {
                Name = name
            };

            context.Categories.Add(category);
            context.SaveChanges();

            return category;
        }

        public Category FindById(int id)
        {
            var category = context.Categories.SingleOrDefault(x => x.Id == id);

            return category;
        }

        public Category FindByName(string name)
        {
            var category = context.Categories.SingleOrDefault(x => x.Name == name);
            
            return category;
        }
    }
}
