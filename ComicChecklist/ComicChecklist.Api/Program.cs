using ComicChecklist.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ComicChecklist.Data;

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

ChecklistEndpoints.ConfigureEndpoints(app);

app.Run();
