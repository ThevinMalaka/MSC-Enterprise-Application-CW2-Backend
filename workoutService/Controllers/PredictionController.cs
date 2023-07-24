using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workoutService.Entities;
using workoutService.Services;

namespace workoutService.Controllers
{
    [Route("[controller]")]
    public class PredictionController : Controller
    {
        private readonly PredictionService _predictionService;

        public PredictionController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<PredictionModel> Get(int userId)
        {
            //get all predictions from the database
            return await _predictionService.GetPredictionsByUserIdAsync(userId);
        }
    
	}
}

