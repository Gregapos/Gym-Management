using ErrorOr;

using MediatR;

namespace GymManagement.Application.Profiles.ListProfiles;

public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<ListProfilesResult>>;