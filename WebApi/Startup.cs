using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using WebApi.DataServices;
using WebApi.Services;
using WebApi.Settings;

namespace WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            SigningConfigurations signingConfigurations = new SigningConfigurations();
            TokenConfigurations tokenConfigurations = Configuration.GetSection("TokenConfigurations").Get<TokenConfigurations>();
            services.AddSingleton(tokenConfigurations);
            services.AddSingleton(signingConfigurations);

            services.AddScoped<ITokenService, TokenService>();

            services.AddCors();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingConfigurations.Key,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuers = tokenConfigurations.Issuer,
                    ValidateAudience = true,
                    ValidAudiences = tokenConfigurations.Audience,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };
            });

            services.AddDbContext<DataService>(options =>
            {
                options.UseSqlite("Data Source = ./db.db",
                    option => option.MigrationsAssembly("WebApi")
                );
            });
            services.AddScoped<RepositoryTodoImplementation, RepositoryTodo>();
            services.AddScoped<RepositoryUserImplementation, RepositoryUser>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Web Api",
                        Version = "v1",
                        Description = "Web Api",
                        Contact = new OpenApiContact()
                        {
                            Email = "fulviocanducci@hotmail.com",
                            Name = "Fúlvio Cezar Canducci Dias",
                        }
                    });
                var (PathApplication, NameApplication) = GetPlatformServicesValue();
                c.IncludeXmlComments(Path.Combine(PathApplication, $"{NameApplication}.xml"));
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Bearer Token Authentication",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            services.AddControllersWithViews();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static (string PathApplication, string NameApplication) GetPlatformServicesValue()
        {
            return (PlatformServices.Default.Application.ApplicationBasePath, PlatformServices.Default.Application.ApplicationName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarefas");
            });
        }
    }
}
