using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PasswordManager.Internal.Contract.ViewModels;

namespace PasswordManager.Helpers
{
    public delegate void SearchEventHandler(object o, SearchEventArgs args);

    static class PageHelper
    {
        public static event SearchEventHandler SearchEventHandler;

        public static Frame Frame { get; set; }
        public static TextBlock MainPageText { get; set; }
        public static Button NewButton { get; set; }
        public static Button SearchButton { get; set; }
        public static Button CleanButton { get; set; }
        public static TextBox TxtSearch { get; set; }
        public static Frame ConfigFrame { get; set; }
        
        public static void InvokeSearch(object sender, SearchEventArgs args) => SearchEventHandler?.Invoke(sender, args);
        public static void Navigate(IServiceProvider serviceProvider, string key)
        {
            var dict = GetPageTypes();
            if (dict.ContainsKey(key))
            {
                var constructor = dict[key].GetConstructors().FirstOrDefault();

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

    public class SearchEventArgs : EventArgs
    {
        public SearchOptions Options { get; set; }

        public SearchEventArgs(string text)
        {
            Options = new SearchOptions {Name = text};
        }
        
        public SearchEventArgs(SearchOptions options)
        {
            Options = options;
        }
    }
}
