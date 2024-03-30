using ConferenceService.Core.Repositories.Interfaces;
using ConferenceService.Data;
using ConferenceService.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarbageBids : ControllerBase
    {
        private readonly IBidRepository bidRepository;

        public GarbageBids(IBidRepository bidRepository) =>
            this.bidRepository = bidRepository;

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetSubmitedBidsByDate([FromQuery] DateTime date)
        {

            var bids = await bidRepository.GetBidsByDate(date, true);

            if (bids is null)
                return BadRequest("Bids is no found");

            if (bids.Count == 0)
                return Ok("no bids by date");

            return Ok(bids);
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetNoSubmitedBidsByDate([FromQuery] DateTime date)
        {
            var bids = await bidRepository.GetBidsByDate(date);

            if (bids is null)
                return BadRequest("Bids is no found");

            if (bids.Count == 0)
                return Ok("no bids by date");

            return Ok(bids);
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetNoSubmitedBidByUserId([FromQuery] Guid userID)
        {
            var bid = await bidRepository.Get(userID);

            if (bid is null || bid.IsSent == true)
                return BadRequest("Bids is no found");

            return Ok(bid);
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetBidById([FromQuery] int id)
        {
            var bid = await bidRepository.Get(id);

            if (bid is null)
                return BadRequest("Bids is no found");

            return Ok(bid);
        }
    }
}
