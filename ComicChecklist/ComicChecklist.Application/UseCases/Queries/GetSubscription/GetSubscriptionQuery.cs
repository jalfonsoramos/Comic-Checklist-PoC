using ComicChecklist.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicChecklist.Application.UseCases.Queries
{
    public record GetSubscriptionQuery(string UserName, int ChecklistId) : IRequest<UseCaseResult<SubscriptionFullDto>>;
}
