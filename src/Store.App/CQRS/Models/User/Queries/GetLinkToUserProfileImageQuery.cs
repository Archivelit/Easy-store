namespace Store.App.CQRS.Models.User.Queries;

public record GetLinkToUserProfileImageQuery(Guid Id) : IQuery<string>;