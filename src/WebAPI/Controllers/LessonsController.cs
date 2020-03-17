using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Specifications.LessonSpecifications;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DTOs.LessonDTOs;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    public class LessonsController : ApiControllerBase<LessonsController>
    {
        public LessonsController(ILogger<LessonsController> logger, IMapper mapper, IUnitOfWork uow) : base(logger, mapper, uow) { }

        // api/lessons/
        [HttpPost]
        public IActionResult PostLesson([FromBody] PostLessonDTO body)
        {
            var newLesson = _mapper.Map<Lesson>(body);
            _uow.Repository<Lesson>().Add(newLesson);

            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return CreatedAtRoute("GetLesson", new { id = newLesson.Id }, _mapper.Map<SimpleLessonDTO>(newLesson));
        }

        // api/lessons/{id:int}
        [HttpGet("{id}", Name = "GetLesson")]
        public async Task<IActionResult> GetLesson([FromRoute] int id)
        {
            var qro = await _uow.Repository<Lesson>().Find(new DetailLessonSpecification(id));
            if (qro == null || qro.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var lesson = qro.QueryResult.SingleOrDefault();
            if (lesson == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DetailLessonDTO>(lesson));
        }

        // api/lessons/
        [HttpGet]
        public async Task<IActionResult> GetLessons(
            // [FromQuery] SearchOption searchOption
            [FromQuery] OrderingOption orderingOption,
            [FromQuery] PagingOption pagingOption
        )
        {
            var specification = new SimpleLessonsSpecification(
                    orderingOption.GetOrderByInfos(),
                    pagingOption.CurrentPage, pagingOption.PageSize
                );
            var qro = await _uow.Repository<Lesson>().Find(specification);
            if (qro == null || qro.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<SimpleLessonsResultDTO>(qro));
        }

        // api/lessons/{id:int}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson([FromRoute] int id, [FromBody] PutLessonDTO body)
        {
            var lesson = await _uow.Repository<Lesson>().Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            lesson = _mapper.Map(body, lesson);
            _uow.Repository<Lesson>().Update(lesson);

            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<SimpleLessonDTO>(lesson));
        }

        // api/lessons/{id:int}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson([FromRoute] int id)
        {
            var lesson = await _uow.Repository<Lesson>().Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _uow.Repository<Lesson>().Remove(lesson);
            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}