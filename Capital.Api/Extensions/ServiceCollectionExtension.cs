using Capital.Application.Services;
using Capital.Application.Validators;
using Capital.Core.Interfaces.Common;
using Capital.Core.Interfaces.Infrastructure;
using Capital.Infrastructure;
using Capital.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Capital.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDispatchServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatchService, CommandDispatchService>();
            services.AddScoped<IQueryDispatchService, QueryDispatchService>();
        }

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CapitalPolicy",
                    builder =>
                    {
                        builder.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("Settings:DatabaseName");
            services.AddDbContext<ToDoDbContext>(context => { context.UseInMemoryDatabase(databaseName); });
        }


        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IArgumentValidator, ArgumentValidator>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
        }

        public static void AddClientAppServices(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
        }
    }
}
