using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ComicChecklist.Api.Secure
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string jwtSecretKey)
        {
            // Add JWT Bearer Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Configure token validation parameters
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Validate the signature of the token
                        ValidateIssuerSigningKey = true,
                        // Set the secret key used to validate the token's signature
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretKey)),
                        // Skip issuer validation (optional)
                        ValidateIssuer = false,
                        // Skip audience validation (optional)
                        ValidateAudience = false,
                        // Validate the token's expiration time
                        ValidateLifetime = true,
                        // Set the tolerance for validating the token's expiration time
                        ClockSkew = TimeSpan.Zero,
                        // Require the token to have an expiration time
                        RequireExpirationTime = true,
                        LifetimeValidator = (before, expires, token, parameters) =>
                        {
                            var tokenLifetimeMinutes = (expires - before)?.TotalMinutes;
                            return tokenLifetimeMinutes <= 10; // Set the maximum token lifetime to 10 minutes
                        }
                    };
                });

            return services;
        }
    }
}
