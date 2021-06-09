using System.Threading.Tasks;

namespace Cqrs.Example.Infrastructure
{
    interface IQueryValidator<in TQuery>
    {
        void Validate(TQuery query);
    }

    interface IQueryValidatorAsync<in TQuery>
    {
        Task ValidateAsync(TQuery query);
    }
}
