using GymManagement.Api;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Common.Models;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;

namespace GymManagement.Application.SubcutaneousTests.Common;

public class MediatorFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    private SqliteTestDatabase _testDatabase = null!;
    private readonly ICurrentUserProvider _customUserProvider;

    public MediatorFactory()
    {
        _customUserProvider = Substitute.For<ICurrentUserProvider>();
        _customUserProvider.GetCurrentUser().Returns(new CurrentUser(
            Guid.NewGuid(),
            new List<string>(),
            new List<string> { "Admin" }
        ));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _testDatabase = SqliteTestDatabase.CreateAndInitialize();

        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<GymManagementDbContext>>()
                .AddDbContext<GymManagementDbContext>((sp, options) => options.UseSqlite(_testDatabase.Connection));

            services
                .RemoveAll<ICurrentUserProvider>()
                .AddSingleton(_customUserProvider);
        });
    }

    public IMediator CreateMediator()
    {
        var serviceScope = Services.CreateScope();

        _testDatabase.ResetDatabase();

        return serviceScope.ServiceProvider.GetRequiredService<IMediator>();
    }


    public Task InitializeAsync() => Task.CompletedTask;

    public new Task DisposeAsync()
    {
        _testDatabase.Dispose();

        return Task.CompletedTask;
    }
}