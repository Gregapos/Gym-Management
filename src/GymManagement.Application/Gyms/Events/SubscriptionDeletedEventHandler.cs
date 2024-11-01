using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins.Events;
using MediatR;

namespace GymManagement.Application.Gyms.Events;

public class SubscriptionDeletedEventHandler(
    IGymsRepository gymsRepository,
    IUnitOfWork unitOfWork)
    : INotificationHandler<SubscriptionDeletedEvent>
{
    private readonly IGymsRepository _gymsRepository = gymsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
    {
        var gyms = await _gymsRepository.ListBySubscriptionIdAsync(notification.SubscriptionId);

        await _gymsRepository.RemoveRangeAsync(gyms);
        await _unitOfWork.CommitChangesAsync();
    }
}
