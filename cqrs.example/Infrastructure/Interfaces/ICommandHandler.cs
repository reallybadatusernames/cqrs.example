using System.Threading.Tasks;

namespace Cqrs.Example.Infrastructure
{
    /// <summary>
    /// Base interface for command handlers
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public interface ICommandHandler<in TParameter> where TParameter : ICommand
    {
        void Execute(TParameter command);
    }

    public interface ICommandHandlerAsync<in TParameter> where TParameter : ICommand
    {
        Task ExecuteAsync(TParameter command);
    }

    public interface ICommandHandlerForResultAsync<TParameter, TResult> where TParameter : ICommand where TResult : ICommandResult
    {
        Task<TResult> ExecuteAsync(TParameter command);
    }

    public interface ICommandHandlerForResult<TParameter, TResult> where TParameter : ICommand where TResult : ICommandResult
    {
        TResult Execute(TParameter command);
    }
}
