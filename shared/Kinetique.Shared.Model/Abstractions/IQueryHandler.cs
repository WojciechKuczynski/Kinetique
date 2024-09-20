namespace Kinetique.Shared.Model.Abstractions;

public interface IQueryHandler<in TQuery, TResult> where TQuery: class, IQuery<TResult>
{
    Task<TResult> Handle(TQuery query, CancellationToken token = default);
}
