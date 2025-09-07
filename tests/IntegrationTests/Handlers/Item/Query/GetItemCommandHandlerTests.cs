namespace IntegrationTests.Handlers.Item.Query;

public class GetItemCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	
	public GetItemCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
	}

	[Fact]
	public async Task GetByIdAsync_ShouldWork()
	{
        // Arrange
        var query = new GetItemByIdQuery(SeedModels.Item2.Id);
		var handler = _scope.ServiceProvider.GetRequiredService<IQueryHandler<GetItemByIdQuery, ItemDto>>();
        
		// Act
        var result = await handler.Handle(query, CancellationToken.None);

		// Assert
		result.Should().BeEquivalentTo(new ItemDto(SeedModels.Item2));
	}
}