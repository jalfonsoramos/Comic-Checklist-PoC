using ComicChecklist.Application.Commands;
using ComicChecklist.Domain.Interfaces.Repositories;
using ComicChecklist.Infra.Data;
using ComicChecklist.Infra.Data.Repositories;
using ComicChecklist.Presentation.Api.Endpoints;
using ComicChecklist.Presentation.Api.Secure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core dbcontext
builder.Services.AddDbContext<ComicChecklistDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ComicChecklistDb"));
});

// Add services to the container.
builder.Services.AddTransient<IChecklistRepository, ChecklistRepository>();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateChecklistCommand).Assembly));

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

// Added JWT Authentication using the provided secret key
builder.Services.AddJwtAuthentication(Settings.JwtSecretKey);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddLoginEndpoints();
app.AddAdminChecklistEndpoints();
app.AddUserChecklistEndpoints();

app.Run();
