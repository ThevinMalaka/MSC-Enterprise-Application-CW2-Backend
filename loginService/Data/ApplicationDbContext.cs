//using System;
//namespace loginService.Data
//{
//	public class ApplicationDbContext
//	{
//		public ApplicationDbContext()
//		{
//		}
//	}
//}



using loginService;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}


