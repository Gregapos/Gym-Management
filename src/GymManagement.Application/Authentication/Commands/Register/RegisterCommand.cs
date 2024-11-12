using ErrorOr;

using MediatR;
using GymManagement.Application.Authentication.Common;

namespace GymManagement.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;