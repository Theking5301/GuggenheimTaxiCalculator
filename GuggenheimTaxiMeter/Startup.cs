using GuggenheimTaxiMeter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace GuggenheimTaxiMeter {
    /// <summary>
    /// Startup class.
    /// </summary>
    /// <author>Amine Hosseini</author>
    /// <created>2/11/2019</created>
    /// <modified>2/11/2019</modified>
    public class Startup {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        /// <summary>
        /// JSON configuration file.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Registers the services and ensures the swagger JSON is generated.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) {

            // Enable CORS (this is only used when in the development environment.
            services.AddCors(o => o.AddPolicy("DebugCORSPolicy", builder => {
                builder.WithOrigins("http://localhost:4200", "http://localhost:9876")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();
            }));

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add swagger API documentation generator.
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Guggenheim Taxi API", Version = "v1" });
            });

            // Register cost calculator and database interfaces services.
            services.AddSingleton(typeof(ICostCalculatorService), typeof(CostCalculatorService));
            services.AddSingleton(typeof(IDatabaseInterfaceService), typeof(DatabaseInterfaceService));
        }

        /// <summary>
        /// Configures the HTTP request pipeline and enables the swagger landing page.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseCors("DebugCORSPolicy");
            } else {
                app.UseExceptionHandler("/Error");
            }

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            // Enable swagger UI and point it to the generated swagger JSON.
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Guggenheim Taxi API V1"); ;
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
