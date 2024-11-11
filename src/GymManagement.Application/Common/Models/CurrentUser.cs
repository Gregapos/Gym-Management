namespace GymManagement.Application.Common.Models
{
    public record CurrentUser(Guid Id, IReadOnlyList<string> Permissions);
}