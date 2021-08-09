using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.ViewModels;

namespace PasswordManager.Internal.Contract.Services
{
    public interface IPasswordService
    {
        public Task<ResultModel<Password>> CreateRecord(PasswordViewModel viewModel);
        public Task DeleteRecord(string id);
        public Task UpdateRecord(PasswordViewModel viewModel);
        public Task<ResultModel<IEnumerable<Password>>> GetAll(SearchOptions options);
        public Task<ResultModel<Password>> Get(string id);
    }
}
