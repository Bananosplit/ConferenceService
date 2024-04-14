using DAL.Data;
using DAL.Entity;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ConferenceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarbageBids : ControllerBase
    {
        private readonly IBidRepository bidRepo;
        private readonly ConferenceServiceDBContext dBContext;

        public GarbageBids(IBidRepository bidRepo, ConferenceServiceDBContext dBContext)
        {
            this.bidRepo = bidRepo;
            this.dBContext = dBContext;
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetSubmitedBidsByDate([FromQuery] DateTime date)
        {
            List<Bid>? bids = null;

            dBContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                bids = await bidRepo.GetBidsByDate(date, true);
                dBContext.Database.CommitTransaction();
            }
            catch
            {
                dBContext.Database.RollbackTransaction();
            }

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
            List<Bid>? bids = null;

            dBContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                bids = await bidRepo.GetBidsByDate(date);
                dBContext.Database.CommitTransaction();
            }
            catch
            {
                dBContext.Database.RollbackTransaction();
            }
            

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
            var bid = await bidRepo.Get(userID);

            if (bid is null || bid.IsSent == true)
                return BadRequest("Bids is no found");

            return Ok(bid);
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetBidById([FromQuery] int id)
        {
            var bid = await bidRepo.Get(id);

            if (bid is null)
                return BadRequest("Bids is no found");

            return Ok(bid);
        }
    }
}
