using ABS.FileGenerationAPI.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ABS.FileGenerationAPI.Extensions
{
    public static class CustomJwtAthuExtension
    {
        public static IServiceCollection AddCustomJwtAthuExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found");
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                                ValidateIssuer = false,
                                ValidateAudience = false                                
                            };
                        });

            return services;        

                      
        }
    }
}
