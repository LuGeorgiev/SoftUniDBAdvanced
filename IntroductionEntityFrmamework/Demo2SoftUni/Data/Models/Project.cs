using System;
using System.Collections.Generic;

namespace Demo2SoftUni.Data.Models
{
    public class Project
    {
        public Project()
        {
            EmployeesProjects = new HashSet<EmployeeProjects>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<EmployeeProjects> EmployeesProjects { get; set; }
    }
}
