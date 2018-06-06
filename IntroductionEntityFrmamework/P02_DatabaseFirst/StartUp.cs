using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace P02_DatabaseFirst
{
    public class StartUp
    {
        static void Main()
        {
            var db = new SoftUniContext();

            var employees = db
                .Employees
                .OrderBy(e=>e.EmployeeId)
                .ToList();

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} " +
                    $"{employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }
            
        }
    }
}
