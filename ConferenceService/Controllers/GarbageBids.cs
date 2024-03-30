using ConferenceService.Core;
using ConferenceService.Core.Repositories.Interfaces;
using ConferenceService.Data;
using ConferenceService.Data.Repositories;
using ConferenceService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ConferenceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarbageBids : ControllerBase
    {
        private readonly IUnitOfWork unitOfwork;

        public GarbageBids(IUnitOfWork unitOfwork) =>
            this.unitOfwork = unitOfwork;

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetSubmitedBidsByDate([FromQuery] DateTime date)
        {
            List<Bid>? bids = null;

            unitOfwork.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                bids = await unitOfwork.Bids.GetBidsByDate(date, true);
                unitOfwork.Commit();
            }
            catch
            {
                unitOfwork.RollBack();
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

            unitOfwork.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                bids = await unitOfwork.Bids.GetBidsByDate(date);
                unitOfwork.Commit();
            }
            catch
            {
                unitOfwork.RollBack();
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
            var bid = await unitOfwork.Bids.Get(userID);

            if (bid is null || bid.IsSent == true)
                return BadRequest("Bids is no found");

            return Ok(bid);
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<IActionResult> GetBidById([FromQuery] int id)
        {
            var bid = await unitOfwork.Bids.Get(id);

            if (bid is null)
                return BadRequest("Bids is no found");

            return Ok(bid);
        }
    }
}
