using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using OnlineCoursesPlatform.API.Common;
using OnlineCoursesPlatform.API.Middlewares;
using OnlineCoursesPlatform.API.DependencyInjection.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using OnlineCoursesPlatform.API.DependencyInjection.Authentication;


var builder = WebApplication.CreateBuilder(args);

//builder.Configuration
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
//    .AddEnvironmentVariables();
// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/apsnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithCustomOptions();


// Register App + Infra Layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddLogging();


// Authentication
builder.Services.AddCustomAuthentication(builder.Configuration);

var app = builder.Build();

//Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestTimingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
