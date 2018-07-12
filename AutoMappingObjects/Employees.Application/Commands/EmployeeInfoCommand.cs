using Employees.Application.Contracts;
using Employees.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Commands
{
    class EmployeeInfoCommand : ICommand
    {

        private readonly EmployeeService employeeService;

        public EmployeeInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId>
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);

            var employee = employeeService.GetById(employeeId);

            return $"ID: {employeeId} - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}";
        }
    }
}
