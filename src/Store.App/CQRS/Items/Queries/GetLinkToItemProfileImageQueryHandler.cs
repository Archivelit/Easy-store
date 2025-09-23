namespace Store.App.CQRS.User.Queries;

public sealed class GetLinkToItemProfileImageQueryHandler(
    ILogger<GetLinkToItemProfileImageQueryHandler> logger,
    IMinioClient minio
    ) : IQueryHandler<GetLinkToItemProfileImageQuery, string>
{
    private readonly ILogger<GetLinkToItemProfileImageQueryHandler> _logger = logger;
    private readonly IMinioClient _minio = minio;

    public async Task<string> Handle(GetLinkToItemProfileImageQuery query, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        _logger.LogInformation("Item {ItemId} iamge requested", query.Id);

        var image = await _minio.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(MinIO.ITEM_BUCKET)
            .WithObject($"/images/query.Id.ToString()")
            .WithExpiry(60 * 15) // 15 minutes
        );

        ArgumentNullException.ThrowIfNull(image);
        
        return image;
    }
}