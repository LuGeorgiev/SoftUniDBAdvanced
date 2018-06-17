using Demo.Data;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Z.EntityFramework.Plus;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EmployeesDbContext();

            //var employees = new[]
            //{
            //    new Employee{FirstName="GIvan", LastName="petrov", Salary=6788.98m },
            //    new Employee{FirstName="Vpetkan", LastName="Ivanov", Salary=1988.98m },
            //    new Employee{FirstName="Dragan", LastName="stefanof", Salary=2673.98m },
            //    new Employee{FirstName="Maria", LastName="Manolova", Salary=3788.98m },
            //    new Employee{FirstName="Ivanka", LastName="Petrova", Salary=63488.98m },
            //    new Employee{FirstName="Draganka", LastName="georgieva", Salary=67883.8m },
            //};
            //context.Employees.AddRange(employees);
            //context.SaveChanges();


            ////Native SQL Queries - All comulns have to be included, JOINS cannot be used
            //var query=@"SELECT TOP(2) id,FirstName, LAstName, Salary FROM Employees";
            //var employees = context
            //    .Employees
            //    .FromSql(query)
            //    .ToArray();
            //Console.WriteLine(employees.Count());


            //// Execute procedures
            //var query = @"EXEC usp_UpdateSalary {0}, {1}"; //{} params

            //var result = context.Database.ExecuteSqlCommand(query,1,5000);
            //Console.WriteLine(result);


            //Bulk opperations - Z.EntityFramework.Plus.EFCore

            //var result = context.Employees.Where(e => e.Salary < 7000).Delete(); 
            //This si from Z.EF.Plus Can be achieved with RemoveRange from LINQ but it is slower

            //EF Core update
            //var employees = context.
            //    Employees
            //    .Where(e => e.Salary < 3000);
            //foreach (var employee in employees)
            //{
            //    employee.Salary += 200;
            //}
            //Bulk update
            context.Employees
                .Where(e => e.Salary < 3000)
                .Update(e => new Employee() { Salary =e.Salary+ 2000m });

            context.SaveChanges();
                       


        }
    }
}
