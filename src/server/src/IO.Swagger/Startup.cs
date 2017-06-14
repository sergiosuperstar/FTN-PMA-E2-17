/*
 * Simple TripApp API
 *
 * This is a simple TripApp API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using IO.Swagger.Data;
using IO.Swagger.Models;
using IO.Swagger.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.IO;
using System.Xml.XPath;

namespace IO.Swagger
{
    /// <summary>
    /// Startup class is triggered by environment in order to start web app.
    /// </summary>
    public class Startup
    {
        /// Configuration key for logging section
        public const string LoggingConfigurationSectionKey = "Logging";

        /// Configuration key for application settings section
        public const string AppSettingsConfigurationSectionKey = "AppSettings";

        /// Configuration key for minutes until ticket starts section
        public const string AppSettingsMinutesUntilTicketStartKey = "MinutesUntilTicketStart";

        private readonly IHostingEnvironment _hostingEnv;

        /// <summary>
        /// App configuration.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This is web app startup class constructor.
        /// Use it to set configuration for your web app.
        /// </summary>
        /// <param name="env">Hosting environment instance.</param>
        public Startup(IHostingEnvironment env)
        {
            _hostingEnv = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TripAppContext>(opt => opt.UseInMemoryDatabase());
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddMvc()
                .AddJsonOptions(
                    opts => { opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });

            services.AddSwaggerGen();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                            {
                                Version = "v1",
                                Title = "IO.Swagger",
                                Description = "IO.Swagger (ASP.NET Core 1.0)",
                            }
                        );

                options.DescribeAllEnumsAsStrings();
                options.OperationFilter<ApiKeyOperationFilter>();
                var comments = new XPathDocument($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");
                options.OperationFilter<XmlCommentsOperationFilter>(comments);
                options.ModelFilter<XmlCommentsModelFilter>(comments);
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IApiKeyValidator, SimpleApiKeyValidator>();
            services.AddTransient<ApiKeyAuthenticationOptions>();
            services.AddTransient<ApiKeyOptions>();
            services.AddTransient<IOptions<ApiKeyAuthenticationOptions>, ApiKeyOptions>();
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection(LoggingConfigurationSectionKey));
            loggerFactory.AddDebug();

            TripAppDbInitializer.Seed(app.ApplicationServices);

            app.UseApiKeyAuthentication();
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
