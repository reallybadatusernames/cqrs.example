using System.Threading.Tasks;

namespace Cqrs.Example.Infrastructure
{
    /// <summary>
    /// Base interface for query handlers
    /// </summary>
    /// <typeparam name="TParameter">Query type</typeparam>
    /// <typeparam name="TResult">Query Result type</typeparam>
    public interface IQueryHandler<in TParameter, out TResult> where TResult : IQueryResult where TParameter : IQuery
    {
        TResult Retrieve(TParameter query);
    }

    public interface IQueryHandlerAsync<TParameter, TResult> where TResult : IQueryResult where TParameter : IQuery
    {
        Task<TResult> RetrieveAsync(TParameter query);
    }
}
