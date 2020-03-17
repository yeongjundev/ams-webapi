using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications.Selectors
{
    public class LessonPropertySelector : IPropertySelector<Lesson>
    {
        public Expression<Func<Lesson, object>> GetSelector(string propertyName)
        {
            Expression<Func<Lesson, object>> selector = null;
            switch (propertyName.ToLower())
            {
                case "id":
                    selector = lesson => lesson.Id;
                    break;
                case "title":
                    selector = lesson => lesson.Title;
                    break;
            }
            return selector;
        }
    }
}