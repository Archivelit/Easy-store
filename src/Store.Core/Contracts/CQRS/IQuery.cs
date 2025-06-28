using MediatR;

namespace Store.Core.Contracts.CQRS;

public interface IQuery<TResult> : IRequest<TResult>;