using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workoutService.DTO;
using workoutService.Entities;
using workoutService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace workoutService.Controllers
{
    [Route("[controller]")]
    public class WorkoutPlanController : Controller
    {
        private readonly WorkoutPlanService _workoutPlanService;

        public WorkoutPlanController(ApplicationDbContext context)
        {
            _workoutPlanService = new WorkoutPlanService(context);
        }


        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<List<WorkoutPlanDTO>>> GetWorkoutPlansAsync()
        {
            return await _workoutPlanService.GetWorkoutPlansAsync();
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<WorkoutPlanDTO>> GetWorkoutPlanByIdAsync(int id)
        {
            return await _workoutPlanService.GetWorkoutPlanWithWorkoutsByIdAsync(id);
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<WorkoutPlanDTO>> CreateWorkoutPlanAsync(WorkoutPlanModel workoutPlan)
        {
            return await _workoutPlanService.CreateWorkoutPlanAsync(workoutPlan);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<ActionResult<WorkoutPlanDTO>> UpdateWorkoutPlanAsync(int id, WorkoutPlanModel workoutPlan)
        {
            if (id != workoutPlan.Id)
            {
                return BadRequest();
            }

            return await _workoutPlanService.UpdateWorkoutPlanAsync(workoutPlan);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<ActionResult<WorkoutPlanDTO>> DeleteWorkoutPlanAsync(int id)
        {
            return await _workoutPlanService.DeleteWorkoutPlanAsync(id);
        }

    }
}

