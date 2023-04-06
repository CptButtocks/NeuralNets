using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> haystack)
        {
            Random random = new Random();
            int index = random.Next(haystack.Count());
            return haystack.ElementAt(index);
        }
    }
}
