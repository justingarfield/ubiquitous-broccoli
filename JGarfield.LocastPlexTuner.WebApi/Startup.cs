using JGarfield.LocastPlexTuner.Library.Clients;
using JGarfield.LocastPlexTuner.Library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JGarfield.LocastPlexTuner.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient()
                    .AddHttpClientLogging(_configuration)
                    .AddSingleton<IIpInfoClient, IpInfoClient>()
                    .AddSingleton<ILocastClient, LocastClient>()
                    .AddSingleton<IFccClient, FccClient>()
                    .AddSingleton<IChannelsM3UService, ChannelsM3UService>()
                    .AddSingleton<IEpg2XmlService, Epg2XmlService>()
                    .AddSingleton<IFccService, FccService>()
                    .AddSingleton<IInitializationService, InitializationService>()
                    .AddSingleton<IIpInfoService, IpInfoService>()
                    .AddSingleton<ILocastService, LocastService>()
                    .AddSingleton<IStationsService, StationsService>()
                    .AddSingleton<ITunerService, TunerService>()
                    .AddHostedService<LocastHostedService>()
                    .AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<RequestLoggingMiddleware>();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
