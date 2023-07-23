using System;
using Microsoft.EntityFrameworkCore;
using workoutService.Entities;

namespace workoutService.Services
{
	public class WorkoutService
    {
        private readonly ApplicationDbContext _context;

        public WorkoutService(ApplicationDbContext context)
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

