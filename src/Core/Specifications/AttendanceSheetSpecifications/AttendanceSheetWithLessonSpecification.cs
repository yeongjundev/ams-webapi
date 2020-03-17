using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications.AttendanceSheetSpecifications
{
    public class AttendanceSheetWithLessonSpecification : SpecificationBase<AttendanceSheet>
    {
        private readonly AttendanceSheetPropertySelector PropertySelector = new AttendanceSheetPropertySelector();

        public AttendanceSheetWithLessonSpecification(int id) : base()
        {
            ApplyFilter(attendanceSheet => attendanceSheet.Id == id);

            ApplyInclude(attendanceSheet => attendanceSheet.Lesson);
        }

        protected override IPropertySelector<AttendanceSheet> GetPropertySelector()
        {
            return PropertySelector;
        }
    }
}