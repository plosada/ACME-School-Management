using Application.ServicesContracts;
using Domain.Courses;
using Domain.Students;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        //services.AddSingleton<IPaymentService, PaymentService>();
        services.AddSingleton<ICourseRepository, CourseRepository>();
        services.AddSingleton<IStudentRepository, StudentRepository>();

        return services;
    }
}
