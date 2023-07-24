using System;
using System.Text.Json.Serialization;

namespace workoutService.DTO
{
    //public class UserWeightDto
    //{
    //       public int Id { get; set; }
    //       public DateTime Date { get; set; }
    //       public double Weight { get; set; }
    //       public int UserId { get; set; }
    //   }
    public class UserWeightDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}

