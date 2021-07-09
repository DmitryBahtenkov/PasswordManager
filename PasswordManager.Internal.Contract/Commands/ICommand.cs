using System.Threading.Tasks;

namespace PasswordManager.Internal.Contract.Commands
{
    public interface ICommand<TInput, TOutput>
    {
        public Task<TOutput> Execute(TInput input);
    }
}
