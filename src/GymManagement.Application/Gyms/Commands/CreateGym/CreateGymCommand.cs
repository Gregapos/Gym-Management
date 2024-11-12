using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;
using GymManagement.Application.Common.Authorization;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

[Authorize(Roles = "Admin")]
public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;