namespace IntegrationTests.Handlers.Item.Command;

public class UpdateItemCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	
	public UpdateItemCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
	}

	[Fact]
	public async Task CreateItemAsync_ShouldWork()
	{
        // Arrange
        var expectedItem = new ItemDto(SeedModels.Item1) with { QuantityInStock = 15 };
		var dto = new UpdateItemDto(expectedItem);
        var command = new UpdateItemCommand(dto);
		var handler = _scope.ServiceProvider.GetRequiredService<ICommandHandler<UpdateItemCommand, ItemDto>>();

		// Act
        var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		result.Should().BeEquivalentTo(expectedItem, options => options
			.Excluding(x => x.UpdatedAt));
    }
}