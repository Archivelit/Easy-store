namespace IntegrationTests.Handlers.User.Command;

public class DeleteUserCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	
	public DeleteUserCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
	}

	[Fact]
	public async Task DeleteUserAsync_ShouldWork()
	{
		// Arrange
        var command = new DeleteUserCommand(SeedModels.User1.Id);
		var handler = _scope.ServiceProvider.GetRequiredService<ICommandHandler<DeleteUserCommand>>();
		var db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

		// Act
        await handler.Handle(command, CancellationToken.None);

		// Assert
		var user = db.Users.FirstOrDefault(i => i.Id == SeedModels.User1.Id);
        user.Should().BeNull();
	}
}