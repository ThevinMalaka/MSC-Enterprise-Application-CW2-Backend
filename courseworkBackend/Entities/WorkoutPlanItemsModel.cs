using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace courseworkBackend.Entities
{
	public class WorkoutPlanItemsModel
	{
		public int Id { get; set; }

		[JsonIgnore]
        public WorkoutPlanModel WorkoutPlan { get; set; }
        
        [ForeignKey("WorkoutPlanModel")]
        public int WorkoutPlanId { get; set; }

        public WorkoutModel Workout { get; set; }

        [ForeignKey("WorkoutModel")]
        public int WorkoutId { get; set; }

        public int Order { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Rest { get; set; }

	}
}

