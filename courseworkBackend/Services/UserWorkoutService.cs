using System;
using courseworkBackend.DataStore;
using courseworkBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace courseworkBackend.Services
{
	public class UserWorkoutService
	{
		private readonly FitnessDbContext _context;

        public UserWorkoutService(FitnessDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserWorkoutModel>> GetWorkoutsAsync()
        {
            return await _context.UserWorkout.ToListAsync();
        }

        public async Task<UserWorkoutModel> GetWorkoutByIdAsync(int id)
        {
            return await _context.UserWorkout.FindAsync(id);
        }

        public async Task<UserWorkoutModel> CreateWorkoutAsync(UserWorkoutModel workout)
        {
            _context.UserWorkout.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<UserWorkoutModel> UpdateWorkoutAsync(UserWorkoutModel workout)
        {
            _context.UserWorkout.Update(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<UserWorkoutModel> DeleteWorkoutAsync(int id)
        {
            var workout = await _context.UserWorkout.FindAsync(id);
            _context.UserWorkout.Remove(workout);
            await _context.SaveChangesAsync();
            return workout;
        }
	}
}

