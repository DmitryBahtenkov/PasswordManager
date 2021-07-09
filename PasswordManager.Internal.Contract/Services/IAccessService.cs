using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.ViewModels;

namespace PasswordManager.Internal.Contract.Services
{
    public interface IAccessService
    {
        public Task<ResultModel<Access>> CreateAccess(string password);
        public Task<ResultModel<Access>> UpdateAccess(string oldPassword, string newPassword);
        public Task<bool> CheckAccess(string password);
    }
}
