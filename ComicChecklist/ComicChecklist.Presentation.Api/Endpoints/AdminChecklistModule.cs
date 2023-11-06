using ComicChecklist.Data.Repositories;
using ComicChecklist.Domain.Models;
using ComicChecklist.Presentation.Api.Models;

namespace ComicChecklist.Presentation.Api.Endpoints
{
    public static class AdminChecklistModule
    {
        public static void AddAdminChecklistEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGroup("/admin/checklists")
                .MapAdminChecklistApi()
                .WithTags("Backoffice")
                .WithOpenApi()
                .RequireAuthorization("admin");
        }

        private static RouteGroupBuilder MapAdminChecklistApi(this RouteGroupBuilder group)
        {
            group.MapGet("/", SearchChecklist)
                            .WithName("SearchChecklist")
                            .Produces<ChecklistModel[]>(StatusCodes.Status200OK, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);
            group.MapGet("/{id}", GetChecklist)
                            .WithName("GetChecklist")
                            .Produces<ChecklistModel>(StatusCodes.Status200OK, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);
            group.MapPost("/", CreateChecklist)
                            .WithName("CreateChecklist")
                            .Produces<ChecklistModel>(StatusCodes.Status201Created, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);
            group.MapPut("/{id}", UpdateChecklist)
                            .WithName("UpdateChecklist")
                            .Produces<ChecklistModel>(StatusCodes.Status200OK, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }

        public static async Task<IResult> CreateChecklist(IChecklistRepository repository, CreateChecklistRequest payload)
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

            var checklistModel = new ChecklistModel(checklist.Id,
                                                checklist.Name,
                                                checklist.Issues.Select(x => new IssueModel(x.Id, x.Title)).ToArray());

            return Results.Created($"/admin/checklists/{checklistModel.Id}", checklistModel);
        }

        public static async Task<IResult> GetChecklist(IChecklistRepository repository, int id)
        {
            Checklist checklist = await repository.GetAsync(id);

            if (checklist == null)
            {
                return Results.NotFound();
            }

            var checklistModel = new ChecklistModel(checklist.Id,
                                                checklist.Name,
                                                checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueModel(x.Id, x.Title)).ToArray());

            return Results.Ok(checklistModel);
        }

        public static async Task<IResult> SearchChecklist(IChecklistRepository repository, string? name, int index)
        {
            if (index < 0)
            {
                return Results.BadRequest("Invalid search criteria");
            }

            var checklists = await repository.Search(name, index * 10, 10);

            var checklistsModels = checklists.Select(checklist => new ChecklistModel(checklist.Id,
                                                                                    checklist.Name,
                                                                                    checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueModel(x.Id, x.Title)).ToArray()));
            return Results.Ok(checklistsModels);


        }

        public static async Task<IResult> UpdateChecklist(IChecklistRepository repository, UpdateChecklistRequest request, int id)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.BadRequest("Checklist name is null or empty.");
            }

            var checklist = await repository.GetAsync(id);

            if (checklist != null)
            {
                if (request.Issues.Count() == checklist.Issues.Count)
                {
                    if (request.Issues.All(x => checklist.Issues.Any(y => y.Id == x.Id)))
                    {
                        checklist.Name = request.Name;

                        for (var i = 0; i < request.Issues.Count(); i++)
                        {
                            var issueId = request.Issues[i].Id;
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

                var checklistModel = new ChecklistModel(checklist.Id,
                                               checklist.Name,
                                               checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueModel(x.Id, x.Title)).ToArray());

                return Results.Ok(checklistModel);
            }
            else
            {
                return Results.NotFound();
            }
        }
    }
}