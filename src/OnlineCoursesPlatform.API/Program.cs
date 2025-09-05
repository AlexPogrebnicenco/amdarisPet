using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnlineCoursesPlatform.API.Common;
using OnlineCoursesPlatform.API.DependencyInjection.Cloudinary;
using OnlineCoursesPlatform.API.DependencyInjection.Swagger;
using OnlineCoursesPlatform.API.Middlewares;
using OnlineCoursesPlatform.Infrastructure.RealTime;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new OnlineCoursesPlatform.API.Converters.DateTimeConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithCustomOptions();


// Register App + Infra Layers
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddLogging();
builder.Services.AddCloudinary(builder.Configuration);


// SignalR
builder.Services.AddSignalR();

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
app.MapHub<NotificationHub>("/notificationHub");

app.Run();

public partial class Program { }
