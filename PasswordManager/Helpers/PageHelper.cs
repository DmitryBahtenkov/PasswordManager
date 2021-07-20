using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PasswordManager.Helpers
{
    static class PageHelper
    {
        public static Frame Frame { get; set; }
        public static TextBlock MainPageText { get; set; }
        public static Button NewButton { get; set; }

        public static void Navigate(IServiceProvider serviceProvider, string key)
        {
            var dict = GetPageTypes();
            if (dict.ContainsKey(key))
            {
                var constructor = dict[key].GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public, 
                    null, 
                    CallingConventions.Any, 
                    new Type[]{}, 
                    null);

                var o = constructor?.GetParameters()
                    ?.Select(parameter => serviceProvider.GetService(parameter.ParameterType))
                    ?.ToArray();

                Frame.Navigate(Activator.CreateInstance(dict[key], o));
            }
        }

        private static Dictionary<string, Type> GetPageTypes()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(Page).IsAssignableFrom(x));
            return types.ToDictionary(x => x.Name, x => x);
        }
    }
}
