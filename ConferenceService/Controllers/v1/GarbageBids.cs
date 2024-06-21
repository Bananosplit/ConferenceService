using Asp.Versioning;
using DAL.Context;
using Domain.Entity;
using Domain.interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ConferenceService.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public async Task<IActionResult> GetSubmitedBidsByDate([FromQuery] DateTime date, [FromQuery] bool isSent)
        {
            List<Bid>? bids = null;

            dBContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                bids = await bidRepo.GetBidsByDate(date, isSent);
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
