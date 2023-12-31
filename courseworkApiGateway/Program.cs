﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);


// Add CORS services.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
    builder =>
    {
        builder.WithOrigins(
            "http://localhost:3000",
            "https://fitnessappweb.z13.web.core.windows.net"
            ) // React Web app running URL
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



