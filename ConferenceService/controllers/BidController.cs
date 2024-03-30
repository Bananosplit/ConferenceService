using ConferenceService.Core;
using ConferenceService.Core.Repositories.Interfaces;
using ConferenceService.Data.Repositories;
using ConferenceService.Models;
using ConferenceService.Utils;
using Microsoft.AspNetCore.Mvc;


namespace ConferenceService.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public BidController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        [HttpPost]
        [Route("/[action]")]
        public async Task<IActionResult> Create([FromBody] BidDto bidDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data is not valid");

            var existBid = await unitOfWork.Bids.Get(bidDto.UserId);

            if (existBid != null)
                return BadRequest("The Bid already EXISTS. You cant create more than one bid");

            var bid = new Bid()
            {
                UserId = bidDto.UserId,
                Name = bidDto?.Name,
                Description = bidDto?.Description ?? "",
                Plan = bidDto?.Plan,
                ActivityTypeId = bidDto.ActivityTypeId,
                SendDate = DateTime.UtcNow,
                IsSent = false
            };

            var result = await unitOfWork.Bids.Create(bid);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpPatch]
        [Route("/[action]")]
        public async Task<IActionResult> Edit([FromBody] BidDto bid)
        {
            if (!ModelState.IsValid)
                return BadRequest("The bid not found");

            var existBid = await unitOfWork.Bids.Get(bid.UserId);

            if (existBid == null)
                return NotFound("The bid not found");

            if (existBid.IsSent == true)
                return BadRequest("You can't edit already sent bid");

            var result = await unitOfWork.Bids.Update(existBid.FillBid(bid));

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpDelete]
        [Route("/[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existBid = await unitOfWork.Bids.Get(id);

            if (existBid is null)
                return BadRequest("The bid not found");

            if (existBid.IsSent == true)
                return BadRequest("You can't delete already sent bid");

            var result = await unitOfWork.Bids.Delete(existBid);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpPost]
        [Route("/[action]")]
        public async Task<IActionResult> SendToCommission(Guid id)
        {
            var existBid = await unitOfWork.Bids.Get(id);

            if (ValidateBid(existBid, out string message) == false)
                return BadRequest(message);

            existBid.IsSent = true;
            existBid.SendDate = DateTime.UtcNow;

            var result = await unitOfWork.Bids.Update(existBid);

            if (result == false)
                return Conflict();

            return Ok();
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<ActionResult<List<string>>> GetActivityList()
        {
            var types = await unitOfWork.Activity.GetAll();

            if (types is null || types.Count == 0)
                return NotFound();

            return Ok(types);
        }

        private static bool ValidateBid(Bid? existBid, out string message)
        {
            message = string.Empty;

            if (existBid is null)
            {
                message = "The bid not found";
                return false;
            }
            else if (existBid.IsSent == true)
            {
                message = "The bid was sent";
                return false;
            }
            else if (string.IsNullOrEmpty(existBid.Plan))
            {
                message = "The plan must be filled";
                return false;
            }
            else if (string.IsNullOrEmpty(existBid.ActivityType.Name))
            {
                message = "The activity type must be filled";
                return false;
            }

            return true;
        }

    }
}
