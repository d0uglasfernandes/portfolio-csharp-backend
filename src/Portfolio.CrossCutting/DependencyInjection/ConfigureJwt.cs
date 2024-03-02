using Portfolio.Domain.Dto.AppSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Portfolio.CrossCutting.DependencyInjection
{
    public static class ConfigureJwt
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment environment)
        {
            byte[] key;
            if (environment.IsEnvironment("Testing"))
            {
                key = Encoding.ASCII.GetBytes("95bdebcb4bb748238d6545a181eaf52b");
            }
            else
            {
                var appSettings = configuration.Get<AppSettingsDto>();
                key = Encoding.ASCII.GetBytes(appSettings.Secret);
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            return services;
        }

        public static IApplicationBuilder UseJwtConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
