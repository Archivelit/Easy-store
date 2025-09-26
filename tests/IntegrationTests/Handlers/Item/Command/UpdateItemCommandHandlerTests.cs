namespace IntegrationTests.Handlers.Item.Command;

public class UpdateItemCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	private readonly IMediator _mediator;
	
	public UpdateItemCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
		_mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
	}

	[Fact]
	public async Task UpdateItemAsync_ShouldWork()
	{
        // Arrange
        var expectedItem = new ItemDto(SeedModels.Item1) with { QuantityInStock = 15 };
		var dto = new UpdateItemDto(expectedItem);
        var command = new UpdateItemCommand(dto);

		// Act
        var result = await _mediator.Send(command, CancellationToken.None);

		// Assert
		result.Should().BeEquivalentTo(expectedItem, options => options
			.Excluding(x => x.UpdatedAt));
    }
}