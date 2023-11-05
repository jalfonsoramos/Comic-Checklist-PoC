using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComicChecklist.Api
{
    public static class TokenGenerator
    {
        // Generates a JWT token with the specified secret key and token expiry time
        public static string GenerateToken(string jwtSecretKey, int tokenExpiryMinutes, string userName, bool isAdmin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecretKey);

            // Configure the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName), // Set the claim with the name of the token user
                    new Claim(ClaimTypes.Role, isAdmin?"admin":"enduser") // Set the claim with the role of the token user
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpiryMinutes), // Set the token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Set the signing credentials for the token
            };

            // Create the JWT token based on the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Write the JWT token as a string
            var generatedToken = tokenHandler.WriteToken(token);

            return generatedToken;
        }

        // Generates a JWT token endpoint result for use in an API controller
        public static IResult GenerateTokenEndpoint(string jwtSecretKey, int tokenExpiryMinutes, string userName, bool isAdmin)
        {
            // Generate a JWT token using the provided secret key and token expiry time
            var token = GenerateToken(jwtSecretKey, tokenExpiryMinutes, userName, isAdmin);

            // Return the generated token as an OK response
            return Results.Ok(token);
        }
    }
}
