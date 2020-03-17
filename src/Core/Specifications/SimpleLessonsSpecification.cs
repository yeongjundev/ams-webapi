using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications
{
    public class SimpleLessonsSpecification : SpecificationBase<Lesson>
    {
        private readonly LessonPropertySelector PropertySelector = new LessonPropertySelector();

        public SimpleLessonsSpecification(
            List<(string, bool)> orderByInfos,
            int currentPage, int pageSize
        ) : base(orderByInfos, currentPage, pageSize) { }

        protected override IPropertySelector<Lesson> GetPropertySelector()
        {
            return PropertySelector;
        }
    }
}