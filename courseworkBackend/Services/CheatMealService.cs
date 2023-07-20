using System;
using courseworkBackend.DataStore;
using courseworkBackend.Entities;

namespace API.Services
{
    public class CheatMealService
    {
        private readonly FitnessDbContext _context;

        public CheatMealService(FitnessDbContext context)
        {
            _context = context;
        }

        public async Task<CheatMealModel> GetCheatMealByIdAsync(int id)
        {
            return await _context.CheatMeals.FindAsync(id);
        }

        public async Task<CheatMealModel> CreateCheatMealAsync(CheatMealModel cheatMeal)
        {
            object value = _context.CheatMeals.Add(cheatMeal);
            await _context.SaveChangesAsync();
            return cheatMeal;
        }

        public async Task<CheatMealModel> UpdateCheatMealAsync(CheatMealModel cheatMeal)
        {
            _context.CheatMeals.Update(cheatMeal);
            await _context.SaveChangesAsync();
            return cheatMeal;
        }

        public async Task<CheatMealModel> DeleteCheatMealAsync(int id)
        {
            var cheatMeal = await _context.CheatMeals.FindAsync(id);
            _context.CheatMeals.Remove(cheatMeal);
            await _context.SaveChangesAsync();
            return cheatMeal;
        }
    }
}