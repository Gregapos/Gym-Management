using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins.Events;
using MediatR;

namespace GymManagement.Application.Subscriptions.Events;

public class SubscriptionDeletedEventHandler(
    ISubscriptionsRepository subscriptionsRepository,
    IUnitOfWork unitOfWork)
    : INotificationHandler<SubscriptionDeletedEvent>
{
    private readonly ISubscriptionsRepository _subscriptionRepository = subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(notification.SubscriptionId)
            ?? throw new InvalidOperationException();

        await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();
    }
}
