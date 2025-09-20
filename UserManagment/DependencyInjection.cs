using Api.UserManagment.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.UserManagment;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(option =>
            option.Filters.Add<ApiExceptionFilterAttribute>());

        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        return services;

    }
}
