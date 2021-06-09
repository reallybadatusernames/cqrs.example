namespace Cqrs.Example.Infrastructure
{
    /// <summary>
    /// Marker interface to mark a query
    /// </summary>
    public interface IQuery
    {
        
    }

    public interface IQuery<TResult>
    {

    }
}