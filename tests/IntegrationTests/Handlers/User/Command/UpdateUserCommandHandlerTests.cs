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
        var command = new UpdateUserCommand(expectedUser);
        
        // Act
        var user = await _mediator.Send(command, CancellationToken.None);

        // Assert
        user.Should().BeEquivalentTo(expectedUser);
    }
}