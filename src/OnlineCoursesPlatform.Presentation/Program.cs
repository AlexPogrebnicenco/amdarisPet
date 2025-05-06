using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesPlatform.Application.Features.Users.Commands;
using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Repositories;

namespace OnlineCoursesPlatform.Presentation
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Создаем контейнер зависимостей
            var services = new ServiceCollection();

            // Регистрируем все необходимые сервисы и обработчики MediatR
            services.AddMediatR(typeof(CreateUserHandler).Assembly); // Регистрация обработчиков из текущей сборки

            // Регистрируем репозитории и сервисы
            services.AddSingleton<IRepository<User>, InMemoryRepository<User>>();
            services.AddSingleton<UserService>();

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetService<IMediator>();

            var createUserCommand = new CreateUser("Alex Pogreb", "alex@pogreb.com");
            if (mediator != null)
            {
                var userDto = await mediator.Send(createUserCommand);
                Console.WriteLine($"Created User: {userDto.Name}, Email: {userDto.Email}");
            }
            else
            {
                Console.WriteLine("Mediator is not initialized.");
            }
        }
    }
}