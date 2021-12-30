using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Internal.Contract.Models;

namespace PasswordManager.Internal.Contract.Services
{
    public interface IExportService
    {
        public Task<List<Password>> Import(string file);
        public Task<string> Export(string file);
    }
}