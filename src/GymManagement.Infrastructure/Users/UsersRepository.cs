using Microsoft.EntityFrameworkCore;

using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Common.Persistence;
using GymManagement.Domain.Users;

namespace GymManagement.Infrastructure.Users;

public class UsersRepository(GymManagementDbContext _dbContext) : IUsersRepository
{
    public async Task AddUserAsync(User user)
    {
        await _dbContext.AddAsync(user);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public Task UpdateAsync(User user)
    {
        _dbContext.Update(user);

        return Task.CompletedTask;
    }
}
