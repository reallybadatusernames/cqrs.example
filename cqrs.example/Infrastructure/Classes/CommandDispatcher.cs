using System;
using System.Threading.Tasks;

using SimpleInjector;

namespace Cqrs.Example.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Container _container;

        public CommandDispatcher(Container container)
        {
            _container = container ?? throw new ArgumentNullException("container");
        }

        /// <summary>
        /// Used to dispatch a command.
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="command">ICommand to be executed</param>
        public void Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            var registration = _container.GetRegistration(typeof(ICommandValidator<TParameter>));
            if (registration != null)
                _container.GetInstance<ICommandValidator<TParameter>>().Validate(command);

            var handler = _container.GetInstance<ICommandHandler<TParameter>>();
            handler.Execute(command);
        }

        /// <summary>
        /// Used to dispatch a command asynchronously.
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="command">ICommand to be executed</param>
        public async Task DispatchAsync<TParameter>(TParameter command) where TParameter : ICommand
        {
            //Command validation used to be synchronous and some methods may still use them. Leave in place until they've been identified
            var registration = _container.GetRegistration(typeof(ICommandValidator<TParameter>));
            if (registration != null)
                _container.GetInstance<ICommandValidator<TParameter>>().Validate(command);

            //use ICommandValidatorAsync going forward
            registration = _container.GetRegistration(typeof(ICommandValidatorAsync<TParameter>));
            if (registration != null)
                await _container.GetInstance<ICommandValidatorAsync<TParameter>>().ValidateAsync(command);

            var handler = _container.GetInstance<ICommandHandlerAsync<TParameter>>();
            await handler.ExecuteAsync(command);
        }

        /// <summary>
        /// Used to execute a command and return the Id of the entity affected. Do not implement to perform advanced queries.
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command">ICommand to be executed</param>
        /// <returns>ICommandResult</returns>
        public TResult DispatchForResult<TParameter, TResult>(TParameter command)
            where TParameter : ICommand
            where TResult : ICommandResult
        {
            var registration = _container.GetRegistration(typeof(ICommandValidator<TParameter>));
            if (registration != null)
                _container.GetInstance<ICommandValidator<TParameter>>().Validate(command);

            var handler = _container.GetInstance<ICommandHandlerForResult<TParameter, TResult>>();
            return handler.Execute(command);
        }

        /// <summary>
        /// Used to execute a command asynchronously and return the Id of the entity affected. Do not implement to perform advanced queries.
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command">ICommand to be executed</param>
        /// <returns>ICommandResult</returns>
        public async Task<TResult> DispatchForResultAsync<TParameter, TResult>(TParameter command)
            where TParameter : ICommand
            where TResult : ICommandResult
        {
            var registration = _container.GetRegistration(typeof(ICommandValidator<TParameter>));
            if (registration != null)
                _container.GetInstance<ICommandValidator<TParameter>>().Validate(command);

            //use ICommandValidatorAsync going forward
            registration = _container.GetRegistration(typeof(ICommandValidatorAsync<TParameter>));
            if (registration != null)
                await _container.GetInstance<ICommandValidatorAsync<TParameter>>().ValidateAsync(command);

            var handler = _container.GetInstance<ICommandHandlerForResultAsync<TParameter, TResult>>();
            return await handler.ExecuteAsync(command);
        }
    }
}