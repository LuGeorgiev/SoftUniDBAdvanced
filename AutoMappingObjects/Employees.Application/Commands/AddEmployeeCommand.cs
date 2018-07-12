using Employees.Application.Contracts;
using Employees.DtoModels;
using Employees.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Commands
{
    class AddEmployeeCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public AddEmployeeCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        //<firstName> <lastName> <salary> 
        public string Execute(params string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse( args[2]);

            var employeeDto = new EmployeeDto(firstName, lastName, salary);

            employeeService.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} successfully added!";
        }
    }
}
