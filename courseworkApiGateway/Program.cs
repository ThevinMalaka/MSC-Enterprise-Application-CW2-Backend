using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddOcelot(builder.Configuration);

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

//app.UseHttpsRedirection();

//app.MapWhen(
//    context => context.Request.Path.Value.IndexOf("swagger", StringComparison.CurrentCultureIgnoreCase) >= 0,
//    appBuilder => { appBuilder.UseRouting(); }
//);

//app.UseOcelot().Wait();

//app.Run();


// ---------------------

//var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Services.AddOcelot(builder.Configuration);

//var app = builder.Build();
//app.MapGet("/", () => "Hello World");

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
//await app.UseOcelot();

//app.Run();





var builder = WebApplication.CreateBuilder(args);


// Add CORS services.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
    builder =>
    {
        builder.WithOrigins("http://localhost:3000") // React Web app running URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add authentication services
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySuperSecretKeyForFitnessAppInMyMSCourseWork"))
        };
    });

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowMyOrigin"); // Use the CORS policy

app.UseAuthentication(); // Use authentication middleware

app.UseHttpsRedirection();

app.UseAuthorization();

await app.UseOcelot();

app.Run();



