using ComicChecklist.Presentation.Api.Models;
using ComicChecklist.Presentation.Api.Secure;

namespace ComicChecklist.Presentation.Api.Endpoints
{
    public static class LoginModule
    {
        public static void AddLoginEndpoints(this IEndpointRouteBuilder app)
        {
            // Simulate login for testing purposes
            app.MapPost("/token", (CredentialsDto credentials) =>
            {
                if (credentials == null)
                {
                    return Results.BadRequest("Invalid credentials");
                }

                if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrEmpty(credentials.Pwd))
                {
                    return Results.BadRequest("Invalid credentials");
                }

                bool validCredentials = credentials.UserName.Equals("Admin") && credentials.Pwd.Equals("4dm1n") ||
                                   credentials.UserName.Equals("User") && credentials.Pwd.Equals("us3r");

                if (validCredentials)
                {

                    bool isAdmin = credentials.UserName.Equals("Admin");
                    // Generate JWT token
                    return TokenGenerator.GenerateTokenEndpoint(Settings.JwtSecretKey, Settings.TokenExpiryMinutes, credentials.UserName, isAdmin);
                }
                else
                {
                    return Results.BadRequest("Invalid credentials");
                }
            })
            .WithTags("Login")
            .AllowAnonymous();
        }
    }
}