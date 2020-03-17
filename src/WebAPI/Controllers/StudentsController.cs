using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    public class StudentsController : ApiControllerBase<StudentsController>
    {
        public StudentsController(ILogger<StudentsController> logger, IMapper mapper) : base(logger, mapper) { }

        // api/students/
        // [HttpPost]
        // public IActionResult PostStudent([FromBody] Student newStudent)
        // {

        // }
    }
}