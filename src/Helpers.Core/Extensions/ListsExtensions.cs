using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers.Core
{
    public static class ListsExtensions
    {
        public static IList<IViolation> FlatViolations(this IList<IViolation> violations)
        {
            return getFlatViolations(violations);
        }

        private static IList<IViolation> getFlatViolations(this IList<IViolation> violations, IList<IViolation> flatViolations = null)
        {
            if (flatViolations == null)
                flatViolations = new List<IViolation>();
            foreach (var violation in violations)
            {
                if (violation.SubViolations.Any())
                    flatViolations.AddRange(getFlatViolations(violation.SubViolations, flatViolations));
                else
                    flatViolations.Add(violation);
            }

            return flatViolations;
        }

        public static IList<T> NullableToList<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return new List<T>();
            return source.ToList();
        }


        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return null;
            return new HashSet<T>(source);
        }


        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            var list = destination as List<T>;
            if (list != null)
            {
                list.AddRange(source);
            }
            else
            {
                foreach (T item in source)
                {
                    destination.Add(item);
                }
            }
        }

        /// <summary>
        ///     Split given list to many sub lists according to the wanted count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList">The main list to split it</param>
        /// <param name="chunksCount">Number of wanted sub lists</param>
        /// <returns></returns>
        public static List<List<T>> SplitToChunks<T>(this ICollection<T> originalList, int chunksCount)
        {
            var originalListCount = originalList.Count();
            var chunkBase = originalListCount%chunksCount == 0
                ? originalListCount/(chunksCount)
                : originalListCount/(chunksCount - 1);

            return originalList.Select((x, i) => new
            {
                Index = i,
                Value = x
            }).GroupBy(x => x.Index/chunkBase)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        /// <summary>
        ///     Split given list to many sub lists each sub list will contain certain number of items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList">The main list to split it</param>
        /// <param name="chunkSize">
        ///     Number of items that every sub list should contains. just the last sub list may contains
        ///     items less than this count
        /// </param>
        /// <returns></returns>
        public static List<List<T>> DistributeToChunks<T>(this ICollection<T> originalList, int chunkSize)
        {
            return originalList.Select((x, i) => new
            {
                Index = i,
                Value = x
            }).GroupBy(x => x.Index/chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        /// <summary>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }


        /// <summary>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctByGroup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(g => g.First());
        }


        /// <summary>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static List<TSource> DistinctByGroup<TSource, TKey>(this List<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(g => g.First()).ToList();
        }
    }
}