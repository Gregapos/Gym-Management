using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;
using GymManagement.Application.Common.Authorization;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

[Authorize(Permissions = "gyms:create")]
public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;