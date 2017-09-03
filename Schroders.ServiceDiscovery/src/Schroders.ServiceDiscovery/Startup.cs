using System;
using System.Diagnostics;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Schroders.ServiceBase.Commands;
using Schroders.ServiceBase.Commands.Pipeline;
using Schroders.ServiceBase.Commands.Pipeline.PipelineFactory;
using Schroders.ServiceBase.Commands.PipelineActions;
using Schroders.ServiceDiscovery.Commands;

using Schroders.ServiceDiscovery.DataContracts.Requests;
using Schroders.ServiceDiscovery.DataContracts.Responses;
using Schroders.ServiceDiscovery.Repository;
using Serilog;

namespace Schroders.ServiceDiscovery
{
    using DataContracts.Configurations;

    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.Configuration)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<ServicesConfiguration>(Configuration.GetSection(nameof(ServicesConfiguration)));
            services.AddSingleton<IConfiguration>(Configuration);


            // Add framework services.
            services.AddMvc();

            // Swagger services.
            services.AddSwaggerGen();

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register Interceptors
            builder.RegisterGeneric(typeof(TracePipelineAction<,>)).AsImplementedInterfaces();

            // Register Command
            builder.RegisterType<GetAvailableServicesCommand>().As<ICommand<GetAvailableServicesRequest, GetServicesResponse>>();
            builder.RegisterType<GetServiceUrlCommand>().As<ICommand<GetServiceUrlRequest, GetServiceUrlResponse>>();

            builder.RegisterGeneric(typeof(Pipeline<,,>)).AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PipelineFactory>().As<IPipelineFactory>().SingleInstance();

            builder.RegisterType<ServiceRepository>().As<IServiceRepository>().SingleInstance();

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            var @switch = new SourceSwitch("TraceSwitch")
            {
                Level = SourceLevels.Verbose
            };
            loggerFactory.AddTraceSource(@switch, new TextWriterTraceListener(writer: Console.Out));

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
