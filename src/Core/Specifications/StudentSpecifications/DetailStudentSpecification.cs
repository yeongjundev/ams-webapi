using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications.StudentSpecifications
{
    public class DetailStudentSpecification : SpecificationBase<Student>
    {
        private readonly StudentPropertySelector PropertySelector = new StudentPropertySelector();

        public DetailStudentSpecification(int id) : base()
        {
            ApplyFilter(student => student.Id == id);

            ApplyInclude(student => student.Enrolments);
            ApplyInclude("Enrolments.Lesson");

            ApplyInclude(student => student.AttendanceLogs);
            ApplyInclude("AttendanceLogs.Lesson");
            ApplyInclude("AttendanceLogs.AttendanceSheet");
        }

        protected override IPropertySelector<Student> GetPropertySelector()
        {
            return PropertySelector;
        }
    }
}