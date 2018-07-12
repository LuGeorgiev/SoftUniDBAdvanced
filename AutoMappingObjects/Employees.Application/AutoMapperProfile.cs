using AutoMapper;
using Employees.DtoModels;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application
{
    class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto,Employee>();

            CreateMap<Employee, EmployeePersonalDto>();
            CreateMap<EmployeePersonalDto, Employee>(); // not needed because we have only to read
        }
    }
}
