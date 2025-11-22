namespace Store.API.Authorization.AuthorizationHandlers;

public class ItemOwnerHandler : AuthorizationHandler<ItemOwnerRequirement, Item>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        ItemOwnerRequirement requirement, 
        Item resource)
    {
        var userId = context.User.FindFirst("sub")?.Value;

        if (userId is not null && resource.UserId.ToString() == userId)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}