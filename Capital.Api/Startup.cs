using Capital.Api.Extensions;
using Capital.Api.Helpers;
using Capital.Application.Extensions;
using Capital.Application.Validators;
using Capital.Core.Interfaces.Common;
using Capital.Core.Interfaces.Infrastructure;
using Capital.Infrastructure;
using Capital.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Capital.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
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
            services.AddSingleton<IObjectModelValidator, NullObjectModelValidator>();
            services.AddDbContext<ToDoDbContext>(context => { context.UseInMemoryDatabase("ToDoDatabase"); });
            services.AddScoped<IArgumentValidator, ArgumentValidator>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddCqrsHandlers();
            services.AddDispatchServices();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(GlobalExceptionMiddleware)) ;

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API");
            });

            app.UseRouting();

            app.UseCors("CapitalPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
