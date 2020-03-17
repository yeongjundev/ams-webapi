using System.Collections.Generic;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications.LessonSpecifications
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