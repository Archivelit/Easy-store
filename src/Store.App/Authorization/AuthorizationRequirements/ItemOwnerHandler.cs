namespace Store.App.Authorization.AuthorizationRequirements;

public class ItemOwnerHandler : AuthorizationHandler<ItemOwnerRequirement, Item>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        ItemOwnerRequirement requirement, 
        Item resource)
    {
        var userId = context.User.FindFirst("sub")?.Value;

        if (userId != null && resource.UserId.ToString() == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}