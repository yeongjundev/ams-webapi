using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.Selectors
{
    public class AttendanceSheetPropertySelector : IPropertySelector<AttendanceSheet>
    {
        public Expression<Func<AttendanceSheet, object>> GetSelector(string propertyName)
        {
            Expression<Func<AttendanceSheet, object>> selector = null;
            switch (propertyName.ToLower())
            {
                case "id":
                    selector = attendanceSheet => attendanceSheet.Id;
                    break;
                case "lesson.title":
                    selector = attendanceSheet => attendanceSheet.Lesson.Title;
                    break;
                case "startdatetime":
                    selector = attendanceSheet => attendanceSheet.StartDateTime;
                    break;
                case "enddatetime":
                    selector = attendanceSheet => attendanceSheet.EndDateTime;
                    break;
                case "createdatetime":
                    selector = attendanceSheet => attendanceSheet.CreateDateTime;
                    break;
                case "updatedatetime":
                    selector = attendanceSheet => attendanceSheet.UpdateDateTime;
                    break;
            }
            return selector;
        }
    }
}