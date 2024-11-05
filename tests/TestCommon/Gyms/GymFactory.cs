using GymManagement.Domain.Gyms;
using TestCommon.TestConstants;

namespace TestCommon.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        string name = Constants.Gym.Name,
        int maxRooms = Constants.Subscriptions.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            name,
            maxRooms,
            subscriptionId: Constants.Subscriptions.Id,
            id: id ?? Constants.Gym.Id);
    }
}