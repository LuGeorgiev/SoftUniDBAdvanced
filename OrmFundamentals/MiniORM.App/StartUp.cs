using MiniORM.App.Data;
using MiniORM.App.Data.Entities;
using System;
using System.Linq;

namespace MiniORM.App
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database =MiniORM; Integrated Security = True";

            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirstName="Lyubmir",
                MiddleName="Georgiev",
                LastName="Georgiev",
                DepartmentId =1,
                IsEmpolyed=true
            } );

            var employee = context.Employees.Last();
            employee.FirstName = "NoEnoughtTimeForCoding";

            context.SaveChanges();
        }
    }
}
