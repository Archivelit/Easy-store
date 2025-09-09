namespace IntegrationTests.Handlers.Item.Command;

public class DeleteItemCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	
	public DeleteItemCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
	}

	[Fact]
	public async Task DeleteItemAsync_ShouldWork()
	{
		// Arrange
        var command = new DeleteItemCommand(SeedModels.Item1.Id);
		var handler = _scope.ServiceProvider.GetRequiredService<ICommandHandler<DeleteItemCommand>>();
		var db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

		// Act
        await handler.Handle(command, CancellationToken.None);

		// Assert
		var item = db.Items.FirstOrDefault(i => i.Id == SeedModels.Item1.Id);
        item.Should().BeNull();
	}
}