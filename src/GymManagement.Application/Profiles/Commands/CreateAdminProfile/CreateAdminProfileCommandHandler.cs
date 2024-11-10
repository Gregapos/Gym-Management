using ErrorOr;

using MediatR;

using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;

namespace GymManagement.Application.Profiles.Commands.CreateAdminProfile;

public class CreateAdminProfileCommandHandler(
    IUsersRepository _usersRepository,
    IAdminsRepository _adminsRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateAdminProfileCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        var createAdminProfileResult = user.CreateAdminProfile();
        var admin = new Admin(userId: user.Id, id: createAdminProfileResult.Value);

        await _usersRepository.UpdateAsync(user);
        await _adminsRepository.AddAdminAsync(admin);
        await _unitOfWork.CommitChangesAsync();

        return createAdminProfileResult;
    }
}