﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinateCoveringProblem.Core.Extensions
{
    public static class SourceSetExtensions
    {
        public static Dictionary<int, List<int>> Reverse(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            var revSet = new Dictionary<int, List<int>>();

            foreach (var row in source)
            {
                foreach (var column in row.Value)
                {
                    if (revSet.ContainsKey(column))
                    {
                        revSet.FirstOrDefault(c => c.Key == column).Value.Add(row.Key);
                        continue;
                    }
                    revSet.Add(column, new List<int>() { row.Key });
                }
            }

            return revSet.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public static void RemoveAssociatedRows(this Dictionary<int, List<int>> source, int column)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            var associatedRows = new List<int>();

            foreach (var row in source)
            {
                if (row.Value.Contains(column))
                {
                    associatedRows.Add(row.Key);
                }
            }

            foreach (var row in associatedRows)
            {
                source.Remove(row);
            }
        }

        public static void RemoveColumn(this Dictionary<int, List<int>> source, int column)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            foreach (var row in source)
            {
                if (row.Value.Contains(column))
                {
                    row.Value.Remove(column);
                }
            }
        }

        public static bool IsEssentialColumn(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            return source.Any(s => s.Value.Count.Equals(1) && s.Value.FirstOrDefault() > 0);
        }

        public static int GetEssentialColumn(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            return source.FirstOrDefault(s => s.Value.Count.Equals(1)).Value.FirstOrDefault(v => v > 0);
        }

        public static bool IsUnacceptableColumn(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            return source.Any(s => s.Value.Count.Equals(1) && s.Value.FirstOrDefault() < 0);
        }

        public static int GetUnacceptableColumn(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            return source.FirstOrDefault(s => s.Value.Count.Equals(1)).Value.FirstOrDefault(v => v < 0);
        }

        public static bool IsTerminalCase(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            var column = 0;

            if (source.Any(s => s.Value.Count.Equals(1)))
            {
                column = source.FirstOrDefault(s => s.Value.Count.Equals(1)).Value.FirstOrDefault();
            }

            return source.Any(s => s.Value.Count.Equals(1) && s.Value.FirstOrDefault().Equals(-column));
        }

        public static string Print(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            var output = new StringBuilder("F = {");
            foreach (var row in source)
            {
                output.Append(string.Format(" {0}:[", row.Key));
                foreach (var column in row.Value)
                {
                    output.Append(string.Format("{0},", column));
                }
                output.Replace(",", "]", output.Length - 1, 1);
            }
            output.Append(" }");

            return output.ToString();
        }
    }
}
