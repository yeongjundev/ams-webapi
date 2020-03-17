using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications
{
    public class SimpleStudentsSpecification : SpecificationBase<Student>
    {
        private readonly StudentPropertySelector PropertySelector = new StudentPropertySelector();

        public SimpleStudentsSpecification(
            List<(string, bool)> orderByInfos,
            int currentPage, int pageSize
        ) : base(orderByInfos, currentPage, pageSize) { }

        protected override IPropertySelector<Student> GetPropertySelector()
        {
            return PropertySelector;
        }
    }
}