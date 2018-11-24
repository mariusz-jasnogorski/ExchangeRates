using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

using ExchangeRates.Repositories.Interfaces;
using ExchangeRates.Repositories.Implementations;
using ExchangeRates.Services.Interfaces;
using ExchangeRates.Services.Implementations;
using ExchangeRates.API.Middleware;


namespace ExchangeRates.API
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

            services.AddScoped<IExchangeRateRepository, FixerExchangeRateRepository>();
            services.AddScoped<IExchangeRatesService, ExchangeRatesService>();

            ConfigureServicesSwagger(services);
        }

        private void ConfigureServicesSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Exchange Rates",
                    Description = "Exchange Rates Web API",
                    TermsOfService = "None"
                });

                c.DescribeAllEnumsAsStrings();
                c.IgnoreObsoleteActions();
                c.IgnoreObsoleteProperties();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            ConfigureSwagger(app);

            app.UseMvc();
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
            c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exchange Rates API");
            });
        }
    }
}
