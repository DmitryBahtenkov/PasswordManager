using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Internal;
using PasswordManager.Internal.Commands;
using PasswordManager.Internal.Contract;
using PasswordManager.Internal.Contract.Commands;
using PasswordManager.Internal.Contract.Services;
using PasswordManager.Internal.Services;

namespace PasswordManager.IoC
{
    public class Startup
    {
        public void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<IApplicationContext, ApplicationContext>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ICryptService, CryptService>();
            services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IGeneratePasswordCommand, GeneratePasswordCommand>();
            services.AddEntityFrameworkSqlite();
        }
    }
}