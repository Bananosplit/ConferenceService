using App.BidServices;
using Asp.Versioning;
using ConferenceService.Models;
using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;


namespace ConferenceService.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly BidServices bidService;

        public BidController(BidServices bidRepo) =>
            bidService = bidRepo;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BidDto bidDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data is not valid");

            var bid = new Bid()
            {
                Id = bidDto.UserId,
                Name = bidDto?.Name,
                Description = bidDto?.Description ?? "",
                Plan = bidDto?.Plan,
                ActivityType = (ActivityType)bidDto.ActivityTypeId,
                SendDate = DateTime.UtcNow,
                IsSent = false
            };

            var result = await bidService.Create(bid);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit([FromBody] BidDto bidDto, Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest("The bid not found");

            var bid = new Bid()
            {
                Id = bidDto.UserId,
                Name = bidDto?.Name,
                Description = bidDto?.Description ?? "",
                Plan = bidDto?.Plan,
                ActivityType = (ActivityType)bidDto.ActivityTypeId,
                SendDate = DateTime.UtcNow,
                IsSent = false
            };

            var result = await bidService.Update(bid);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await bidService.Delete(id);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SendToCommission(Guid id)
        {
            await bidService.SendToCommission(id);

            return Ok();
        }

        [HttpGet]
        public List<string> GetActivityList()
        {
            return bidService.GetActivityType();
        }
    }
}
