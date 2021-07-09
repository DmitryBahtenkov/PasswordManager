using PasswordManager.Internal.Contract.ViewModels;

namespace PasswordManager.Internal.Contract.Commands
{
    public interface IGeneratePasswordCommand : ICommand<PasswordOptions, string>
    { }
}
