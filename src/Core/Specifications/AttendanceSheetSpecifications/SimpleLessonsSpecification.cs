using System.Collections.Generic;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications.AttendanceSheetSpecifications
{
    public class SimpleAttendanceSheetsSpecification : SpecificationBase<AttendanceSheet>
    {
        private readonly AttendanceSheetPropertySelector PropertySelector = new AttendanceSheetPropertySelector();

        public SimpleAttendanceSheetsSpecification(
            List<(string, bool)> orderByInfos,
            int currentPage, int pageSize
        ) : base(orderByInfos, currentPage, pageSize)
        {
            ApplyInclude(attendanceSheet => attendanceSheet.Lesson);
        }

        protected override IPropertySelector<AttendanceSheet> GetPropertySelector()
        {
            return PropertySelector;
        }
    }
}