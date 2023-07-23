using Microsoft.EntityFrameworkCore;
using workoutService;
using workoutService.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public DbSet<WorkoutModel> Workouts { get; set; }
    public DbSet<WorkoutPlanModel> WorkoutPlans { get; set; }
    public DbSet<WorkoutPlanItemsModel> WorkoutPlanItems { get; set; }
    public DbSet<UserWorkoutEnrollmentModel> UserWorkoutEnrollments { get; set; }

}