using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications.EnrolmentSpecifications
{
    public class EnrolmentsSpecification : SpecificationBase<Enrolment>
    {
        private readonly EnrolmentPropertySelector PropertySelector = new EnrolmentPropertySelector();

        public EnrolmentsSpecification(
            List<(string, bool)> orderByInfos,
            int currentPage, int pageSize
        ) : base(orderByInfos, currentPage, pageSize)
        {
            ApplyInclude(enrolment => enrolment.Student);
            ApplyInclude(enrolment => enrolment.Lesson);
        }

        protected override IPropertySelector<Enrolment> GetPropertySelector()
        {
            return PropertySelector;
        }
    }
}