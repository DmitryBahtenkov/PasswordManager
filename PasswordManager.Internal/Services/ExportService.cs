using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Internal.Contract.Commands;
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.Services;

namespace PasswordManager.Internal.Services
{
    public class ExportService : IExportService
    {
        private readonly IReadCsvCommand _readCsvCommand;
        private readonly IPasswordService _passwordService;

        public ExportService(IReadCsvCommand readCsvCommand, IPasswordService passwordService)
        {
            _readCsvCommand = readCsvCommand;
            _passwordService = passwordService;
        }

        public async Task<List<Password>> FromChrome(string file)
        {
            var csv = await _readCsvCommand.Execute(file);

            var result = await _passwordService.CreateRecords(csv);

            return result.IsSuccess ? result.Content : new List<Password>();
        }

        public async Task<List<Password>> FromApp(string file)
        {
            throw new System.NotImplementedException();
        }
    }
}