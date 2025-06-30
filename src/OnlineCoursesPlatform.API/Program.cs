using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnlineCoursesPlatform.API.Common;
using OnlineCoursesPlatform.API.DependencyInjection.Swagger;
using OnlineCoursesPlatform.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

//builder.Configuration
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
//    .AddEnvironmentVariables();
// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new OnlineCoursesPlatform.API.Converters.DateTimeConverter());
    });
builder.Services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/apsnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithCustomOptions();


// Register App + Infra Layers
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddLogging();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});



// Authentication
builder.Services.AddCustomAuthentication(builder.Configuration);

var app = builder.Build();

//Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("FrontendPolicy");

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

public partial class Program { }
