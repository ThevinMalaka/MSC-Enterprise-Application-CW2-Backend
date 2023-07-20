using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.MapGet("/", () => "Hello World");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
await app.UseOcelot();

app.Run();





