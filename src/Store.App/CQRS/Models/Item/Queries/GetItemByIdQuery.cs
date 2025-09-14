namespace Store.App.CQRS.Models.Item.Queries;

public record GetItemByIdQuery : IQuery<ItemDto>
{
    public Guid Id { get; set; }

    public GetItemByIdQuery(Guid id)
    {
        Id = id;
    }

    public GetItemByIdQuery(string id)
    {
        Id = Guid.Parse(id);
    }
}; 