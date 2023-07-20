using System;
using courseworkBackend.DataStore;
using courseworkBackend.DTO;
using courseworkBackend.Entities;
using Microsoft.EntityFrameworkCore; 

namespace courseworkBackend.Services
{
	public class WorkoutPlanService
	{
		private readonly FitnessDbContext _context;

		public WorkoutPlanService(FitnessDbContext context)
		{
			_context = context;
		}

        public async Task<List<WorkoutPlanDTO>> GetWorkoutPlansAsync()
        {
            //return await _context.WorkoutPlans.ToListAsync();
            var workoutPlanModels = await _context.WorkoutPlans.Include(wp => wp.WorkoutPlanItems).ThenInclude(wpi => wpi.Workout).ToListAsync();
            var workoutPlanDTOs = new List<WorkoutPlanDTO>();

            foreach (var workoutPlanModel in workoutPlanModels)
            {
                var workoutPlanDto = new WorkoutPlanDTO
                {
                    Id = workoutPlanModel.Id,
                    Name = workoutPlanModel.Name,
                    Description = workoutPlanModel.Description,
                    Difficulty = workoutPlanModel.Difficulty,
                    Duration = workoutPlanModel.Duration,
                    TotalMET = workoutPlanModel.TotalMET,
                    WorkoutPlanItems = workoutPlanModel.WorkoutPlanItems.Select(wpi => new WorkoutPlanItemDTO
                    {
                        Id = wpi.Id,
                        WorkoutPlanId = wpi.WorkoutPlanId,
                        Order = wpi.Order,
                        Sets = wpi.Sets,
                        Reps = wpi.Reps,
                        Rest = wpi.Rest,
                        Workout = new WorkoutDTO
                        {
                            Id = wpi.Workout.Id,
                            Name = wpi.Workout.Name,
                            Description = wpi.Workout.Description,
                            Met = wpi.Workout.MET
                        }
                    }).ToList()
                };
                workoutPlanDTOs.Add(workoutPlanDto);
            }

            return workoutPlanDTOs;
        }
        public async Task<List<WorkoutPlanModel>> GetWorkoutPlansWithWorkoutsAsync()
        {
            return await _context.WorkoutPlans.Include(wp => wp.WorkoutPlanItems).ThenInclude(wpi => wpi.Workout).ToListAsync();
            
        }

        public async Task<WorkoutPlanModel> GetWorkoutPlanByIdAsync(int id)
        {
            return await _context.WorkoutPlans.FindAsync(id);
        }
		
		public async Task<WorkoutPlanDTO> GetWorkoutPlanWithWorkoutsByIdAsync(int id)
		{
			var workoutPlanModel = await _context.WorkoutPlans.Include(wp => wp.WorkoutPlanItems).ThenInclude(wpi => wpi.Workout).FirstOrDefaultAsync(wp => wp.Id == id);

			if (workoutPlanModel == null)
			{
				return null; // or handle accordingly
			}

			var workoutPlanDto = new WorkoutPlanDTO
			{
				Id = workoutPlanModel.Id,
				Name = workoutPlanModel.Name,
				Description = workoutPlanModel.Description,
				Difficulty = workoutPlanModel.Difficulty,
				Duration = workoutPlanModel.Duration,
				TotalMET = workoutPlanModel.TotalMET,
				WorkoutPlanItems = workoutPlanModel.WorkoutPlanItems.Select(wpi => new WorkoutPlanItemDTO
				{
					Id = wpi.Id,
					WorkoutPlanId = wpi.WorkoutPlanId,
					Order = wpi.Order,
					Sets = wpi.Sets,
					Reps = wpi.Reps,
					Rest = wpi.Rest,
					Workout = new WorkoutDTO
					{
						Id = wpi.Workout.Id,
						Name = wpi.Workout.Name,
						Description = wpi.Workout.Description,
						Met = wpi.Workout.MET
					}
				}).ToList()
			};

			return workoutPlanDto;
		}

        public async Task<WorkoutPlanDTO> CreateWorkoutPlanAsync(WorkoutPlanModel workoutPlan)
        {
            _context.WorkoutPlans.Add(workoutPlan);
            await _context.SaveChangesAsync();
            return await GetWorkoutPlanWithWorkoutsByIdAsync(workoutPlan.Id); // refetch the created workoutPlan
        }

        public async Task<WorkoutPlanDTO> UpdateWorkoutPlanAsync(WorkoutPlanModel workoutPlan)
        {
            _context.WorkoutPlans.Update(workoutPlan);
            await _context.SaveChangesAsync();
            return await GetWorkoutPlanWithWorkoutsByIdAsync(workoutPlan.Id); // refetch the updated workoutPlan
        }

        public async Task<WorkoutPlanDTO> DeleteWorkoutPlanAsync(int id)
        {
            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);
            var workoutPlanDto = await GetWorkoutPlanWithWorkoutsByIdAsync(id); // fetch the DTO before deleting
            _context.WorkoutPlans.Remove(workoutPlan);
            await _context.SaveChangesAsync();
            return workoutPlanDto; // return the DTO of the deleted item
        }

	}
}

