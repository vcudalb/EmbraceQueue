using Microsoft.Extensions.DependencyInjection;
using EmbraceQueue.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using EmbraceQueue.API.IdentityService.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace EmbraceQueue.API
{
    /// <summary>
    /// Dependecy injection for API
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Customized identity
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomizedIdentity(this IServiceCollection services, IConfiguration configuration, JwtSettings jwtSettings)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.ClaimsIdentity.UserNameClaimType = JwtRegisteredClaimNames.Sub;
                options.ClaimsIdentity.UserIdClaimType = "id";
            })
                .AddEntityFrameworkStores<EmbraceQueueDbContext>()
                .AddDefaultTokenProviders();

            if (jwtSettings.IsEnable)
            {
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey));

                // Get options from app settings
                var jwtAppSettingOptions = configuration.GetSection(nameof(TokenProviderOptions));

                var tokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    // Validate the token expiry
                    ValidateLifetime = true,
                    // If you want to allow a certain amount of clock drift, set that here:
                    ClockSkew = TimeSpan.Zero,
                    // The require expiration
                    RequireExpirationTime = false,
                };

                services.AddSingleton(tokenValidationParameters);

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(options =>
                    {
                        options.ClaimsIssuer = jwtAppSettingOptions[nameof(TokenProviderOptions.Issuer)];
                        options.TokenValidationParameters = tokenValidationParameters;
                        options.SaveToken = true;
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                {
                                    context.Response.Headers.Add("Token-Expired", "true");
                                }
                                return Task.CompletedTask;
                            }
                        };
                    })
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = jwtSettings.CookieName;
                        options.TicketDataFormat = new CustomJwtDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters);
                        options.ExpireTimeSpan = TimeSpan.FromHours(8);
                        options.SlidingExpiration = true;
                    });
            }

            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/swagger");
                x.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.CompletedTask;
                    }

                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
                x.Events.OnRedirectToAccessDenied = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return Task.CompletedTask;
                    }

                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
            });
            return services;
        }

        /// <summary>
        /// Customized data store
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmbraceQueueDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        /// <summary>
        /// UseCustomizedIdentity
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
        {
            app.UseWhen(
                context => context.Request.Path.StartsWithSegments("/api"),
                a => a.Use(async (context, next) =>
                {
                    if (!context.User.Identity.IsAuthenticated)
                    {
                        var principal = new ClaimsPrincipal();

                        var bearerAuthResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                        if (bearerAuthResult?.Principal != null)
                        {
                            principal.AddIdentities(bearerAuthResult.Principal.Identities);
                        }

                        context.User = principal;
                    }

                    await next();
                }));

            app.UseAuthorization();
            return app;
        }
    }
}
