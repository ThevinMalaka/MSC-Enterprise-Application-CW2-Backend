using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseworkBackend.Entities
{
	public class UserWorkoutModel
	{
        public int Id { get; set; }

        public WorkoutModel Workout { get; set; }
        public UserModel User { get; set; }

        [ForeignKey("WorkoutModel")]
        public int WorkoutId { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }

        public DateOnly Date { get; set; }

        public int? Duration { get; set; }

        public int? Count { get; set; }

        public int? CaloriesBurned { get; set; }
	}
}

