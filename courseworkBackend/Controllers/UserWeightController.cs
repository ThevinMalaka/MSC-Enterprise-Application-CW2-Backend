using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using courseworkBackend.DataStore;
using courseworkBackend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace courseworkBackend.Controllers
{
    [Route("api/[controller]")]
    public class UserWeightController : ControllerBase
    {

        private readonly FitnessDbContext _context;

        public UserWeightController(FitnessDbContext context)
        {
            _context = context;
        }

        // GET: api/UserWeight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserWeightModel>> GetUserWeight(int id)
        {
            var userWeight = await _context.UserWeightsLogs.FindAsync(id);

            if (userWeight == null)
            {
                return NotFound();
            }

            return userWeight;
        }

        // PUT: api/UserWeight/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserWeight(int id, UserWeightModel userWeight)
        {
            if (id != userWeight.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userWeight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserWeightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserWeight
        [HttpPost]
        public async Task<ActionResult<UserWeightModel>> PostUserWeight(UserWeightModel userWeight)
        {
            _context.UserWeightsLogs.Add(userWeight);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserWeight), new { id = userWeight.UserId }, userWeight);
        }

        // DELETE: api/UserWeight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserWeight(int id)
        {
            var userWeight = await _context.UserWeightsLogs.FindAsync(id);
            if (userWeight == null)
            {
                return NotFound();
            }

            _context.UserWeightsLogs.Remove(userWeight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserWeightExists(int id)
        {
            return _context.UserWeightsLogs.Any(e => e.UserId == id);
        }







        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

