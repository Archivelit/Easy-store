namespace Store.App.CQRS.Models.Item.Queries;

public record GetItemProfileImageQuery(Guid Id) : IQuery<object>;