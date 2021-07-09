using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Internal.Contract.Models
{
    public class Password
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Crypt { get; set; }
    }
}
