using AddressBook.Api.Infrastructure.AutofacModules;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using NotificationAddressBookApplication;
using Prometheus;
using Serilog;

namespace AddressBook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
      .SetBasePath(env.ContentRootPath)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
      .AddEnvironmentVariables();


            Configuration = builder.Build();

            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
       .ReadFrom.Configuration(configuration)
       .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCustomizedProblemDetails();
            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //JSON Serializer
            //services.AddControllersWithViews().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            //    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
            //    = new DefaultContractResolver());
            services.AddControllers().AddNewtonsoftJson(
                     options => options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local).AddFluentValidation(s =>
                     {
                         s.RegisterValidatorsFromAssemblyContaining<Program>();
                         s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                     });

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AddressBook services.",
                    Description = "AddressBook services are used in AddressBook Project of State Agency of Mandatory Health Insurance. Services are below.",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Contact",
                        Url = new Uri("https://its.gov.az")
                    },
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Example License",
                    //    Url = new Uri("https://example.com/license")
                    //}
                });
            });

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new AutoMapperModule());
            containerBuilder.RegisterModule(new ApplicationModule(Configuration));
            var container = containerBuilder.Build();
            var componentContext = container.Resolve<IComponentContext>();
            services.AddSingleton(componentContext.Resolve<IMediator>());
            services.AddSingleton(componentContext.Resolve<IMapper>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.EnvironmentName == "Local" || env.EnvironmentName == "Dev" || env.EnvironmentName == "Test" || env.EnvironmentName == "Prod")
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = "swagger";
                });
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseHttpsRedirection();
            //app.UseProblemDetails();
            app.UseAuthorization();
            app.UseMetricServer();
            app.UseHttpMetrics();
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("Strict-Transport-Security",
                                     "max-age=31536000;includeSubDomains;preload");

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
