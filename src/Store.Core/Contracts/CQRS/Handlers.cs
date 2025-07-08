namespace Store.App.GraphQl.CQRS;

public interface ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken ct);
};

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken ct);
};


public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
Task<TResponse> Handle(TQuery query, CancellationToken ct);
};
