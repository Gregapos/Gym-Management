using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscriptionCommand;

public record CreateSubscriptionCommand(
   string SubscriptionType,
   Guid AdminId
) : IRequest<ErrorOr<Subscription>>;