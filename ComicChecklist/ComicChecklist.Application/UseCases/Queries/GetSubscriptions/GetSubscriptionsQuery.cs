using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public record GetSubscriptionsQuery(string UserName) : IRequest<SubscriptionSummaryDto[]>;
}
