using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
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
            builder.Register(c =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(sbc =>
                        sbc.Host("localhost", "", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        })
                    );
                })
                .As<IBusControl>()
                .As<IPublishEndpoint>()
               .As<ISendEndpointProvider>()
                .SingleInstance();

            builder.Populate(services);
            builder.RegisterModule<AutofacModule>();

            var container = builder.Build();
            return new AutofacServiceProvider(container);
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
