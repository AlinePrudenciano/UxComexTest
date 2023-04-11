using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UxComexTest.Api.Models;
using UxComexTest.Domain.Entities;
using UxComexTest.Domain.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UxComexTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController<Address, AddressModel>
    {
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;

        public AddressController(IMapper mapper, IAddressService service) : base(mapper, service)
        {
            _addressService = service;
            _mapper = mapper;
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> Create(int userId, [FromBody]AddressModel address, CancellationToken cancellationToken)
        {
            try
            {
                await _addressService.Add(userId, _mapper.Map<Address>(address), cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }
        
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<AddressModel>>> GetByUser(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _addressService.GetByUser(userId, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<AddressModel>>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }
        
        [HttpGet("cep/{cep}")]
        public async Task<ActionResult<Address>> GetCep(string cep, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _addressService.GetCep(cep, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }


    }
}
