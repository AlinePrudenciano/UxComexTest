using AutoMapper;
using UxComexTest.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UxComexTest.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/[Controller]")]
    public class BaseController<TEntity, TModel> : Controller where TEntity : Domain.Entities.BaseEntity where TModel : Models.BaseModel
    {
        private readonly IService<TEntity> _service;
        private readonly IMapper _mapper;

        public BaseController(IMapper mapper,
                              IService<TEntity> service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TModel>>> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(_mapper.Map<List<TModel>>(await _service.Get(cancellationToken)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TModel>> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(_mapper.Map<TModel>(await _service.Get(id, cancellationToken)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
