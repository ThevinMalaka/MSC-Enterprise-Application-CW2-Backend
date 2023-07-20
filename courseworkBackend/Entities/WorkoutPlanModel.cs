using System;
using courseworkBackend.DTO;

namespace courseworkBackend.Entities
{
	public class WorkoutPlanModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Difficulty { get; set; }
		public int Duration { get; set; }
		public double TotalMET { get; set; }
		public ICollection<WorkoutPlanItemsModel> WorkoutPlanItems { get; set; }
	}
}

