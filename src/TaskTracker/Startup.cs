using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.IO;
using System.Text.Json.Serialization;
using TaskTracker.Data;
using System.Collections.Generic;
using System.Collections;

namespace TaskTracker
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="webHostEnvironment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
                    {
                        options.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskTracker", Version = "v1" });

                        string xmldocPath = Path.Combine(AppContext.BaseDirectory, "TaskTracker.xml");
                        options.IncludeXmlComments(xmldocPath, true);
                    })
                    .AddDbContext<TaskTrackerContext>(options => options.UseSqlServer(GetConnectionString()))
                    .AddControllers()
                    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="webHostEnvironment"></param>
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            applicationBuilder.UseSwagger()
                              .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskTracker v1"))
                              .UseRouting()
                              .UseAuthorization()
                              .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private string GetConnectionString() =>
            WebHostEnvironment.IsDevelopment() ?
            Configuration["ConnectionStrings:TaskTrackerDb"] :
            string.Format(Configuration["ConnectionStrings:TaskTrackerDb"],
                          Environment.GetEnvironmentVariable("DB_HOSTNAME"),
                          Environment.GetEnvironmentVariable("DB_HOSTPORT"),
                          Environment.GetEnvironmentVariable("DB_LOGIN"),
                          Environment.GetEnvironmentVariable("DB_PASSWORD"));
    }
}
