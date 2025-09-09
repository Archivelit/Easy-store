using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Handlers.User.Command;

public class UpdateUserCommandHandlerTests : IClassFixture<StoreApiFixture>
{
    private readonly StoreApiFixture _fixture;
    private readonly IServiceScope _scope;
    
    public UpdateUserCommandHandlerTests(StoreApiFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.Services.CreateScope();
    }

    [Fact]
    public async Task UpdateUser_ShouldWork() 
    {
        // Arrange
        var expectedUser = new UserDto(SeedModels.User2) with { Name = "John Doe" };
        var handler = _scope.ServiceProvider.GetRequiredService<ICommandHandler<UpdateUserCommand, UserDto>>();
        var command = new UpdateUserCommand(expectedUser);
        
        // Act
        var user = await handler.Handle(command, CancellationToken.None);

        // Assert
        user.Should().BeEquivalentTo(expectedUser);
    }
}