using ConferenceService.Data;
using ConferenceService.Models;
using ConferenceService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ConferenceService.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly ConferenceServiceDBContext dBContext;

        public BidController(ConferenceServiceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpPost]
        [Route("/[action]")]
        public async Task<IActionResult> Create([FromBody] BidDto bidDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data is not valid");


            var existBid = await dBContext.Bids.FirstOrDefaultAsync(b => b.UserId == bidDto.UserId);

            if (existBid is not null)
                return BadRequest("The Bid already EXISTS. You cant create more than one bid");

            var bid = new Bid()
            {
                UserId = bidDto.UserId,
                Name = bidDto?.Name,
                Description = bidDto?.Description,
                Plan = bidDto?.Plan,
                ActivityTypeId = bidDto.ActivityTypeId,
                SendDate = DateTime.UtcNow
            };

            await dBContext.Bids.AddAsync(bid);
            await dBContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch]
        [Route("/[action]")]
        public async Task<IActionResult> Edit([FromBody] BidDto bid)
        {
            if (!ModelState.IsValid)
                return BadRequest("The bid not found");

            var existBid = await dBContext.Bids.FirstOrDefaultAsync(b => b.UserId == bid.UserId);

            if (existBid == null)
                return BadRequest("The bid not found");

            if (existBid.IsSent == true)
                return BadRequest("You can't edit already sent bid");

            dBContext.Bids.Update(existBid.FillBid(bid));
            await dBContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("/[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            var existBid = dBContext.Bids.FirstOrDefault(b => b.Id == id);

            if (existBid is null)
                return BadRequest("The bid not found");

            if (existBid.IsSent == true)
                return BadRequest("You can't delete already sent bid");

            dBContext.Bids.Remove(existBid);
            await dBContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("/[action]")]
        public async Task<IActionResult> SendToCommission(int id)
        {
            var existBid = await dBContext.Bids.Include(b => b.ActivityType).FirstOrDefaultAsync(b => b.Id == id);

            if (existBid is null)
                return BadRequest("The bid not found");

            if (existBid.IsSent == true)
                return BadRequest("The bid was sent");

            if (string.IsNullOrEmpty(existBid.Plan))
                return BadRequest("The plan must be filled");

            if (string.IsNullOrEmpty(existBid.ActivityType.Name))
                return BadRequest("The activity type must be filled");

            existBid.IsSent = true;
            existBid.SendDate = DateTime.UtcNow;

            dBContext.Bids.Update(existBid);
            await dBContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<ActionResult<List<string>>> GetActivityList()
        {
            var types = await dBContext.ActivityTypes.Select(t => t.Name).ToListAsync();

            if (types is null || types.Count == 0)
                return NotFound();

            return Ok(types);
        }
    }
}
