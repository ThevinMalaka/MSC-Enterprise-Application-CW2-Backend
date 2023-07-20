using System;
namespace courseworkBackend.DTO
{
	public class ReportDTO
	{
        public DateTime Date { get; set; }
        public int WorkoutPlanId { get; set; }
        public string? WorkoutPlanName { get; set; }
        public DateTime StartDate { get; set; }
        public int CompletedDays { get; set; }
        public int Days { get; set; }
        public double TotalCaloriesBurnt { get; set; }
    }
}

