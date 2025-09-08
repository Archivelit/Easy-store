namespace Store.Core.Contracts.CQRS.User.Queries;

public record GetUserByIdQuery(Guid Id) : IQuery<UserDto>;