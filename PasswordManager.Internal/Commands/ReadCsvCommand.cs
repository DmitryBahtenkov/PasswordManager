using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PasswordManager.Internal.Contract.Commands;
using PasswordManager.Internal.Contract.Models;
using PasswordManager.Internal.Contract.Services;
using PasswordManager.Internal.Helpers;

namespace PasswordManager.Internal.Commands
{
    public class ReadCsvCommand : IReadCsvCommand
    {
        private readonly ICryptService _cryptService;

        public ReadCsvCommand(ICryptService cryptService)
        {
            _cryptService = cryptService;
        }

        public async Task<List<Password>> Execute(string input)
        {
            if (!File.Exists(input))
            {
                return new List<Password>();
            }

            var lines = await File.ReadAllLinesAsync(input);

            return await lines.Skip(1).Select(line => line.Split(',')).Select(async x => new Password
            {
                Name = string.IsNullOrEmpty(x[0]) ? x[2] : x[0],
                Login = x[2],
                Crypt = await _cryptService.Encrypt(x[3]),
                Id = Guid.NewGuid().ToString()
            }).ToListAsync();
        }
    }
}