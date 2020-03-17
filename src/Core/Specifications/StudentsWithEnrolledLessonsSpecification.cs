// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Core.Entities;
// using Core.Specifications.Selectors;

// namespace Core.Specifications
// {
//     public class StudentOnlySpecification : SpecificationBase<Student>
//     {
//         protected readonly new StudentPropertySelector PropertySelector = new StudentPropertySelector();

//         public StudentWithEnrolledLessonsSpecification(int id, string searchKey, (string, bool)[] orderByPropertyNames, int currentPage, int pageSize) : base(searchKey, orderByPropertyNames, currentPage, pageSize)
//         {

//         }
//     }
// }