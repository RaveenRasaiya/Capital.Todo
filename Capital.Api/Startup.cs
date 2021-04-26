using Capital.Api.Extensions;
using Capital.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddCorsPolicy();
            services.AddDatabase(Configuration);
            services.AddCommonServices();
            services.AddCqrsHandlers();
            services.AddDispatchServices();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddClientAppServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddlewares(env);
            app.UseClientAppFiles();
            app.UseHttps();
            app.UseSwaggerServices();
            app.UseRouting();
            app.UseCorsPolicy();
            app.UseAuthorization();
            app.UseControllsMapping();
        }
    }
}
