using Forum.Models;


namespace Forum.Services.Contracts
{
    public interface ICategoryService
    {
        //Category FindById(int id);

        //Category FindByName(string name);

        //Category Create(string name);

        //Automapper Implemenrtaion
        TModel FindById<TModel>(int id);

        TModel FindByName<TModel>(string name);

        TModel Create<TModel>(string name);

    }
}
