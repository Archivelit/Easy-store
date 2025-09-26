namespace IntegrationTests.Handlers.Item.Command;

public class CreateItemCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	private readonly IMediator _mediator;

	public CreateItemCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
		_mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
	}

	[Fact]
	public async Task CreateItemAsync_ShouldWork()
	{
		// Arrange
		var item = new CreateItemDto("Bycicle", null, 3.99m, Guid.NewGuid(), 3);
        var command = new CreateItemCommand(item);
        
		// Act
        var result = await _mediator.Send(command, CancellationToken.None);

		// Assert
		result.Should().BeEquivalentTo(new ItemDto(item));
	}
}