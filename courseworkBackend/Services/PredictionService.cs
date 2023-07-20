using System;
using courseworkBackend.DataStore;
using courseworkBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace courseworkBackend.Services
{
	public class PredictionService
	{
        private readonly FitnessDbContext _context;

        public PredictionService(FitnessDbContext context)
        {
            _context = context;
        }

        //create a new prediction
        public async Task<PredictionModel> CreatePredictionAsync(int userId)
        {

                //get current workout plan
                var currentUserEnrollment = await _context.UserWorkoutEnrollments.Where(uwe => uwe.UserId == userId).OrderByDescending(uwe => uwe.Id).FirstOrDefaultAsync();
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
                var currentWeight = await _context.UserWeightsLogs.Where(uw => uw.UserId == userId).OrderByDescending(uw => uw.Id).FirstOrDefaultAsync();
                if (currentWeight == null)
                {
                    throw new Exception("No UserWeightsLog found for the given userId");
                }

                var currentWeightValue = currentWeight.Weight;
                if (currentWeightValue == null)
                {
                    throw new Exception("Weight is null in the current UserWeightsLog");
                }

                //get workout days
                var workoutDays = currentUserEnrollment.Days;

                // get prediction date
                var predictionDate = DateTime.Now.AddDays(workoutDays);

                var avgDurationForWorkout = 20;

                // calculate calories burned
                var caloriesBurned = (currentWorkoutPlanMET * currentWeightValue * 3.5 * (avgDurationForWorkout * workoutDays)) / 200;

                // calculate weight loss
                var weightLoss = caloriesBurned / 7700;

                // average calories gain
                var avgCaloriesGain = 2000;

                // average weight gain
                var avgWeightGain = (avgCaloriesGain * workoutDays) / 7700;

                // calculate predicted weight
                var predictedWeight = currentWeightValue + avgWeightGain - weightLoss; // in kg

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
        public async Task<List<PredictionModel>> GetPredictionsByUserIdAsync(int id)
        {
            return await _context.Predictions.Where(p => p.UserId == id).OrderByDescending(p => p.Id).ToListAsync();
        }

    }
}

