using Capital.Application.Services;
using Capital.Core.Interfaces.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capital.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDispatchServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatchService, CommandDispatchService>();
            services.AddScoped<IQueryDispatchService, QueryDispatchService>();
        }
    }
}
