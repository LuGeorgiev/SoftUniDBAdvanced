using P02_DatabaseFirst.Contracts;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using P02_DatabaseFirst.Utilities;
using System;
using System.Linq;
using System.Text;

namespace P02_DatabaseFirst
{
    public class StartUp
    {
        static void Main()
        {
            IWriter writer = new ConsoleWriter();
            
            using (var db = new SoftUniContext())
            {

                string employees = PrintAllEmployee(db);
                writer.WiteLine(employees);
            }   
                     
           
        }

        private static string PrintAllEmployee(SoftUniContext db)
        {
            var employees = db
                .Employees
                .OrderBy(e => e.EmployeeId)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} " +
                    $"{employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
