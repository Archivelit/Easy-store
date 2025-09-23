namespace Store.Core.Constants;

public static class MinIO
{
    // for dev environment only
    // use .env or another secure way to store your keys in production
    public const string ENDPOINT = "localhost:9000";
    public const string ACCESS_KEY = "ROOTNAME";
    public const string SECRET_KEY = "CHANGEME123";

    // bucket names
    public const string USER_BUCKET = "users";
    public const string ITEM_BUCKET = "items";

}
