using Microsoft.EntityFrameworkCore;
using PasswordManager.Internal.Contract;
using PasswordManager.Internal.Contract.Models;

namespace PasswordManager.Internal
{
    public sealed class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Password> Passwords { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Passwords.db");
        }
    }
}