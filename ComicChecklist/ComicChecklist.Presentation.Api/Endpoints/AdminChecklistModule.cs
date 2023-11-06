using ComicChecklist.Application.Commands;
using ComicChecklist.Application.Queries;
using ComicChecklist.Domain.Dtos;
using ComicChecklist.Domain.Interfaces.Repositories;
using ComicChecklist.Presentation.Api.Models;
using MediatR;

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
                            .Produces<ChecklistDto[]>(StatusCodes.Status200OK, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);
            group.MapGet("/{id}", GetChecklistById)
                            .WithName("GetChecklistById")
                            .Produces<ChecklistDto>(StatusCodes.Status200OK, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);
            group.MapPost("/", CreateChecklist)
                            .WithName("CreateChecklist")
                            .Produces<ChecklistDto>(StatusCodes.Status201Created, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);
            group.MapPut("/{id}", UpdateChecklist)
                            .WithName("UpdateChecklist")
                            .Produces<ChecklistDto>(StatusCodes.Status200OK, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }

        public static async Task<IResult> CreateChecklist(IMediator mediator, CreateChecklistRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.BadRequest("Checklist name is null or empty.");
            }

            if (request.Issues == null || request.Issues.Count() == 0)
            {
                return Results.BadRequest("Checklist issues array is null or empty.");
            }

            if (request.Issues.Any(x => string.IsNullOrEmpty(x)))
            {
                return Results.BadRequest("Checklist issues array contains a null or empty item.");
            }

            var checklistDto = await mediator.Send(new CreateChecklistCommand(request.ToChecklistDto()));

            return Results.Created($"/admin/checklists/{checklistDto.Id}", checklistDto);
        }

        public static async Task<IResult> GetChecklistById(IMediator mediator, int id)
        {
            var checklist = await mediator.Send(new GetChecklistByIdQuery(id));
            return checklist != null ? Results.Ok(checklist) : Results.NotFound();
        }

        public static async Task<IResult> SearchChecklist(IMediator mediator, string name, int index)
        {
            if (index < 0)
            {
                return Results.BadRequest("Invalid search criteria");
            }
            var checkists = await mediator.Send(new SearchChecklistQuery(name, index));
            return checkists != null ? Results.Ok(checkists) : Results.Ok(Enumerable.Empty<ChecklistDto>);
        }

        public static async Task<IResult> UpdateChecklist(IChecklistRepository repository, UpdateChecklistRequest request, int id)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.BadRequest("Checklist name is null or empty.");
            }

            var checklist = await repository.GetByIdAsync(id);

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

                var checklistModel = new ChecklistDto(checklist.Id,
                                               checklist.Name,
                                               checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueDto(x.Id, x.Title)).ToArray());

                return Results.Ok(checklistModel);
            }
            else
            {
                return Results.NotFound();
            }
        }
    }

    public static class RequestExtensions
    {
        public static ChecklistDto ToChecklistDto(this CreateChecklistRequest request)
        {
            return new ChecklistDto(0, request.Name, request.Issues.Select(issue => new IssueDto(0, issue)).ToArray());
        }
    }
}