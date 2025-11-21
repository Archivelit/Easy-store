namespace IntegrationTests.Handlers.User.Command;

public class DeleteUserCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	private readonly IMediator _mediator;

	public DeleteUserCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
		_mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
	}

	[Fact]
	public async Task DeleteUserAsync_ShouldWork()
	{
		// Arrange
		var command = new DeleteUserCommand(SeedModels.User1.Id);
		var db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

		// Act
		await _mediator.Send(command, CancellationToken.None);

		// Assert
		var user = db.Users.FirstOrDefault(i => i.Id == SeedModels.User1.Id);
		user.Should().BeNull();
	}

	[Fact]
	public async Task DeleteUserAsync_ShouldThrowException_WhenUserDoesNotExist()
	{
		// Arrange
		var command = new DeleteUserCommand(Guid.NewGuid());

		// Act
		var act = async () => await _mediator.Send(command, CancellationToken.None);

		// Assert
		await act.Should().ThrowAsync<InvalidUserDataException>();
	}
}