namespace Store.App.CQRS.User.Queries;

public sealed class GetItemProfileImageQueryHandler(
    ILogger<GetItemProfileImageQueryHandler> logger,

    ) : IQueryHandler<GetItemProfileImageQuery,object>
{
    private readonly ILogger<GetItemProfileImageQueryHandler> _logger = logger;
    private readonly _minio = minio;

    public async Task<object> Handle(GetItemProfileImageQuery query)
    {
        _logger.LogInfo("Item {ItemId} iamge requested", query.Id);


    }
}