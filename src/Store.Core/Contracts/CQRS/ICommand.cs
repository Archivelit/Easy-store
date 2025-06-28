using MediatR;

namespace Store.Core.Contracts.CQRS;

public interface ICommand : IRequest;
public interface ICommand<TResult> : IRequest<TResult>;