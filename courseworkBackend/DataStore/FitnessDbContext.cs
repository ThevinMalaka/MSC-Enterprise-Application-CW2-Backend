using System;
using courseworkBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace courseworkBackend.DataStore
{
	public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
        : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<WorkoutPlanModel> WorkoutPlans { get; set; }
        public DbSet<CheatMealModel> CheatMeals { get; set; }
        public DbSet<WorkoutModel> Workouts { get; set; }
        public DbSet<UserWeightModel> UserWeightsLogs { get; set; }
        public DbSet<UserWorkoutEnrollmentModel> UserWorkoutEnrollments { get; set; }
        public DbSet<UserWorkoutModel> UserWorkout { get; set; }
        public DbSet<PredictionModel> Predictions { get; set; }
        public DbSet<WorkoutPlanItemsModel> WorkoutPlanItems { get; set; }
        
    }
}

