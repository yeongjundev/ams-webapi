using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications.LessonSpecifications
{
    public class DetailLessonSpecification : SpecificationBase<Lesson>
    {
        private readonly LessonPropertySelector PropertySelector = new LessonPropertySelector();

        public DetailLessonSpecification(int id) : base()
        {
            ApplyFilter(lesson => lesson.Id == id);

            ApplyInclude(lesson => lesson.Enrolments);
            ApplyInclude("Enrolments.Student");

            ApplyInclude(lesson => lesson.AttendanceSheets);
        }

        protected override IPropertySelector<Lesson> GetPropertySelector()
        {
            return PropertySelector;
        }
    }
}