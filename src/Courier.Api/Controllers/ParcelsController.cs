using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Courier.Core.Commands;
using Courier.Core.Services;

namespace Courier.Api.Controllers
{
    public class ParcelsController : ApiControllerBase
    {
        private readonly ILocationService locationService;

        public ParcelsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet("delivery-available/{address}")]
        public async Task<IActionResult> DeliveryAvailable(string address)
        {
            var dto = await locationService.GetAsync(address);
            if(dto != null)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateParcel command)
        {
            return Ok();
        }
    }
}