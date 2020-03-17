using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controllers]")]
    public abstract class ApiControllerBase<TController> : ControllerBase where TController : ControllerBase
    {
        protected readonly ILogger<TController> _logger;

        protected readonly IMapper _mapper;

        // protected readonly IUnitOfWork _uow;

        public ApiControllerBase(ILogger<TController> logger, IMapper mapper) //, IUnitOfWork uow)
        {
            _logger = logger;
            _mapper = mapper;
            // _uow = uow;
        }
    }
}