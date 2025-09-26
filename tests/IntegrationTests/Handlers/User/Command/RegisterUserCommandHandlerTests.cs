namespace IntegrationTests.Handlers.User.Command;

public class RegisterUserCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	private readonly IMediator _mediator;

	public RegisterUserCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
		_mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
	}

	[Fact]
	public async Task RegisterUserAsync_ShouldWork()
	{
		// Arrange
		var command = new RegisterUserCommand(new RegisterUserDto("FooBuzz@example.com", "Foo"));
		var db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

		// Act
		var registeredUser = await _mediator.Send(command, CancellationToken.None);

		// Assert
		var user = db.Users.FirstOrDefault(i => i.Email == "FooBuzz@example.com");
		user.Should().BeEquivalentTo(registeredUser);
	}

	[Fact]
	public async Task RegisterUserAsync_ShouldThrowException_WhenEmailAlreadyExists()
	{
		// Arrange
		var command = new RegisterUserCommand(new RegisterUserDto(SeedModels.User1.Email, "Foo"));

		// Act
		var act = async () => await _mediator.Send(command, CancellationToken.None);

		// Assert
		await act.Should().ThrowAsync<InvalidUserDataException>();
	}
}