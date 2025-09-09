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
		var item = new UpdateItemDto(SeedModels.Item1.Id, null, null, null, 15);
        var command = new UpdateItemCommand(item);
		var handler = _scope.ServiceProvider.GetRequiredService<ICommandHandler<UpdateItemCommand, ItemDto>>();
		
		var expectedResult = new ItemDto(SeedModels.Item1) with {
			QuantityInStock = 15
		};

		// Act
        var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		result.Should().BeEquivalentTo(expectedResult);
	}
}