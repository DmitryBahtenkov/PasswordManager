using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Internal.Helpers
{
    public static class EnumerableExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(this IEnumerable<Task<T>> source)
        {
            var result = new List<T>();
            foreach (var task in source)
            {
                result.Add(await task);
            }

            return result;
        }
    }
}