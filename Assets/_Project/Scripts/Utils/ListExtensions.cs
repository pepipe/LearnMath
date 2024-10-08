using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnMath.Utils
{
    public static class ListExtensions
    {
        static readonly Random Rng = new ();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
        
        public static IEnumerable<T> Shuffle<T>(this HashSet<T> hashSet)
        {
            var list = hashSet.ToList();
            list.Shuffle();
            return list;
        }
    }

}