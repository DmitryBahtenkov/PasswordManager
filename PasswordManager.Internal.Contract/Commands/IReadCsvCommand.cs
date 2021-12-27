using System.Collections.Generic;
using PasswordManager.Internal.Contract.Models;

namespace PasswordManager.Internal.Contract.Commands
{
    public interface IReadCsvCommand : ICommand<string, List<Password>>
    {
        
    }
}