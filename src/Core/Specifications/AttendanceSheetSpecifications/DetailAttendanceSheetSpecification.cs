using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications.AttendanceSheetSpecifications
{
    public class DetailAttendanceSheetSpecification : SpecificationBase<AttendanceSheet>
    {
        private readonly AttendanceSheetPropertySelector PropertySelector = new AttendanceSheetPropertySelector();

        public DetailAttendanceSheetSpecification(int id) : base()
        {
            ApplyFilter(attendanceSheet => attendanceSheet.Id == id);

            ApplyInclude(attendanceSheet => attendanceSheet.Lesson);
            ApplyInclude(attendanceSheet => attendanceSheet.AttendanceLogs);
            ApplyInclude("AttendanceLogs.Student");
        }

        protected override IPropertySelector<AttendanceSheet> GetPropertySelector()
        {
            return PropertySelector;
        }
    }
}