namespace IntegrationTests.Handlers.User.Query;

public class GetUserByIdQueryHandlerTests : IClassFixture<StoreApiFixture>
{
    private readonly StoreApiFixture _fixture;
    private readonly IServiceScope _scope;
    public GetUserByIdQueryHandlerTests(StoreApiFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.Services.CreateScope();
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldWork()
    {
        // Arrange
        var query = new GetUserByIdQuery(SeedModels.User1.Id);
        var handler = _scope.ServiceProvider.GetRequiredService<IQueryHandler<GetUserByIdQuery, UserDto>>();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(SeedModels.User1);
    }
}
