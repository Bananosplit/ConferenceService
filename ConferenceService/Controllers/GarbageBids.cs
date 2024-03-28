using ConferenceService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarbageBids : ControllerBase
    {
        private readonly ConferenceServiceDBContext dBContext;

        public GarbageBids(ConferenceServiceDBContext dBContext) => 
            this.dBContext = dBContext;

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetSubmitedBidsByDate([FromQuery]DateTime date)
        {
            var bids = await dBContext.Bids
                .Where(b => b.IsSent == true)
                .Where(b => b.SendDate > date)
                .ToListAsync();

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
            var bids = await dBContext.Bids
                .Where(b => b.SendDate > date)
                .ToListAsync();

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
            var bid = await dBContext.Bids.FirstOrDefaultAsync(b => b.UserId == userID);

            if (bid is null)
                return BadRequest("Bids is no found");

            return Ok(bid);
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetBidById([FromQuery] int id)
        {
            var bid = await dBContext.Bids.FirstOrDefaultAsync(b => b.Id == id);

            if (bid is null)
                return BadRequest("Bids is no found");

            return Ok(bid);
        }
    }
}
