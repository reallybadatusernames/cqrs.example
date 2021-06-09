using System.Threading.Tasks;

namespace Cqrs.Example.Infrastructure
{
    interface ICommandValidator<in TCommand>
    {
        void Validate(TCommand command);
    }

    interface ICommandValidatorAsync<in TCommand>
    {
        Task ValidateAsync(TCommand command);
    }
}
