using System;
using System.Collections.Generic;

namespace Demo2SoftUni.Data.Models
{
    public class Department
    {
        public Department()
        {
            DeletedEmployees = new HashSet<DeletedEmployee>();
            Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }

        public Employee Manager { get; set; }
        public ICollection<DeletedEmployee> DeletedEmployees { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
