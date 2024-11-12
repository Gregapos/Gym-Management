using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using GymManagement.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence
{
    public class GymManagementDbContext(
        DbContextOptions options,
        IHttpContextAccessor httpContextAccessor,
        IPublisher _publisher) : DbContext(options), IUnitOfWork
    {
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<Gym> Gyms { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task CommitChangesAsync()
        {
            // get hold of all the domain events
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(entry => entry.Entity.PopDomainEvents())
                .SelectMany(x => x)
                .ToList();

            // store them in the http context for later
            if (IsUserWaitingOnline())
            {
                AddDomainEventsToOfflineProcessingQueue(domainEvents);
            }
            else //for testing purposes
            {
                await PublishDomainEvents(_publisher, domainEvents);
            }


            await base.SaveChangesAsync();
        }

        private static async Task PublishDomainEvents(IPublisher _publisher, List<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }

        private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;

        private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
        {
            // fetch queue from http context or create a new queue if it doesn't exist
            var domainEventsQueue = _httpContextAccessor.HttpContext!.Items
                .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
                    ? existingDomainEvents
                    : new Queue<IDomainEvent>();

            // add the domain events to the end of the queue
            domainEvents.ForEach(domainEventsQueue.Enqueue);

            // store the queue in the http context
            _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}