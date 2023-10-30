using ComicChecklist.Api.Models;
using ComicChecklist.Data.Repositories;
using ComicChecklist.Domain.Models;

internal static class ChecklistEndpoints
{
    internal static void ConfigureEndpoints(WebApplication app)
    {
        app.MapPost("/checklist", async (IChecklistRepository repository, CreateChecklist payload) =>
        {
            if (string.IsNullOrEmpty(payload.Name))
            {
                return Results.BadRequest("Checklist name is null or empty.");
            }

            var checklist = new Checklist()
            {
                Name = payload.Name
            };

            repository.Add(checklist);
            await repository.SaveChangesAsync();

            return Results.Created($"/checklist/{checklist.Id}", checklist);
        })
        .WithName("CreateChecklist")
        .WithDisplayName("Create Checklist")
        .Produces<Checklist>(StatusCodes.Status201Created, "app/json")
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        app.MapGet("/checklist/{id}", async (IChecklistRepository repository, int id) =>
        {
            Checklist checklist = await repository.GetAsync(id);

            if (checklist == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(checklist);
        })
        .WithName("GetChecklist")
        .WithDisplayName("Get Checklist")
        .Produces<Checklist>(StatusCodes.Status200OK, "app/json")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        app.MapGet("/checklist", async (IChecklistRepository repository, string name, int index) =>
        {
            if (index < 0)
            {
                return Results.BadRequest("Index is < 0");
            }

            var checklists = await repository.Search(name, index * 3, 3);

            return Results.Ok(checklists);
        })
        .WithName("SearchChecklist")
        .WithDisplayName("Search Checklist")
        .Produces<IEnumerable<Checklist>>(StatusCodes.Status200OK, "app/json")
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi();
    }
}