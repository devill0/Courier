using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Courier.Core.Commands;
using Courier.Core.Services;
using Courier.Api.Framework;

namespace Courier.Api.Controllers
{
    public class ParcelsController : ApiControllerBase
    {
        private readonly IParcelService parcelService;
        private readonly ICommandDispatcher commandDispatcher;

        public ParcelsController(IParcelService parcelService, ICommandDispatcher commandDispatcher)
        {
            this.parcelService = parcelService;
            this.commandDispatcher = commandDispatcher;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var parcel = await parcelService.GetAsync(id);
            if (parcel == null)
            {
                return NotFound();
            }

            return Ok(parcel);
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {            
            return Ok(await parcelService.BrowseAsync());
        }

        [HttpGet("delivery-available/{address}")]
        public async Task<IActionResult> DeliveryAvailable(string address)
        {
            var deliveryAvailable = await parcelService.DeliveryAvailableAsync(address);
            if(deliveryAvailable)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateParcel command)
        {
            await commandDispatcher.DispatchAsync(command);

            return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
        }
    }
}