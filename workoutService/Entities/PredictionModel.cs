using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace workoutService.Entities
{
	public class PredictionModel
	{
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public int UserId { get; set; }
    }
}

