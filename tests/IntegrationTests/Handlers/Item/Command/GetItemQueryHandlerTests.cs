namespace IntegrationTests.Handlers.Item.Command;

public class CreateItemCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	
	public CreateItemCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
	}

	[Fact]
	public async Task CreateItemAsync_ShouldWork()
	{
		// Arrange
		var item = new CreateItemDto("Bycicle", null, 3.99m, Guid.NewGuid(), 3);
        var command = new CreateItemCommand(item);
		var handler = _scope.ServiceProvider.GetRequiredService<ICommandHandler<CreateItemCommand, ItemDto>>();
        
		// Act
        var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		result.Should().BeEquivalentTo(new ItemDto(item));
	}
}