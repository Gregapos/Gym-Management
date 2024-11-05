using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GymManagement.Application.Common.Behaviors;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Domain.Gyms;
using MediatR;
using NSubstitute;
using TestCommon.Gyms;

namespace GymManagement.Application.UnitTests.Common.Behaviors;

public class ValidationBehaviorTests
{
    private readonly ValidationBehavior<CreateGymCommand, ErrorOr<Gym>> _validationBehavior;
    private readonly IValidator<CreateGymCommand> _mockValidator;
    private readonly RequestHandlerDelegate<ErrorOr<Gym>> _mockNextBehavior;
    public ValidationBehaviorTests()
    {
        // Create a next behavior (mock)
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Gym>>>();

        // Create validator (mock)
        _mockValidator = Substitute.For<IValidator<CreateGymCommand>>();

        // Create validation behavior (SystemUnderTest)
        _validationBehavior = new ValidationBehavior<CreateGymCommand, ErrorOr<Gym>>(_mockValidator);
    }

    [Fact]
    public async Task InvokeBehavior_WhenValidationResultIsValid_ShouldInvokeNextBehavior()
    {
        // Arrange
        var createGymRequest = GymCommandFactory.CreateCreateGymCommand();
        var gym = GymFactory.CreateGym();

        _mockValidator
            .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _mockNextBehavior.Invoke().Returns(gym);

        // Act
        var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(gym);
    }

    [Fact]
    public async Task InvokeBehavior_WhenValidationResultIsNotValid_ShouldReturnListOfErrors()
    {
        // Arrange
        var createGymRequest = GymCommandFactory.CreateCreateGymCommand();
        List<ValidationFailure> validationFailures = [new(propertyName: "propertyNameThatFailed", errorMessage: "This is the errorMessage")];

        _mockValidator
            .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(validationFailures));

        // Act
        var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("propertyNameThatFailed");
        result.FirstError.Description.Should().Be("This is the errorMessage");
    }
}