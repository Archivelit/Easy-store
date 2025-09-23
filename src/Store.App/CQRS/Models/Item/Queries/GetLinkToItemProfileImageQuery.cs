namespace Store.App.CQRS.Models.Item.Queries;

public record GetLinkToItemProfileImageQuery(Guid Id) : IQuery<string>;