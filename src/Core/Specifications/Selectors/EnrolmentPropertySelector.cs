using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.Selectors
{
    public class EnrolmentPropertySelector : IPropertySelector<Enrolment>
    {
        public Expression<Func<Enrolment, object>> GetSelector(string propertyName)
        {
            Expression<Func<Enrolment, object>> selector = null;
            switch (propertyName.ToLower())
            {
                case "title":
                    selector = enrolment => enrolment.Lesson.Title;
                    break;
                case "firstname":
                    selector = enrolment => enrolment.Student.Firstname;
                    break;
                case "middlename":
                    selector = enrolment => enrolment.Student.Middlename;
                    break;
                case "lastname":
                    selector = enrolment => enrolment.Student.Lastname;
                    break;
                case "email":
                    selector = enrolment => enrolment.Student.Email;
                    break;
                case "phone":
                    selector = enrolment => enrolment.Student.Phone;
                    break;
            }
            return selector;
        }
    }
}