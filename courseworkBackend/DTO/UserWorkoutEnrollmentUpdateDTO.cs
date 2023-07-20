using System;
namespace courseworkBackend.DTO
{
	public class UserWorkoutEnrollmentUpdateDTO
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompletedDays { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
    }
}

