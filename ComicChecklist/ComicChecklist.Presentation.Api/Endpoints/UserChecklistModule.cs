using ComicChecklist.Domain.Dtos;
using MediatR;
using System.Security.Claims;

namespace ComicChecklist.Presentation.Api.Endpoints
{
    public static class UserChecklistModule
    {
        public static void AddUserChecklistEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGroup("/checklists")
                .MapChecklistApi()
                .WithTags("Public")
                .WithOpenApi()
                .RequireAuthorization("admin");
        }

        private static RouteGroupBuilder MapChecklistApi(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAvailableChecklists)
                            .WithName("GetAvailableChecklists")
                            .Produces<ChecklistDto[]>(StatusCodes.Status200OK, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }

        private static async Task<IResult> GetAvailableChecklists(IMediator mediator, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
            //return Results.Ok(user.Identity.Name);
        }
    }
}