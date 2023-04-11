using AutoMapper;
using UxComexTest.Api.Models;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UxComexTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User, UserModel>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService service) : base(mapper, service)
        {
            _userService = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create([FromBody] UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.Add(_mapper.Map<User>(user), cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
