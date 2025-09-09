using Store.Core.Contracts.CQRS;

namespace Store.App.CQRS.Models.User.Queries;

public record GetUserByIdQuery(Guid Id) : IQuery<UserDto>;