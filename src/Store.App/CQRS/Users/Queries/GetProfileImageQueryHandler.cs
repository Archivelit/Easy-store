namespace Store.App.CQRS.User.Queries;

public sealed class GetUserProfileImageQueryHandler(
    ILogger<GetUserByIdQueryHandler> logger,
    
    ) : IQueryHandler<GetUserProfileImageQuery,object>
{
    private readonly ILogger<GetUserProfileImageQuery> _logger = logger;
    private readonly _minio = minio;

    public async Task<object> Handle
    {
        _logger.LogInfo("Item {ItemId} iamge requested", query.Id);
    }
}