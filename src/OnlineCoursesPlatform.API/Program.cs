using MediatR;
using OnlineCoursesPlatform.API.Middleware;
using OnlineCoursesPlatform.Application;
using OnlineCoursesPlatform.Infrastructure;
using OnlineCoursesPlatform.Application.Features.Users.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/apsnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register App + Infra Layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddLogging();


var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestTimingMiddleware>();

app.UseAuthorization();
app.MapControllers();
app.Run();
