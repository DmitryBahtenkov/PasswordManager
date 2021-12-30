using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Internal.Contract;
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.Services;
using PasswordManager.Internal.Contract.ViewModels;

namespace PasswordManager.Internal.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IApplicationContext _applicationContext;
        private readonly ICryptService _cryptService;

        public PasswordService(IApplicationContext applicationContext, ICryptService cryptService)
        {
            _applicationContext = applicationContext;
            _cryptService = cryptService;
        }

        public async Task<ResultModel<Password>> CreateRecord(PasswordViewModel viewModel)
        {
            var record = new Password
            {
                Id = Guid.NewGuid().ToString(),
                Name = viewModel.Name,
                Crypt = await _cryptService.Encrypt(viewModel.Password),
                Login = viewModel.Login
            };

            try
            {
                await _applicationContext.Passwords.AddAsync(record);
                await _applicationContext.SaveChangesAsync();

                return new ResultModel<Password>(record);
            }
            catch (Exception ex)
            {
                return ResultModel<Password>.WithError(ex.Message);
            }
        }

        public async Task<ResultModel<List<Password>>> CreateRecords(List<Password> passwords)
        {
            try
            {
                await _applicationContext.Passwords.AddRangeAsync(passwords);
                await _applicationContext.SaveChangesAsync();

                return new (passwords);
            }
            catch (Exception ex)
            {
                return ResultModel<List<Password>>.WithError(ex.Message);
            }
        }

        public async Task DeleteRecord(string id)
        {
            var password = await _applicationContext.Passwords.FindAsync(id);
            try
            {
                _applicationContext.Passwords.Remove(password);
                await _applicationContext.SaveChangesAsync();
            }
            catch
            {
                //ignore
            }
        }

        public async Task UpdateRecord(PasswordViewModel viewModel)
        {
            var p = await _applicationContext.Passwords.FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (p is not null)
            {
                p.Crypt = await _cryptService.Encrypt(viewModel.Password);
                p.Name = viewModel.Name;
                p.Login = viewModel.Login;
            }

            await _applicationContext.SaveChangesAsync();
        }

        public async Task<ResultModel<IEnumerable<Password>>> GetAll(SearchOptions options)
        {
            if (!string.IsNullOrEmpty(options.Name))
            {
                var lowerName = options.Name.ToLower();
                var items = await _applicationContext.Passwords
                    .Where(x=>x.Name.ToLower().Contains(lowerName)
                        || x.Login.ToLower().Contains(lowerName))
                    .ToListAsync();
                return new(items);
            }
            else
            {
                return new(await _applicationContext.Passwords.ToListAsync());
            }
        }

        public async Task<ResultModel<Password>> Get(string id)
        {
            return new(await _applicationContext.Passwords.FindAsync(id));
        }
    }
}
