using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Internal.Contract.Models;

namespace PasswordManager.Internal.Contract
{
    public interface IApplicationContext
    {
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Password> Passwords { get; set; }
    }
}
