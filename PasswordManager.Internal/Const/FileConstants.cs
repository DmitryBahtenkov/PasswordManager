using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Internal.Const
{
    public class FileConstants
    {
        public static readonly string Filepath = $"{AppDomain.CurrentDomain.BaseDirectory}.keys";
        public static readonly string PubPath = $"{AppDomain.CurrentDomain.BaseDirectory}.keys/pub.xml";
        public static readonly string PrivPath = $"{AppDomain.CurrentDomain.BaseDirectory}.keys/priv.xml";
    }
}
