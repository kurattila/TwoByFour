using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoByFour.MultiplicationTable;
using TwoByFour.Utils;

namespace Szorzotabla
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
            services.AddMvc();

            services.AddSingleton<IRandomWrapper, RandomWrapper>();
            services.AddSingleton<ITrainingCourse, TrainingCourse>();
            services.AddSingleton<IMultiplicationRandomGenerator, MultiplicationRandomGenerator>(); // keeps track of base numbers, e.g. {2,3,4,5}

            services.AddTransient<IMultiplicationSmartFactory, MultiplicationSmartFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IMultiplicationRandomGenerator multiplicationRandomGenerator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            multiplicationRandomGenerator.Init(new[] { 2, 3, 4, 5 });
        }
    }
}
