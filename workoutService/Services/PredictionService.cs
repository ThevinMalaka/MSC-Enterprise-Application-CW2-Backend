using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using workoutService.DTO;
using workoutService.Entities;

namespace workoutService.Services
{
	public class PredictionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _userServiceEndpoint; // Getting var from appsetting.json file

        public PredictionService(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _context = context;
            _clientFactory = clientFactory;
            _userServiceEndpoint = configuration["UserServiceEndpoint"]; // Getting var from appsetting.json file
        }

        //create a new prediction
        public async Task<PredictionModel> CreatePredictionAsync(int userId)
        {


            Console.WriteLine($"DDDDDDDD ----- user service userId---- {userId}");
            //get current workout plan
            //var currentUserEnrollment = await _context.UserWorkoutEnrollments.Where(uwe => uwe.UserId == userId).OrderByDescending(uwe => uwe.Id).FirstOrDefaultAsync();
            var currentUserEnrollment = await _context.UserWorkoutEnrollments
                                            .Include(uwe => uwe.WorkoutPlan) // Include WorkoutPlan in the query
                                            .Where(uwe => uwe.UserId == userId)
                                            .OrderByDescending(uwe => uwe.Id)
                                            .FirstOrDefaultAsync();
            Console.WriteLine($"DDDDDDDD 11111 ----- currentUserEnrollment--1-- {currentUserEnrollment.CompletedDays}");
            Console.WriteLine($"DDDDDDDD 11111 ----- currentUserEnrollment---2- {currentUserEnrollment.Days}");
            Console.WriteLine($"DDDDDDDD 11111 ----- currentUserEnrollment---3- {currentUserEnrollment.Date}");
            Console.WriteLine($"DDDDDDDD 11111 ----- currentUserEnrollment---4 Duration- {currentUserEnrollment.WorkoutPlan.Duration}");
            Console.WriteLine($"DDDDDDDD 11111 ----- currentUserEnrollment---4 TotalMET- {currentUserEnrollment.WorkoutPlan.TotalMET}");
            Console.WriteLine($"DDDDDDDD 11111 ----- currentUserEnrollment---5- {currentUserEnrollment.WorkoutPlanId}");
            Console.WriteLine($"DDDDDDDD 11111 ----- currentUserEnrollment---5- {currentUserEnrollment.WorkoutPlanId}");
            if (currentUserEnrollment == null)
            {
                throw new Exception("No UserWorkoutEnrollment found for the given userId");
            }

            var currentWorkoutPlan = currentUserEnrollment.WorkoutPlan;
            if (currentWorkoutPlan == null)
            {
                throw new Exception("WorkoutPlan is null in the current UserWorkoutEnrollment");
            }

            var currentWorkoutPlanMET = currentWorkoutPlan.TotalMET;
            if (currentWorkoutPlanMET == null)
            {
                throw new Exception("TotalMET is null in the current WorkoutPlan");
            }

            //get his latest weight 
            //var currentWeight = await _context.UserWeightsLogs.Where(uw => uw.UserId == userId).OrderByDescending(uw => uw.Id).FirstOrDefaultAsync();
            // --------------------------------------------------------
            // --------------------------------------------------------
            // --------------------------------------------------------
            // Call the USER SERIVCE through API GATEWAY url ----------
            string requestEndpoint = string.Format(_userServiceEndpoint, userId);
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(requestEndpoint);

            double currentWeight = 0;

            Console.WriteLine($"AAAAAAA----------- {currentWeight}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"BBBBBBBBB-----responseString-> {responseString}");
                var latestWeightData = JsonSerializer.Deserialize<UserWeightDto>(responseString);
                Console.WriteLine($"BBBBBBBBB-----Weight-> {latestWeightData.Weight}");
                Console.WriteLine($"BBBBBBBBB-----UserId-> {latestWeightData.UserId}");
                Console.WriteLine($"BBBBBBBBB-----Date-> {latestWeightData.Date}");
                Console.WriteLine($"BBBBBBBBB----Id--> {latestWeightData.Id}");
                currentWeight = latestWeightData.Weight;
            }

            Console.WriteLine($"CCCCCCCCCCC------> {currentWeight}");
            // Call the USER SERIVCE through API GATEWAY url
            // --------------------------------------------------------
            // --------------------------------------------------------
            // --------------------------------------------------------
            if (currentWeight == null)
            {
                throw new Exception("No UserWeightsLog found for the given userId");
            }

            var currentWeightValue = currentWeight;
            if (currentWeightValue == null)
            {
                throw new Exception("Weight is null in the current UserWeightsLog");
            }

            //get workout days
            var workoutDays = currentUserEnrollment.WorkoutPlan.Duration;

            // get prediction date
            var predictionDate = DateTime.Now.AddDays(workoutDays);

            var avgDurationForWorkout = 30;

            Console.WriteLine($"EEEEEE-----currentWorkoutPlanMET-> {currentWorkoutPlanMET}");
            // calculate calories burned
            var caloriesBurned = (currentWorkoutPlanMET * currentWeightValue * 3.5 * (avgDurationForWorkout * workoutDays)) / 200;
            Console.WriteLine($"DDDDDDDDD------> {caloriesBurned}");

            // calculate weight loss
            var weightLoss = caloriesBurned / 7700;
            Console.WriteLine($"EEEEEE-----weightLoss-> {weightLoss}");

            // average calories gain
            var avgCaloriesGain = 1600;

            // average weight gain
            var avgWeightGain = (avgCaloriesGain * workoutDays) / 7700;
            Console.WriteLine($"EEEEEE-----avgWeightGain-> {avgWeightGain}");

            // calculate predicted weight
            var predictedWeight = currentWeightValue + avgWeightGain - weightLoss; // in kg
            Console.WriteLine($"EEEEEE------> {predictedWeight}");

            // create new prediction
            var newPrediction = new PredictionModel
            {
                UserId = userId,
                Date = predictionDate,
                Weight = predictedWeight
            };

            _context.Predictions.Add(newPrediction);
            await _context.SaveChangesAsync();

            return newPrediction;
        }

        //get all predictions by user id
        public async Task<PredictionModel> GetPredictionsByUserIdAsync(int id)
        {
            //return await _context.Predictions.Where(p => p.UserId == id).OrderByDescending(p => p.Id).ToListAsync();
            return await _context.Predictions
                                .Where(p => p.UserId == id)
                                .OrderByDescending(p => p.Id)
                                .FirstOrDefaultAsync();
        }

    }
}

