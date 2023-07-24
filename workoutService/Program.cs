using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using workoutService.Entities;
using workoutService.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Get Another Service data
builder.Services.AddHttpClient();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Getting var from appsetting.json file
string UserServiceEndpoint = builder.Configuration["UserServiceEndpoint"];


// Get JWT Secret Key from Environment Variable
string jwtSecret = "ThisIsMySuperSecretKeyForFitnessAppInMyMSCourseWork";

// Add JWT Authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "thevinmalaka.com",
            ValidAudience = "thevinmalaka.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });


// Add Services
builder.Services.AddScoped<WorkoutService>();
builder.Services.AddScoped<WorkoutPlanService>();
builder.Services.AddScoped<UserWorkoutEnrollmentService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<PredictionService>();


// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Wrokout MicroService", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {securityScheme, new[] {"Bearer"}}
    };
    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// --------------------------------------------------------
// Seed default data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Workouts.Any())
    {
        dbContext.Workouts.AddRange(
            new WorkoutModel { Id = 1, Name = "Workout 1", Description = "This is workout 1", MET = 4.0 },
            new WorkoutModel { Id = 2, Name = "Workout 2", Description = "This is workout 2", MET = 5.0 },
            new WorkoutModel { Id = 3, Name = "Workout 3", Description = "This is workout 3", MET = 6.0 },
            new WorkoutModel { Id = 4, Name = "Workout 4", Description = "This is workout 4", MET = 7.0 }
        );
    }

    if (!dbContext.WorkoutPlans.Any())
    {
        dbContext.WorkoutPlans.AddRange(
            new WorkoutPlanModel { Id = 1, Name = "Plan 1", Description = "Description 1", Difficulty = "Beginner", Duration = 10, TotalMET = 5.5 },
            new WorkoutPlanModel { Id = 2, Name = "Plan 2", Description = "Description 2", Difficulty = "Intermediate", Duration = 15, TotalMET = 6.5 },
            new WorkoutPlanModel { Id = 3, Name = "Plan 3", Description = "Description 3", Difficulty = "Advanced", Duration = 20, TotalMET = 7.5 },
            new WorkoutPlanModel { Id = 4, Name = "Plan 4", Description = "Description 4", Difficulty = "Expert", Duration = 20, TotalMET = 8.5 }
        );
    }

    if (!dbContext.WorkoutPlanItems.Any())
    {
        dbContext.WorkoutPlanItems.AddRange(
            new WorkoutPlanItemsModel { Id = 1, WorkoutPlanId = 1, WorkoutId = 1, Order = 1, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 2, WorkoutPlanId = 1, WorkoutId = 2, Order = 2, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 3, WorkoutPlanId = 1, WorkoutId = 3, Order = 3, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 4, WorkoutPlanId = 1, WorkoutId = 4, Order = 4, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 5, WorkoutPlanId = 2, WorkoutId = 1, Order = 1, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 6, WorkoutPlanId = 2, WorkoutId = 2, Order = 2, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 7, WorkoutPlanId = 2, WorkoutId = 3, Order = 3, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 8, WorkoutPlanId = 2, WorkoutId = 4, Order = 4, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 9, WorkoutPlanId = 3, WorkoutId = 1, Order = 1, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 10, WorkoutPlanId = 3, WorkoutId = 2, Order = 2, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 11, WorkoutPlanId = 3, WorkoutId = 3, Order = 3, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 12, WorkoutPlanId = 3, WorkoutId = 4, Order = 4, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 13, WorkoutPlanId = 4, WorkoutId = 1, Order = 1, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 14, WorkoutPlanId = 4, WorkoutId = 2, Order = 2, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 15, WorkoutPlanId = 4, WorkoutId = 3, Order = 3, Sets = 3, Reps = 10, Rest = 60 },
            new WorkoutPlanItemsModel { Id = 16, WorkoutPlanId = 4, WorkoutId = 4, Order = 4, Sets = 3, Reps = 10, Rest = 60 }
        );
    }

    dbContext.SaveChanges();
}
// --------------------------------------------------------

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication(); // Use JWT Authentication

app.UseAuthorization();

app.MapControllers();


app.Run();

