using AutoMapper;
using Employees.Data;
using Employees.DtoModels;
using Employees.Models;
using System;

namespace Employees.Services
{
    public class EmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext contex)
        {
            this.context = contex;
        }

        public EmployeeDto GetById(int id)
        {
            var employee = context.Employees.Find(id);
            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = Mapper.Map<Employee>(dto);
            context.Employees.Add(employee);

            context.SaveChanges();
        }

        public string SetBirthday(int employeeId, DateTime date)
        {
            var employee = context.Employees.Find(employeeId);
            employee.Birthday = date;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);
            employee.Address = address;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public EmployeePersonalDto PersonnalById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);
            var employeeDto = Mapper.Map<EmployeePersonalDto>(employee);

            return employeeDto;
        }
    }
}
