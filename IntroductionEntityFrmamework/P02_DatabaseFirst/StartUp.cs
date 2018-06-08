using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Contracts;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using P02_DatabaseFirst.Utilities;
using System;
using System.Globalization;
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
                // 3. Employees Full Information
                //string employees = PrintAllEmployee(db);
                //writer.WiteLine(employees);

                //4.	Employees with Salary Over 50 000
                //int salary = 50000;
                //string employeesWithSalary = ExtractAllBySalary(db, salary);
                //writer.WiteLine(employeesWithSalary);

                //5.Employees from Research and Development
                //string department = "Research and Development";
                //string employeesFromDepartment = ExtractEmployeeFromDepartment(db, department);
                //writer.WiteLine(employeesFromDepartment);

                //6.	Adding a New Address and Updating Employee
                //string result = AddNewAddresAndUpdateEmployee(db);
                //writer.WiteLine(result);

                //7.	Employees and Projects
                //string result = EmployeesAndProjects(db);
                //writer.WiteLine(result);

                //8.	Addresses by Town
                //PrintAddressesByTown(db);

                //9.	Employee 147
                //PrintEmployeeById(db, 147);

                //10.	Departments with More Than 5 Employees
                //PintDepartmentsWithMoreThanFiveEmployees(db);

                //11.	Find Latest 10 Projects
                //PrintLastTenProjects(db);

                //12.	Increase Salaries
                //IncreaseSalaries(db);

                //13.	Find Employees by First Name Starting With "Sa"
                //PrintEmployeeStartWith(db, "Sa");

                //14.	Delete Project by Id
                //DeletProjectById(db, 2);

                //15.	Remove Towns
                RemoveTown(db, "Seattle");
            }
        }

        private static void RemoveTown(SoftUniContext db, string townName)
        {
            var townToDelete = db.Towns
                .Where(x => x.Name == townName)
                .FirstOrDefault();
                
            var adressesToDelete = db.Addresses
                .Where(x => x.Town == townToDelete);
            int count = 0;
            foreach (var employee in db.Employees)
            {
                foreach (var address in adressesToDelete)
                {
                    if (employee.AddressId==address.AddressId)
                    {                        
                        employee.AddressId = null;
                        count++;
                    }
                }
            }
            foreach (var address in adressesToDelete)
            {
                db.Addresses.Remove(address);
            }
            db.Towns.Remove(townToDelete);
            db.SaveChanges();

            Console.WriteLine($"{count} address in {townName} was deleted");
        }
        private static void PrintEmployeeStartWith(SoftUniContext db, string startsWith)
        {
            db.Employees
                .Where(x => x.FirstName.StartsWith(startsWith))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Select(x => new
                {
                    Name = $"{x.FirstName} {x.LastName}",
                    x.JobTitle,
                    x.Salary
                })
                .ToList()
                .ForEach(x => Console.WriteLine($"{x.Name} - {x.JobTitle} - (${x.Salary:F2})"));
        }
        private static void IncreaseSalaries(SoftUniContext db)
        { 
            var happyEmployees = db.Employees
                .Where(x => x.Department.Name == "Engineering" || x.Department.Name == "Tool Design" ||
                x.Department.Name == "Marketing" || x.Department.Name == "Information Services")
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);
                

            foreach (var emp in happyEmployees)
            {
                emp.Salary *= 1.12m;
            }
            db.SaveChanges();

            foreach (var empolyee in happyEmployees)
            {
                Console.WriteLine($"{empolyee.FirstName} {empolyee.LastName} (${empolyee.Salary:F2})");
            }
        }
        private static void PrintLastTenProjects(SoftUniContext db)
        {
            //var lastProjects = 
                db.Projects
                .OrderByDescending(x => x.StartDate)
                .ThenBy(x => x.Name)
                .Take(10)
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                    x.StartDate
                })
                .ToList()
                .ForEach(x=>Console.WriteLine($"{x.Name }{Environment.NewLine}{x.Description}{Environment.NewLine}{x.StartDate.ToString("M/d/yyyy h:mm:ss tt")}"));


        }
        private static void PintDepartmentsWithMoreThanFiveEmployees(SoftUniContext db)
        {
            var departments = db.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    DepName = d.Name,
                    ManagerName = $"{d.Manager.FirstName} {d.Manager.LastName}",
                    Employees = d.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .Select(e => new
                        {
                            EmployeeName = $"{e.FirstName} {e.LastName}",
                            EmployeJob = e.JobTitle
                        })
                });

            foreach (var dep in departments)
            {
                Console.WriteLine($"{dep.DepName} - {dep.ManagerName}");
                foreach (var emp in dep.Employees)
                {
                    Console.WriteLine($"{emp.EmployeeName} - {emp.EmployeJob}");
                }
                Console.WriteLine(new string('-',10));
            }
        }
        private static void PrintEmployeeById(SoftUniContext db, int employeeId)
        {
            var employees = db.Employees
                .Where(e => e.EmployeeId == employeeId)
                .Select(e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",                    
                    JobTitle = $"{e.JobTitle}",
                    Projects = e.EmployeeProjects
                        .OrderBy(ep=>ep.Project.Name)
                        .Select(ep=> new
                        {
                            ProjectName = ep.Project.Name
                        })
                });

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Name} - {employee.JobTitle}");
                foreach (var project in employee.Projects)
                {
                    Console.WriteLine($"{project.ProjectName}");
                }
            }
        }
        private static void PrintAddressesByTown(SoftUniContext db)
        {
            var addresses = db.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(a => new
                {
                    a.AddressText,
                    a.Town.Name,
                    a.Employees.Count
                });

            addresses.ToList()
                .ForEach(a => Console.WriteLine($"{a.AddressText}, {a.Name} - {a.Count} employees"));
        }
        private static void DeletProjectById(SoftUniContext db, int id)
        {
            var empProj = db.EmployeesProjects
                .Where(ep => ep.ProjectId == id);
            foreach (var ep in empProj)
            {
                db.EmployeesProjects.Remove(ep);
            }

            var project = db.Projects.Find(id);
            db.Projects.Remove(project);

            db.SaveChanges();

            db.Projects
               .Take(10)
               .ToList()
               .ForEach(p => Console.WriteLine(p.Name));
        }
        private static string EmployeesAndProjects(SoftUniContext db)
        {
            var result = db.Employees
                .Where
                (
                    e => e.EmployeeProjects.Any(p =>
                          p.Project.StartDate.Year >= 2001 &&
                          p.Project.StartDate.Year <= 2003
                ))
                .Take(30)
                .Select(x => new
                {
                    Name = $"{x.FirstName} {x.LastName}",
                    ManagerName = $"{x.Manager.FirstName} {x.Manager.LastName}",
                    Projects = x.EmployeeProjects
                        .Select(p => new
                        {
                            ProjName = $"{p.Project.Name}",
                            p.Project.StartDate,
                            p.Project.EndDate
                        })
                });

            var sb = new StringBuilder();
            foreach (var empoloyee in result)
            {
                sb.AppendLine($"{empoloyee.Name} - Manager: {empoloyee.ManagerName}");
                foreach (var project in empoloyee.Projects)
                {
                    string endDate ="";
                    if (project.EndDate ==null)
                    {
                        endDate = "not finished";
                    }
                    else
                    {
                        endDate = $"{project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt")}";
                    }
                    sb.AppendLine($"--{project.ProjName} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {endDate}");
                }
            }

            return sb.ToString();    
        }
        private static string AddNewAddresAndUpdateEmployee(SoftUniContext db)
        {
            var address = new Address() { TownId = 4, AddressText = "Vitoshka 15" };

            db.Addresses.Add(address);
            var employee = db.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");
            employee.Address = address;

            db.SaveChanges();

            var result = db.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText);

            var sb = new StringBuilder();
            foreach (var item in result)
            {
                sb.AppendLine(item);
            }
            return sb.ToString().TrimEnd();
        }
        private static string ExtractEmployeeFromDepartment(SoftUniContext db, string department)
        {
            var employees = db
                .Employees
                .Where(e => e.Department.Name == department)
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e=>new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    e.Salary,
                    DepartmentName = e.Department.Name
                });

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.Name} from " +
                    $"{employee.DepartmentName} - ${employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }
        private static string ExtractAllBySalary(SoftUniContext db, int salary)
        {
            var employees = db
                .Employees                
                .Where(e=>e.Salary>50000)
                .OrderBy(e => e.FirstName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName}");
            }

            return sb.ToString().TrimEnd();
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
