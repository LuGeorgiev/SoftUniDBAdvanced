using Forum.Models;


namespace Forum.Services.Contracts
{
    public interface ICategoryService
    {
        Category FindById(int id);

        Category FindByName(string name);

        Category Create(string name);
    }
}
