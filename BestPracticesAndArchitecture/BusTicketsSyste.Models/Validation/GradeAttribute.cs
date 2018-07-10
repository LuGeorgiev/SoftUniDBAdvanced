using System;
using System.ComponentModel.DataAnnotations;

namespace BusTicketsSystem.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class GradeAttribute :ValidationAttribute
    {
        private const float MinGrade = 1;
        private const float MaxGrade = 10;

        public override bool IsValid(object value)
        {
            if (value==null)
            {
                return true;
            }

            float grade = float.Parse(value.ToString());
            if (grade<1f||grade>10f)
            {
                return false;
            }

            return true;
        }
    }
}
