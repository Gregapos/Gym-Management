using ErrorOr;

using MediatR;

using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Application.Profiles.ListProfiles;

public class ListProfilesQueryHandler(IUsersRepository _usersRepository) : IRequestHandler<ListProfilesQuery, ErrorOr<ListProfilesResult>>
{
    public async Task<ErrorOr<ListProfilesResult>> Handle(ListProfilesQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        return new ListProfilesResult(user.AdminId, user.ParticipantId, user.TrainerId);
    }
}