using System;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool Compare<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> toCompare)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }
            else if (toCompare is null)
            {
                throw new ArgumentNullException("Source to compare is null");
            }

            return source.Count.Equals(toCompare.Count) && !source.Except(toCompare).Any();
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this Dictionary<TKey, TValue> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            return source.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
