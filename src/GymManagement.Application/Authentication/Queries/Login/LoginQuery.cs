using ErrorOr;

using MediatR;
using GymManagement.Application.Authentication.Common;

namespace GymManagement.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;