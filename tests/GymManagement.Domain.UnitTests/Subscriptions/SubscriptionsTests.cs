using ErrorOr;
using FluentAssertions;
using GymManagement.Domain.Subscriptions;
using TestCommon.Gyms;
using TestCommon.Subscriptions;

namespace GymManagement.Domain.UnitTests.Subscriptions;

public class SubscriptionsTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        // Create a subscription
        var subscription = SubscriptionFactory.CreateSubscription();

        // Create the maximum number of gyms + 1
        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid()))
            .ToList();

        // Act
        // Add all various gyms
        var addGymResults = gyms.ConvertAll(subscription.AddGym);

        // Assert
        // Adding all the gyms succeeded, but the last failed
        var allButLastGymResults = addGymResults[..^1];
        allButLastGymResults.Should().AllSatisfy(addGymResult => addGymResult.Value.Should().Be(Result.Success));

        var lastAddGymResult = addGymResults.Last();
        lastAddGymResult.IsError.Should().BeTrue();
        lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows);
    }
}