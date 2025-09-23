namespace Store.App.CQRS.User.Queries;

public sealed class GetLinkToUserProfileImageQueryHandler : IQueryHandler<GetLinkToUserProfileImageQuery,string>
{
    private readonly ILogger<GetLinkToUserProfileImageQueryHandler> _logger;
    private readonly IMinioClient _minio;

    public GetLinkToUserProfileImageQueryHandler(
        ILogger<GetLinkToUserProfileImageQueryHandler> logger,
        IMinioClient minio)
    {
        _logger = logger;
        _minio = minio;        
    }

    public async Task<string> Handle(GetLinkToUserProfileImageQuery query, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        _logger.LogInformation("Item {ItemId} iamge requested", query.Id);

        var image = await _minio.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(MinIO.USER_BUCKET)
            .WithObject($"/images/query.Id.ToString()")
            .WithExpiry(60 * 15) // 15 minutes
        );

        ArgumentNullException.ThrowIfNull(image);
        
        return image;
    }
}