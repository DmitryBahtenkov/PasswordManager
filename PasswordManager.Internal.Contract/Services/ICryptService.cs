using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Internal.Contract.Services
{
    public interface ICryptService
    {
        public Task CreateKeys();
        public Task DeleteKeys();
        public Task<string> Decrypt(string base64Text);
        public Task<string> Encrypt(string text);
    }
}
