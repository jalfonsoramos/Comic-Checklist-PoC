using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Application.UseCases.Commands;
using ComicChecklist.Infra.Data;
using ComicChecklist.Infra.Data.Repositories;
using ComicChecklist.Presentation.Api.Endpoints;
using ComicChecklist.Presentation.Api.Filters;
using ComicChecklist.Presentation.Api.Secure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core dbcontext
builder.Services.AddDbContext<ComicChecklistDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ComicChecklistDb"));
});

// Add services to the container.
builder.Services.AddTransient<IChecklistRepository, ChecklistRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserChecklistRepository, UserChecklistRepository>();
builder.Services.AddTransient<IUserIssuesRepository, UserIssuesRepository>();

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

    opt.SchemaFilter<EnumSchemaFilter>();
});

builder.Services.AddAuthorization(options =>
{
    // Setting two simple role policies for testing
    options.AddPolicy("private", policy => policy.RequireRole("role_admin"));
    options.AddPolicy("public", policy => policy.RequireRole("role_user"));
});

// Added JWT Authentication using the provided secret key
builder.Services.AddJwtAuthentication(Settings.JwtSecretKey);

// Add support for Enums in minimal api
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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
