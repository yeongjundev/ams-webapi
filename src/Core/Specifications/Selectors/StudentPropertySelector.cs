using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.Selectors
{
    public class StudentPropertySelector : IPropertySelector<Student>
    {
        public Expression<Func<Student, object>> GetSelector(string propertyName)
        {
            Expression<Func<Student, object>> selector = null;
            switch (propertyName.ToLower())
            {
                case "id":
                    selector = student => student.Id;
                    break;
                case "firstname":
                    selector = student => student.Firstname;
                    break;
                case "middlename":
                    selector = student => student.Middlename;
                    break;
                case "lastname":
                    selector = student => student.Lastname;
                    break;
                case "email":
                    selector = student => student.Email;
                    break;
                case "phone":
                    selector = student => student.Phone;
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