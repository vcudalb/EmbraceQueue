using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmbraceQueue.API.IdentityService.JwtBearer;
using EmbraceQueue.API.IdentityService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.IO;
using System;
using EmbraceQueue.API.SwaggerConfigurations;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using EmbraceQueue.Application;
using EmbraceQueue.Infrastructure;
using Microsoft.EntityFrameworkCore;
using EmbraceQueue.API.IdentityService;

namespace EmbraceQueue.API
{
    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Gets the Jwt Settings.
        /// </summary>
        /// <value>The hosting environment.</value>
        private readonly JwtSettings _jwtSettings;

        /// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <value>The configuration.</value>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            // Setup Configuration
            _configuration = configuration;

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);

            _jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Configure service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.Configure<JwtSettings>(_configuration.GetSection("JwtSettings"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCustomizedDataStore(_configuration);

            // Identity Service
            services.AddScoped<IIdentityService, IdentityService.IdentityService>();
            services.AddScoped<IWorkContext, WorkContext>();

            //Injecting Application and Infrastructure layers.
            services.AddApplication();
            services.AddInfrastructure(_configuration);

            services.AddSingleton(_jwtSettings);

            services.AddCustomizedIdentity(_configuration, _jwtSettings);

            services.AddHttpClient();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddCors();

            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = ApiVersion.Parse("1");

                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'V";
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
                options.IncludeXmlComments(apiXmlPath);

                options.AddSecurityDefinition(
                    "Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });

                // This filter removes version from parameters, without it we will have version as parameter 
                // for all endpoints in swagger UI
                options.OperationFilter<RemoveVersionOperationFilter>();

                // This filter replaces v{version:apiVersion} with actual version of corresponding swagger doc.
                options.DocumentFilter<ReplaceVersionDocumentFilter>();
            });
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                setupAction.RoutePrefix = "";
            });

            // Configuration Auth Jwt Token
            ConfigureAuth(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            // Enable global cors policy!
            app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );

            app.UseCustomizedIdentity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
