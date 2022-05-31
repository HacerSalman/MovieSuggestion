using Hangfire;
using Hangfire.MySql.Core;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieSuggestion.Core.Utils;
using MovieSuggestion.Data.Contexts;
using MovieSuggestion.Data.Utils.MovieSuggestion.Core.Utils;
using MovieSuggestion.Job.Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSuggestion.Job
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var connString = EnvironmentVariable.GetConfiguration().DbConnection;
           

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            services.AddHangfire((provider, configuration) =>
            {
                //configuration.UseFilter(provider.GetRequiredService<AutomaticRetryAttribute>());
                configuration.UseStorage(new MySqlStorage(connString + ";Allow User Variables=True", new MySqlStorageOptions
                {
                   
                }));
            });
            services.AddHangfireServer();
            services.AddControllers();
            services.AddDIRegister();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard("/tasks", new DashboardOptions
            {
                DashboardTitle = "Movie Suggestion Hangfire DashBoard", // Dashboard sayfasýna ait Baþlýk alanýný deðiþtiririz.
                AppPath = "/", // Dashboard üzerinden "back to site" button
                //Authorization = new[] { new HangfireDashboardAuthorizationFilter() }, // Güvenlik için Authorization iþlemleri
                Authorization = new[]
               {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = EnvironmentVariable.GetConfiguration().HangfireUserName,
                        Pass = EnvironmentVariable.GetConfiguration().HangfireUserPassword
                    }
                }
            });
           

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
 
            RecurringJobs.TaskOperations();
        }
    }
}
