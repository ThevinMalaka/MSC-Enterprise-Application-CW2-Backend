using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using courseworkBackend.Entities;
using courseworkBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace courseworkBackend.Controllers
{
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly PredictionService _predictionService;

        public PredictionController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<List<PredictionModel>> Get(int userId)
        {
            //get all predictions from the database
            return await _predictionService.GetPredictionsByUserIdAsync(userId);
        }
    }
}

