namespace IntegrationTests.Handlers.Item.Query;

public class GetItemQueryHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	private readonly IMediator _mediator;

	public GetItemQueryHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
		_mediator = _scope.GetRequiredService<IMediator>();
	}

	[Fact]
	public async Task GetItemByIdAsync_ShouldWork()
	{
        // Arrange
        var query = new GetItemByIdQuery(SeedModels.Item2.Id);
        
		// Act
        var result = await _mediator.Send(query, CancellationToken.None); 

		// Assert
		result.Should().BeEquivalentTo(new ItemDto(SeedModels.Item2));
	}

	[Fact]
	public async Task GetItemByIdAsync_WithWrongId_ShouldThrow()
	{
        // Arrange
        var query = new GetItemByIdQuery(Guid.Parse(""));
        
		// Act
        var result = await _mediator.Send(query, CancellationToken.None);

		// Assert
		result.Should().BeEquivalentTo(new ItemDto(SeedModels.Item2));
	}
}