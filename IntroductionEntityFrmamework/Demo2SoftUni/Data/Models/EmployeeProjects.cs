using System;
using System.Collections.Generic;

namespace Demo2SoftUni.Data.Models
{
    public class EmployeeProjects
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }

        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}
