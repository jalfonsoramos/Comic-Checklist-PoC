using ComicChecklist.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ComicChecklist.Data;
using ComicChecklist.Api;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add EF Core dbcontext
builder.Services.AddDbContext<ComicChecklistDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ComicChecklistDb"));
});

// Add services to the container.
builder.Services.AddTransient<IChecklistRepository, ChecklistRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    // Enable Authorization in swagger page
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthorization(options =>
{
    // Setting two simple role policies for testing
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("enduser", policy => policy.RequireRole("enduser"));
});

var jwtSecretKey = "password123casdsadsaiodiasdsadas";
var tokenExpiryMinutes = 10;

// Added JWT Authentication using the provided secret key
builder.Services.AddJwtAuthentication(jwtSecretKey);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

AdminChecklistEndpoints.ConfigureEndpoints(app);

UserChecklistEndpoints.ConfigureEndpoints(app);

// Simulate login for testing purposes
app.MapPost("/token", (Credentials credentials) =>
{
    if (credentials == null)
    {
        return Results.BadRequest("Invalid credentials");
    }

    if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrEmpty(credentials.Pwd))
    {
        return Results.BadRequest("Invalid credentials");
    }
    
    bool validCredentials = (credentials.UserName.Equals("Admin") && credentials.Pwd.Equals("4dm1n")) ||
                       (credentials.UserName.Equals("User") && credentials.Pwd.Equals("us3r"));

    if (validCredentials)
    {

        bool isAdmin = credentials.UserName.Equals("Admin");
        // Generate JWT token
        return TokenGenerator.GenerateTokenEndpoint(jwtSecretKey, tokenExpiryMinutes, credentials.UserName, isAdmin);
    }
    else
    {
        return Results.BadRequest("Invalid credentials");
    }
}).AllowAnonymous();

app.Run();

public record Credentials(string UserName, string Pwd);
