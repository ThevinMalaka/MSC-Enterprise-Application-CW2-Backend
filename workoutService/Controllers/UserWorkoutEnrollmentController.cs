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
    public class UserWorkoutEnrollmentController : Controller
    {
        private readonly UserWorkoutEnrollmentService _userWorkoutEnrollmentService;
        //private readonly PredictionService _predictionService;

        public UserWorkoutEnrollmentController(UserWorkoutEnrollmentService userWorkoutEnrollmentService)
        {
            _userWorkoutEnrollmentService = userWorkoutEnrollmentService;
            //_predictionService = predictionService;
        }


        [HttpGet("user/{userId}")]
        //[Authorize]
        public async Task<List<UserWorkoutEnrollmentModel>> Get(int userId)
        {
            //get all user workout enrollments from the database
            return await _userWorkoutEnrollmentService.GetUserWorkoutEnrollmentsByUserIdAsync(userId);
        }

        [HttpGet("single/{id}")]
        //[Authorize]
        public async Task<UserWorkoutEnrollmentModel> GetEnrollmentById(int id)
        {
            //get all user workout enrollments from the database
            return await _userWorkoutEnrollmentService.GetUserWorkoutEnrollmentByIdAsync(id);
        }

        //add a new user workout enrollment
        [HttpPost("single")]
        //[Authorize]
        public async Task<ActionResult<UserWorkoutEnrollmentModel>> Post([FromBody] UserWorkoutEnrollmentCreateDTO userWorkoutEnrollment)
        {
            //    //create a new user workout enrollment
            //    var result = await _userWorkoutEnrollmentService.CreateUserWorkoutEnrollmentAsync(userWorkoutEnrollment);
            //    return result; 

            //create a new user workout enrollment
            var result = await _userWorkoutEnrollmentService.CreateUserWorkoutEnrollmentAsync(userWorkoutEnrollment);
            var userId = userWorkoutEnrollment.UserId;
            // print out the user id
            Console.WriteLine("User ID:--------------------- " + userId);

            //create a new prediction
            //var predictionResult = await _predictionService.CreatePredictionAsync(userId);
            return result;

        }

        //update a user workout enrollment
        [HttpPut("single")]
        //[Authorize]
        public async Task<ActionResult<UserWorkoutEnrollmentModel>> Put(UserWorkoutEnrollmentUpdateDTO userWorkoutEnrollment)
        {
            //update a user workout enrollment
            var result = await _userWorkoutEnrollmentService.UpdateUserWorkoutEnrollmentAsync(userWorkoutEnrollment);
            return result;

        }

        //complete single day workout
        [HttpPut("complete/{id}")]
        //[Authorize]
        public async Task<ActionResult<UserWorkoutEnrollmentModel>> CompleteDay(int id)
        {
            //complete a single day workout
            var result = await _userWorkoutEnrollmentService.CompleteDayAsync(id);
            return result;

        }
    }
}

