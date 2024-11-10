namespace GymManagement.Application.Profiles.ListProfiles;

public record ListProfilesResult(Guid? AdminId, Guid? ParticipantId, Guid? TrainerId);