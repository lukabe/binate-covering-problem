using System;
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
                    var rowIndex = column > 0 ? row.Key : -row.Key;
                    if (revSet.ContainsKey(Math.Abs(column)))
                    {
                        revSet.FirstOrDefault(c => c.Key == Math.Abs(column)).Value.Add(rowIndex);
                        continue;
                    }
                    revSet.Add(Math.Abs(column), new List<int>() { rowIndex });
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

        public static Dictionary<int, List<int>> WithoutNegations(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            var negationRows = new List<int>();

            foreach (var row in source)
            {
                if (row.Value.Any(v => v < 0))
                {
                    negationRows.Add(row.Key);
                }
            }

            var sourceWithoutNegations = source.ToDictionary();
            foreach (var row in negationRows)
            {
                sourceWithoutNegations.Remove(row);
            }

            return sourceWithoutNegations;
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

            return source.FirstOrDefault(s => s.Value.Count.Equals(1) && s.Value.FirstOrDefault() > 0).Value.FirstOrDefault();
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

            return source.FirstOrDefault(s => s.Value.Count.Equals(1) && s.Value.FirstOrDefault() < 0).Value.FirstOrDefault();
        }

        public static bool IsTerminalCase(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
            }

            if (source.IsEssentialColumn() && source.IsTerminalCase(source.GetEssentialColumn()))
            {
                return true;
            }

            if (source.IsUnacceptableColumn() && source.IsTerminalCase(source.GetUnacceptableColumn()))
            {
                return true;
            }

            return false;
        }

        public static bool IsTerminalCase(this Dictionary<int, List<int>> source, int column)
        {
            if (source is null)
            {
                throw new ArgumentNullException("Source is null");
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
