using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BinateCoveringProblem.App.Shell.Matrix
{
    public static class MatrixExtensions
    {
        public static Dictionary<int, List<int>> ToDictionary(this DataTable matrix)
        {
            var source = new Dictionary<int, List<int>>();

            var rowsCount = matrix.Rows.Count;
            var columnsCount = matrix.Columns.Count;

            for (int r = 1; r < rowsCount + 1; r++)
            {
                source.Add(r, new List<int>());

                for (int c = 1; c < columnsCount + 1; c++)
                {
                    var value = ParseValue(matrix.Rows[r - 1][c - 1]);
                    if (value > 0)
                    {
                        source[r].Add(c);
                    }
                    else if (value < 0)
                    {
                        source[r].Add(-c);
                    }
                }

                if (!source[r].Any())
                {
                    source.Remove(r);
                }
            }

            return source;
        }

        private static int ParseValue(object value)
        {
            if (int.TryParse(value.ToString(), out var outValue))
            {
                return outValue;
            }

            return 0;
        }
    }
}
