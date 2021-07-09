using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.Internal.Contract.Commands;
using PasswordManager.Internal.Contract.ViewModels;

namespace PasswordManager.Internal.Commands
{
    class GeneratePasswordCommand : IGeneratePasswordCommand
    {
        private const string SmallLetters = "qwertyuiopasdfghjklzxcvbnm";
        private const string BigLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        private const string Digits = "1234567890";
        private const string SpecialSymbols = "!@#$%^&*()";

        public Task<string> Execute(PasswordOptions input)
        {
            var symbols = ApplyOptions(input);

            var rnd = new Random();
            var result = "";
            for (int i = 0; i < input.Length; i++)
            {
                result += symbols[rnd.Next(0, symbols.Length)];
            }

            return Task.FromResult(result);
        }

        private string ApplyOptions(PasswordOptions input)
        {
            var symbols = SmallLetters;
            if (input.IncludeBigLetters)
            {
                symbols += BigLetters;
            }

            if (input.IncludeDigit)
            {
                symbols += Digits;
            }

            if (input.IncludeSpecSymbols)
            {
                symbols += SpecialSymbols;
            }

            return symbols;
        }
    }
}
