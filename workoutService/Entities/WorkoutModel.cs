using System;
namespace workoutService.Entities
{
    public class WorkoutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MET { get; set; }

        public ICollection<WorkoutPlanItemsModel> WorkoutPlanItems { get; set; }
    }
}

