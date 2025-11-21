namespace IntegrationTests.Handlers.Item.Command;

public class DeleteItemCommandHandlerTests : IClassFixture<StoreApiFixture>
{
	private readonly StoreApiFixture _fixture;
	private readonly IServiceScope _scope;
	private readonly IMediator _mediator;

	public DeleteItemCommandHandlerTests(StoreApiFixture fixture)
	{
		_fixture = fixture;
		_scope = fixture.Services.CreateScope();
		_mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
	}

	[Fact]
	public async Task DeleteItemAsync_ShouldWork()
	{
		// Arrange
		var command = new DeleteItemCommand(SeedModels.Item1.Id);
		var db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

		// Act
		await _mediator.Send(command, CancellationToken.None);

		// Assert
		var item = db.Items.FirstOrDefault(i => i.Id == SeedModels.Item1.Id);
		item.Should().BeNull();
	}

	[Fact]
	public async Task DeleteItemAsync_ShouldThrowException_WhenItemDoesNotExist()
	{
		// Arrange
		var command = new DeleteItemCommand(Guid.NewGuid());

		// Act
		var act = async () => await _mediator.Send(command, CancellationToken.None);

		// Assert
		await act.Should().ThrowAsync<InvalidItemDataException>();
	}
}