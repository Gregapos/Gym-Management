using ErrorOr;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscriptionCommand;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Guid>>
{
    public Task<ErrorOr<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        ErrorOr<Guid> result = Guid.NewGuid();
        return Task.FromResult(result);
    }
}