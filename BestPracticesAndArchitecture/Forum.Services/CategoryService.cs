using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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


        public TModel Create<TModel>(string name)
        {
            var category = new Category()
            {
                Name = name
            };

            context.Categories.Add(category);
            context.SaveChanges();

            //return category;

            //Automapper TModel corrected
            var dto = Mapper.Map<TModel>(category);

            return dto;
        }

        public TModel FindById<TModel>(int id)
        {
            //var category = context.Categories.SingleOrDefault(x => x.Id == id);

            //Automapper aproach
            var category = context.Categories
                .Where(x => x.Id == id)
                .ProjectTo<TModel>()
                .SingleOrDefault();
            return category;
        }

        public TModel FindByName<TModel>(string name)
        {
            //var category = context.Categories.SingleOrDefault(x => x.Name == name);

            //Automaper
            var category = context.Categories
                .Where(x => x.Name == name)
                .ProjectTo<TModel>()
                .SingleOrDefault();

            return category;
        }
    }
}
