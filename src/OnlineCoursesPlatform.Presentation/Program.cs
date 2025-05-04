using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Commands;
using OnlineCoursesPlatform.Application.Queries;
using OnlineCoursesPlatform.Application.Repositories;
using OnlineCoursesPlatform.Domain.Models;
using MediatR;
namespace OnlineCoursesPlatform.Presentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddMediatR(typeof(Program).Assembly);

            services.AddSingleton<IRepository<Course>, InMemoryRepository<Course>>();

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            var createCourseCommand = new CreateCourseCommand("C# Programming", "Learn C# from scratch", "John Doe");
            var createdCourse = await mediator.Send(createCourseCommand);
            Console.WriteLine($"Created Course: {createdCourse.Title}, {createdCourse.Teacher}");

            var getCourseQuery = new GetCourseByIdQuery(createdCourse.Id);
            var course = await mediator.Send(getCourseQuery);
            Console.WriteLine($"Fetched Course: {course.Title}, {course.Description}");
        }
    }
}
