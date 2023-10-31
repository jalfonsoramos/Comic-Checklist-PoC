using ComicChecklist.Api.Models;
using ComicChecklist.Data.Repositories;
using ComicChecklist.Domain.Models;

internal static class ChecklistEndpoints
{
    internal static void ConfigureEndpoints(WebApplication app)
    {
        app.MapPost("/checklist", async (IChecklistRepository repository, CreateChecklistDto payload) =>
        {
            if (string.IsNullOrEmpty(payload.Name))
            {
                return Results.BadRequest("Checklist name is null or empty.");
            }

            var checklist = new Checklist()
            {
                Name = payload.Name
            };

            if (payload.Issues != null && payload.Issues.Any())
            {
                var issues = payload.Issues.Select((item, index) => new Issue { Order = index, Title = item.Title });

                foreach (var issue in issues)
                {
                    checklist.Issues.Add(issue);
                }
            }

            repository.Add(checklist);

            await repository.SaveChangesAsync();

            var checklistDto = new ChecklistDto(checklist.Id,
                                                checklist.Name,
                                                checklist.Issues.Select(x => new IssueDto(x.Id, x.Title)).ToArray());

            return Results.Created($"/checklist/{checklistDto.Id}", checklistDto);
        })
        .WithName("CreateChecklist")
        .WithDisplayName("Create Checklist")
        .Produces<ChecklistDto>(StatusCodes.Status201Created, "app/json")
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

            var checklistDto = new ChecklistDto(checklist.Id,
                                                checklist.Name,
                                                checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueDto(x.Id, x.Title)).ToArray());

            return Results.Ok(checklistDto);
        })
        .WithName("GetChecklist")
        .WithDisplayName("Get Checklist")
        .Produces<ChecklistDto>(StatusCodes.Status200OK, "app/json")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        app.MapGet("/checklist", async (IChecklistRepository repository, string name, int index) =>
        {
            if (index < 0)
            {
                return Results.BadRequest("Index is < 0");
            }

            var checklists = await repository.Search(name, index * 10, 10);

            var checklistDtos = checklists.Select(checklist => new ChecklistDto(checklist.Id,
                                                                                checklist.Name,
                                                                                checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueDto(x.Id, x.Title)).ToArray()));

            return Results.Ok(checklistDtos);
        })
        .WithName("SearchChecklist")
        .WithDisplayName("Search Checklist")
        .Produces<ChecklistDto[]>(StatusCodes.Status200OK, "app/json")
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        app.MapPut("/checklist/{id}", async (IChecklistRepository repository, UpdateChecklistDto payload, int id) =>
        {
            if (string.IsNullOrEmpty(payload.Name))
            {
                return Results.BadRequest("Checklist name is null or empty.");
            }

            var checklist = await repository.GetAsync(id);

            if (checklist != null)
            {
                if (payload.Issues.Count() == checklist.Issues.Count)
                {
                    if (payload.Issues.All(x => checklist.Issues.Any(y => y.Id == x.Id)))
                    {
                        checklist.Name = payload.Name;

                        for (var i = 0; i < payload.Issues.Count(); i++)
                        {
                            var issueId = payload.Issues[i].Id;
                            var issue = checklist.Issues.SingleOrDefault(x => x.Id == issueId);

                            if (issue != null)
                            {
                                issue.Order = i;
                            }
                        }
                    }
                    else
                    {
                        return Results.BadRequest("Issue list is invalid: IDs does not match.");
                    }
                }
                else
                {
                    return Results.BadRequest("Issue list is invalid: Issues count mismatch.");
                }

                await repository.SaveChangesAsync();

                return Results.Ok();
            }
            else
            {
                return Results.NotFound();
            }            
        })
        .WithName("UpdateChecklist")
        .WithDisplayName("Update Checklist")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithOpenApi();
    }
}