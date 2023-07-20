//using System;
//namespace workoutService.Data
//{
//	public class ApplicationDbContext
//	{
//		public ApplicationDbContext()
//		{
//		}
//	}
//}



using Microsoft.EntityFrameworkCore;
using workoutService;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}