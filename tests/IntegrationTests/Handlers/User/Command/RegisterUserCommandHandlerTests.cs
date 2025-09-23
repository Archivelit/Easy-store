namespace IntegrationTests.Handlers.User.Command;

public class RegisterUserCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	
	public RegisterUserCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
	}

	[Fact]
	public async Task RegisterUserAsync_ShouldWork()
	{
		// Arrange
		var command = new RegisterUserCommand(new RegisterUserDto("FooBuzz@example.com", "Foo"));
		var handler = _scope.ServiceProvider.GetRequiredService<ICommandHandler<RegisterUserCommand, UserDto>>();
		var db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

		// Act
		var registeredUser = await handler.Handle(command, CancellationToken.None);

		// Assert
		var user = db.Users.FirstOrDefault(i => i.Email == "FooBuzz@example.com");
		user.Should().BeEquivalentTo(registeredUser);
	}
}