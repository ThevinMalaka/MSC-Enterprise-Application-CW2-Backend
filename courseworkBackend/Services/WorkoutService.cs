using System;
using courseworkBackend.DataStore;
using courseworkBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace courseworkBackend.Services
{
	public class WorkoutService
	{
		private readonly FitnessDbContext _context;

        public WorkoutService(FitnessDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutModel>> GetWorkoutsAsync()
        {
            return await _context.Workouts.ToListAsync();
        }

        public async Task<WorkoutModel> GetWorkoutByIdAsync(int id)
        {
            return await _context.Workouts.FindAsync(id);
        }

        public async Task<WorkoutModel> CreateWorkoutAsync(WorkoutModel workout)
        {
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<WorkoutModel> UpdateWorkoutAsync(WorkoutModel workout)
        {
            _context.Workouts.Update(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<WorkoutModel> DeleteWorkoutAsync(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return workout;
        }
	}
}

