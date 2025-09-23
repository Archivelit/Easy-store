namespace Store.App.CQRS.Models.User.Queries;

public record GetUserProfileImageQuery(Guid Id) : IQuery<object>;