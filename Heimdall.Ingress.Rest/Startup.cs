using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Heimdal.Transport;
using Heimdall.Transport.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Heimdall.Ingress
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IHostedService, MqHostedService>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<AutofacModule>();

            var configuredTransport = new TransportConfigurator(builder, Configuration)
                .WithAgent<RabbitMqConfigurationAgent>()
                .Configure();

            return new AutofacServiceProvider(configuredTransport.BuiltContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
