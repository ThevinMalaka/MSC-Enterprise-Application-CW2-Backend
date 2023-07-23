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
    private readonly string _userServiceEndpoint; // Getting var from appsetting.json file

    public WeatherForecastController(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _context = context;
        _clientFactory = clientFactory;
        _userServiceEndpoint = configuration["UserServiceEndpoint"]; // Getting var from appsetting.json file
    }

    [HttpGet]
    public async Task<IActionResult> GetUserWeatherForecasts()
    {

        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync(_userServiceEndpoint);

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

