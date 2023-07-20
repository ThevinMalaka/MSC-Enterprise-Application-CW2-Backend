using System;
namespace courseworkBackend.DTO
{
    public class WorkoutUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MET { get; set; }
    }
}

