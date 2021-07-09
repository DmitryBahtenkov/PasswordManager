using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.Services;
using PasswordManager.Internal.Contract.ViewModels;
using PasswordManager.Internal.Helpers;

namespace PasswordManager.Internal.Services
{
    public class AccessService : IAccessService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ICryptService _cryptService;

        public AccessService(ApplicationContext applicationContext, ICryptService cryptService)
        {
            _applicationContext = applicationContext;
            _cryptService = cryptService;
        }

        public async Task<ResultModel<Access>> CreateAccess(string password)
        {
            var (hash, salt) = PasswordHelper.GeneratePassword(password);
            var access = new Access
            {
                Id = Guid.NewGuid().ToString(),
                PasswordHash = hash,
                PasswordSalt = salt
            };

            try
            {
                await _applicationContext.Accesses.AddAsync(access);
                await _applicationContext.SaveChangesAsync();
            }
            catch
            {
                return ResultModel<Access>.WithError("Ошибка базы данных");
            }
            await _cryptService.CreateKeys();
            return new ResultModel<Access>(access);
        }

        public async Task<ResultModel<Access>> UpdateAccess(string oldPassword, string newPassword)
        {
            var access = await _applicationContext.Accesses.FirstOrDefaultAsync();
            if (PasswordHelper.ComparePassword(access, oldPassword))
            {
                var (hash, salt) = PasswordHelper.GeneratePassword(newPassword);
                access!.PasswordHash = hash;
                access!.PasswordSalt = salt;

                await _applicationContext.SaveChangesAsync();

                return new ResultModel<Access>(access);
            }

            return ResultModel<Access>.WithError("Неверно введён пароль");
        }

        public async Task<bool> CheckAccess(string password)
        {
            var access = await _applicationContext.Accesses.FirstOrDefaultAsync();
            return PasswordHelper.ComparePassword(access, password);
        }
    }
}
