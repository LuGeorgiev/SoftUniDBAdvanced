using Employees.Data;
using Employees.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System;

namespace Employees.Application
{
    class StartUp
    {
        static void Main()
        {
            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider);
        }

        static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(option=>option.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<EmployeeService>();

            serviceCollection.AddAutoMapper(cfg=>cfg.AddProfile<AutoMapperProfile>());


            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
