using ConferenceService.Data;
using ConferenceService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<BidController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var bid = new Bid() 
            {  
               UserId = Guid.NewGuid(),
               Name = "Sergey",
               Description = "some",
               Plan = "some plan",
               ActivityType = ActivityType.Report,
               IsSent = true,
               SendDate = DateTime.UtcNow
            };

            dBContext.Bids.Add(bid);
            dBContext.SaveChanges();

            return new string[] { "value1", "value2" };
        }

        // GET api/<BidController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BidController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BidController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BidController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
