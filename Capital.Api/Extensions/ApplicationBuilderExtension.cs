using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capital.Api.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(GlobalExceptionMiddleware));
        }

        public static void UseSwaggerServices(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API");
            });
        }

        public static void UseHttps(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
        }

        public static void UseClientAppFiles(this IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
        }


        public static void UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors("CapitalPolicy");
        }

        public static void UseControllsMapping(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


    }
}
