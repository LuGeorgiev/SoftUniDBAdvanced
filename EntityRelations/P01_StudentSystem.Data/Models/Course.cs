using System;
using System.Collections.Generic;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<StudentCourse> StudentsCourse { get; set; } = new List<StudentCourse>();

        public ICollection<HomeworkSubmission> HomeworksSubmission { get; set; } = new List<HomeworkSubmission>();

        public ICollection<Resource> Resources { get; set; } = new List<Resource>();
    }
}
