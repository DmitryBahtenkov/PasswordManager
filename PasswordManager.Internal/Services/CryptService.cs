using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.Internal.Const;
using PasswordManager.Internal.Contract.Services;

namespace PasswordManager.Internal.Services
{
    public class CryptService : ICryptService
    {
        private readonly RSACryptoServiceProvider _rsaCryptoServiceProvider = new();

        public async Task CreateKeys()
        {
            var publicKey = _rsaCryptoServiceProvider.ToXmlString(false);
            var privateKey = _rsaCryptoServiceProvider.ToXmlString(true);

            if (!Directory.Exists(FileConstants.Filepath))
            {
                Directory.CreateDirectory(FileConstants.Filepath);
            }

            await File.WriteAllTextAsync(FileConstants.PubPath, publicKey, Encoding.Default);
            await File.WriteAllTextAsync(FileConstants.PrivPath, privateKey, Encoding.Default);
        }

        public Task DeleteKeys()
        {
            if (Directory.Exists(FileConstants.Filepath))
            {
                Directory.Delete(FileConstants.Filepath, true);
            }
            return Task.CompletedTask;
        }

        public async Task<string> Decrypt(string base64Text)
        {
            var privateKey = await File.ReadAllTextAsync(FileConstants.PrivPath);
            _rsaCryptoServiceProvider.FromXmlString(privateKey);
            var dataToDecrypt = Convert.FromBase64String(base64Text);
            var decryptedData = _rsaCryptoServiceProvider.Decrypt(dataToDecrypt, false);
            return Encoding.UTF8.GetString(decryptedData);
        }

        public async Task<string> Encrypt(string text)
        {
            var publicKey = await File.ReadAllTextAsync(FileConstants.PubPath);
            _rsaCryptoServiceProvider.FromXmlString(publicKey);
            var dataToEncrypt = Encoding.UTF8.GetBytes(text);

            var encrypted = _rsaCryptoServiceProvider.Encrypt(dataToEncrypt, false);

            return Convert.ToBase64String(encrypted);
        }
    }
}
