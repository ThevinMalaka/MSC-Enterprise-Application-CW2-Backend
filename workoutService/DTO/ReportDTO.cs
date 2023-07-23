using System;
namespace workoutService.DTO
{
	public class ReportDTO
    {
        public DateTime Date { get; set; }

        //workout plan
        public int WorkoutPlanId { get; set; }
        //plan name
        public string? WorkoutPlanName { get; set; }
        //StartDate
        public DateTime StartDate { get; set; }

        //No of Days CompleteDay
        public int CompletedDays { get; set; }

        //No of Days to be completed
        public int Days { get; set; }
        //total Calories Burnt
        public double TotalCaloriesBurnt { get; set; }


    }
}

