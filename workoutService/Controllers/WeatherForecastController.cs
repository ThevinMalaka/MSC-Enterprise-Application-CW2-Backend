using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace workoutService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IHttpClientFactory _clientFactory;
    private readonly string _workoutServiceEndpoint; // Getting var from appsetting.json file

    public WeatherForecastController(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _context = context;
        _clientFactory = clientFactory;
        _workoutServiceEndpoint = configuration["WorkoutServiceEndpoint"]; // Getting var from appsetting.json file
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    //public IEnumerable<WeatherForecast> Get()
    //{
    //    var rng = new Random();
    //    var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    {
    //        Id = Guid.NewGuid().ToString(),  // Generate a new GUID and convert to string
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = rng.Next(-20, 55),
    //        Summary = Summaries[rng.Next(Summaries.Length)]
    //    })
    //    .ToArray();

    //    // Save forecasts to database
    //    _context.WeatherForecasts.AddRange(forecasts);
    //    _context.SaveChanges();

    //    return forecasts;
    //}

    //[HttpGet("AllData")]
    //public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetAllData()
    //{
    //    var data = await _context.WeatherForecasts.Select(wf => wf.Summary).ToListAsync();
    //    return Ok(data);
    //}

    [HttpGet]
    public async Task<IActionResult> GetUserWeatherForecasts()
    {

        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync(_workoutServiceEndpoint);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using JsonDocument document = await JsonDocument.ParseAsync(responseStream, options);
            var root = document.RootElement;

            // Now you can work with the JsonElement, although it might be a little unwieldy.
            // This example just converts it back to a string.
            return Ok(root.GetRawText());
        }
        else
        {
            return BadRequest("Could not retrieve workouts.");
        }
    }
}

