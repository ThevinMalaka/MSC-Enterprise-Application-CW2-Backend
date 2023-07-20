using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using courseworkBackend.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace courseworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheatMealController : Controller
    {
        private readonly CheatMealService _cheatMealService;

        public CheatMealController(CheatMealService cheatMealService)
        {
            _cheatMealService = cheatMealService;
        }

        // GET: api/CheatMeals/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCheatMealById(int id)
        {
            var cheatMeal = await _cheatMealService.GetCheatMealByIdAsync(id);

            if (cheatMeal == null)
            {
                return NotFound();
            }

            return Ok(cheatMeal);
        }

        // POST: api/CheatMeals
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CheatMealModel>> CreateCheatMeal(CheatMealModel cheatMeal)
        {
            var createdCheatMeal = await _cheatMealService.CreateCheatMealAsync(cheatMeal);
            return CreatedAtAction(nameof(GetCheatMealById), new { id = createdCheatMeal.Id }, createdCheatMeal);
        }

        // PUT: api/CheatMeals/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCheatMeal(int id, CheatMealModel cheatMeal)
        {
            if (id != cheatMeal.Id)
            {
                return BadRequest();
            }

            await _cheatMealService.UpdateCheatMealAsync(cheatMeal);
            return NoContent();
        }

        // DELETE: api/CheatMeals/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCheatMeal(int id)
        {
            var cheatMeal = await _cheatMealService.DeleteCheatMealAsync(id);
            if (cheatMeal == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}

