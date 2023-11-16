using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Application.UseCases.Commands;
using ComicChecklist.Application.UseCases.Queries;
using ComicChecklist.Domain.Dtos;
using ComicChecklist.Presentation.Api.ExtensionMethods;
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

        public static async Task<IResult> UpdateChecklist(IMediator mediator, UpdateChecklistRequest request, int id)
        {           
            var result = await mediator.Send(new UpdateChecklistCommand(request.ToChecklistDto(id)));

            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
        }
    }
}