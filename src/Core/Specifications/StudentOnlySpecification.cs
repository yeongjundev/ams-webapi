using Core.Entities;
using Core.Specifications.Selectors;

namespace Core.Specifications
{
    public class StudentOnlySpecification : SpecificationBase<Student>
    {
        protected readonly new StudentPropertySelector PropertySelector = new StudentPropertySelector();

        public StudentOnlySpecification(int id) : base()
        {
            ApplyFilter(student => student.Id == id);
        }
    }
}