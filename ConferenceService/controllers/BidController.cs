using App.Utils;
using ConferenceService.Models;
using ConferenceService.Utils;
using DAL.Entity;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ConferenceService.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepository bidRepo;

        public BidController(IBidRepository bidRepo) => this.bidRepo = bidRepo;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BidDto bidDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data is not valid");

            var existBid = await bidRepo.Get(bidDto.UserId);

            if (existBid != null)
                return BadRequest("The Bid already EXISTS. You cant create more than one bid");

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

            var result = await bidRepo.Create(bid);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit([FromBody] BidDto bid, Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest("The bid not found");

            await Console.Out.WriteLineAsync(id.ToString());

            var existBid = await bidRepo.Get(bid.UserId);

            if (existBid == null)
                return NotFound("The bid not found");

            if (existBid.IsSent == true)
                return BadRequest("You can't edit already sent bid");

            var result = await bidRepo.Update(existBid.FillBid(bid));

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existBid = await bidRepo.Get(id);

            if (existBid is null)
                return BadRequest("The bid not found");

            if (existBid.IsSent == true)
                return BadRequest("You can't delete already sent bid");

            var result = await bidRepo.Delete(existBid);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SendToCommission(Guid id)
        {
            Bid? existBid = await bidRepo.Get(id);

            if (ServiceValidation.ValidateBid(existBid, out string message) == false)
                return BadRequest(message);

            existBid.IsSent = true;
            existBid.SendDate = DateTime.UtcNow;

            var result = await bidRepo.Update(existBid);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetActivityList()
        {
            var types = await bidRepo.GetActivityType();

            return Ok(types);
        }
    }
}
