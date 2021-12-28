using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PasswordManager.Internal.Contract.Commands;
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.Services;
using PasswordManager.Internal.Contract.ViewModels;
using PasswordManager.Internal.Helpers;

namespace PasswordManager.Internal.Services
{
    public class ExportService : IExportService
    {
        private readonly IReadCsvCommand _readCsvCommand;
        private readonly IPasswordService _passwordService;
        private readonly ICryptService _cryptService;

        public ExportService(IReadCsvCommand readCsvCommand, IPasswordService passwordService, ICryptService cryptService)
        {
            _readCsvCommand = readCsvCommand;
            _passwordService = passwordService;
            _cryptService = cryptService;
        }

        public async Task<List<Password>> Import(string file)
        {
            var csv = await _readCsvCommand.Execute(file);

            var result = await _passwordService.CreateRecords(csv);

            return result.IsSuccess ? result.Content : new List<Password>();
        }

        public async Task<string> Export(string file)
        {
            var passwords = (await _passwordService
                .GetAll(new SearchOptions())).Content;

            var headers = new [] {"name,url,login,password"};
            
            var lines = await passwords
                .Select(async x => $"{x.Name},url,{x.Login},{await _cryptService.Decrypt(x.Crypt)}")
                .ToListAsync();

            await File.WriteAllLinesAsync(file, headers.Concat(lines));

            return file;
        }
    }
}