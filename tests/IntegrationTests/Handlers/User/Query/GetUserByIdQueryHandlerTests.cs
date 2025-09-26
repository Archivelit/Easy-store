namespace IntegrationTests.Handlers.User.Query;

public class GetUserByIdQueryHandlerTests : IClassFixture<StoreApiFixture>
{
    private readonly StoreApiFixture _fixture;
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;

    public GetUserByIdQueryHandlerTests(StoreApiFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.Services.CreateScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldWork()
    {
        // Arrange
        var query = new GetUserByIdQuery(SeedModels.User1.Id);

        // Act
        var result = await _mediator.Send(query, CancellationToken.None); 

        // Assert
        result.Should().BeEquivalentTo(SeedModels.User1);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldThrow_WithWrongId()
    {
        // Arrange
        var query = new GetUserByIdQuery(SeedModels.User1.Id);

        // Act
        var act = async () => await _mediator.Send(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidUserDataException>();
    }
}
