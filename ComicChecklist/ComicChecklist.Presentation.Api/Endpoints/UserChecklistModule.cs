﻿using ComicChecklist.Application.UseCases.Commands;
using ComicChecklist.Application.UseCases.Queries;
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
                .RequireAuthorization("public");
        }

        private static RouteGroupBuilder MapChecklistApi(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetChecklistsToSubscribe)
                            .WithName("GetChecklistsToSubscribe")
                            .Produces<ChecklistDto[]>(StatusCodes.Status200OK, "app/json")
                            .Produces(StatusCodes.Status400BadRequest)
                            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/{checklistId}/subscriptions", SubscribeToChecklist)
                .WithName("SubscribeToChecklist")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/subscriptions", GetSubscriptions)
                .WithName("GetSubscriptions")
                .Produces<ChecklistDto[]>(StatusCodes.Status200OK, "app/json")
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/{checklistId}/subscriptions", GetSubscription)
                .WithName("GetSubscription")
                .Produces<SubscriptionFullDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/{checklistId}/subscriptions", UpdateUserIssues)
                .WithName("UpdateUserIssues")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);


            return group;
        }

        private static async Task<IResult> GetChecklistsToSubscribe(IMediator mediator, ClaimsPrincipal user)
        {
            var checkists = await mediator.Send(new GetAvailableChecklistsQuery(user.Identity.Name));
            return Results.Ok(checkists);
        }

        private static async Task<IResult> SubscribeToChecklist(IMediator mediator, ClaimsPrincipal user, int checkListId)
        {
            var result = await mediator.Send(new SubscribeToChecklistCommand(user.Identity.Name, checkListId));
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> GetSubscriptions(IMediator mediator, ClaimsPrincipal user)
        {
            var checklists = await mediator.Send(new GetSubscriptionsQuery(user.Identity.Name));
            return Results.Ok(checklists);
        }

        private static async Task<IResult> GetSubscription(IMediator mediator, ClaimsPrincipal user, int checklistId)
        {
            var result = await mediator.Send(new GetSubscriptionQuery(user.Identity.Name, checklistId));

            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> UpdateUserIssues(IMediator mediator, ClaimsPrincipal user, int checklistId, UserIssueUpdateDto[] issues)
        {
            var result = await mediator.Send(new UpdateUserIssueCommand(user.Identity.Name, checklistId, issues));
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Errors);
        }
    }
}