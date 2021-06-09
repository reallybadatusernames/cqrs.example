using System;
using System.Threading.Tasks;

using SimpleInjector;

namespace Cqrs.Example.Infrastructure
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly Container _container;

        public QueryDispatcher(Container container)
        {
            _container = container ?? throw new ArgumentNullException("container");
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var registration = _container.GetRegistration(typeof(IQueryValidator<TParameter>));
            if (registration != null)
                _container.GetInstance<IQueryValidator<TParameter>>().Validate(query);

            var handler = _container.GetInstance<IQueryHandler<TParameter, TResult>>();
            return handler.Retrieve(query);
        }

        public async Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var registration = _container.GetRegistration(typeof(IQueryValidatorAsync<TParameter>));
            if (registration != null)
                await _container.GetInstance<IQueryValidatorAsync<TParameter>>().ValidateAsync(query);

            var handler = _container.GetInstance<IQueryHandlerAsync<TParameter, TResult>>();
            return await handler.RetrieveAsync(query);
        }
    }
}