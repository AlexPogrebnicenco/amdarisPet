using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Application.Features.Users.Queries;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddApplication();
        services.AddInfrastructure(); 

        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();
        var unitOfWork = provider.GetRequiredService<IUnitOfWork>();

        var result = await mediator.Send(new CreateUser("vasea", "ggg@example.com"));
        await unitOfWork.SaveAsync();

        var user = await unitOfWork.UserRepository.GetByEmailAsync("ggg@example.com");

        var users = await mediator.Send(new GetAllUsers(pageNumber: 1, pageSize: 20));

        Console.WriteLine("\n All Users:");
        foreach (var u in users)
        {
            Console.WriteLine($"• {u.Id}: {u.UserName} - {u.Email}");
        }

        Console.WriteLine(user != null
            ? $"User saved in DB: {user.UserName} ({user.Email})"
            : " User NOT saved!");

        // GET
        var alex = await mediator.Send(new GetUserById(1));
        Console.WriteLine(alex != null ? $"Found: {alex.UserName}" : "Not found");

        // UPDATE
        var updated = await mediator.Send(new UpdateUser(1, "alex", "alex@email.com"));
        Console.WriteLine(updated != null ? $"Updated: {updated.UserName}" : "Update failed");

        //DELETE
       var deleted = await mediator.Send(new DeleteUser(3));
        Console.WriteLine(deleted ? "Deleted successfully" : "Not found for delete");
    }
}
