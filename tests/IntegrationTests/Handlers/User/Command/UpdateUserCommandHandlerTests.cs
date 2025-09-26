namespace IntegrationTests.Handlers.User.Command;

public class UpdateUserCommandHandlerTests : IClassFixture<StoreApiFixture>
{
    private readonly StoreApiFixture _fixture;
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;

    public UpdateUserCommandHandlerTests(StoreApiFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.Services.CreateScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task UpdateUser_ShouldWork()
    {
        // Arrange
        var expectedUser = new UserDto(SeedModels.User2) with { Name = "John Doe" };
        var dto = new UpdateUserDto(SeedModels.User2.Id, "John Doe", null, null);
        var command = new UpdateUserCommand(dto);

        // Act
        var user = await _mediator.Send(command, CancellationToken.None);

        // Assert
        user.Should().BeEquivalentTo(expectedUser);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldNotChangeItem()
    {
        // Arrange
        var expectedUser = new UserDto(SeedModels.User1);
        var dto = new UpdateUserDto(SeedModels.User1.Id, null, null, null);
        var command = new UpdateUserCommand(dto);

        // Act
        var result = await _mediator.Send(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedUser, options => options);
    }

    [Fact]
    public async Task UpdateUser_ShouldThrow_WhenUserEmailClaimedByAnotherUser()
    {
        // Arrange
        var dto = new UpdateUserDto(SeedModels.User1.Id, null, SeedModels.User2.Email, null);
        var command = new UpdateUserCommand(dto);

        // Act
        var act = async () => await _mediator.Send(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidUserDataException>();
    }
}