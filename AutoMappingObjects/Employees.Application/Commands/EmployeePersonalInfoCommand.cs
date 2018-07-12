using Employees.Application.Contracts;
using Employees.Services;
using System;


namespace Employees.Application.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<emplId>
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            var e = employeeService.PersonnalById(employeeId);

            string birthday = "No birthday set!";
            if (e.Birthday!=null)
            {
                birthday = e.Birthday.Value.ToString("dd-MM-yyyy");
            }
                
            string address = e.Address ?? "No address set!";

            string result = $"ID: {employeeId} - {e.FirstName} {e.LastName} - ${e.Salary:F2}" + Environment.NewLine +
                            $"Birthday: {birthday}" + Environment.NewLine +
                            $"Address: {address}";

            return result;
        }
    }
}
