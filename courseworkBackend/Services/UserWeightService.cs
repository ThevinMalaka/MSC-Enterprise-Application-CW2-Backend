using System;
using courseworkBackend.DataStore;
using courseworkBackend.DTO;
using courseworkBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace courseworkBackend.Services
{
	public class UserWeightService
	{
		
        private readonly FitnessDbContext _context;

        public UserWeightService(FitnessDbContext context)
        {
            _context = context;
        }

		// Get all weight logs
        public async Task<List<UserWeightModel>> GetWeightLogsByUserIdAsync(int id)
        {
            return await _context.UserWeightsLogs.Where(wl => wl.UserId == id).ToListAsync();
        }
        
		// Get a weight log by ID
        public async Task<UserWeightModel> GetWeightLogByIdAsync(int id)
        {
            return await _context.UserWeightsLogs.FindAsync(id);
        }

		// Create a weight log
        public async Task<UserWeightModel> CreateWeightLogAsync(WeightLogCreationDTO weightLog)
        {
            // object value = _context.UserWeightsLogs.Add(weightLog);
            // await _context.SaveChangesAsync();
            // return weightLog;
			var newWeightLog = new UserWeightModel
            {
                UserId = weightLog.UserId,
                Weight = weightLog.Weight,
                Date = weightLog.Date
            };

            object value = _context.UserWeightsLogs.Add(newWeightLog);
            await _context.SaveChangesAsync();
            return newWeightLog;
        }

		// Update a weight log
        public async Task<UserWeightModel> UpdateWeightLogAsync(UserWeightModel weightLog)
        {
            _context.UserWeightsLogs.Update(weightLog);
            await _context.SaveChangesAsync();
            return weightLog;
        }

		// Delete a weight log
        public async Task<UserWeightModel> DeleteWeightLogAsync(int id)
        {
            var weightLog = await _context.UserWeightsLogs.FindAsync(id);
            _context.UserWeightsLogs.Remove(weightLog);
            await _context.SaveChangesAsync();
            return weightLog;
        }
	}
}

