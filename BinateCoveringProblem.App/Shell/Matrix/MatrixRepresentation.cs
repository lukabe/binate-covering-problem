using BinateCoveringProblem.App.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BinateCoveringProblem.App.Shell.Matrix
{
    public class MatrixRepresentation : IMatrixRepresentation
    {
        private static readonly string[] AvailableCellValues = new string[] { "0", "1", "-1" };
        private static readonly string DefaultCellValue = AvailableCellValues[0];

        private readonly DataTable matrix;
        private readonly Cell selectedCell;

        public MatrixRepresentation()
        {
            matrix = new DataTable();
            selectedCell = new Cell();
        }

        public DataView ToDataView => matrix.AsDataView();

        public void ChangeColumnsCount(int count)
        {
            var currentCount = matrix.Columns.Count;

            if (count == currentCount)
            {
                return;
            }
            
            if (count > currentCount)
            {
                for (int i = currentCount + 1; i <= count; i++)
                {
                    var column = new DataColumn($"x{i}")
                    {
                        DefaultValue = DefaultCellValue,
                        ReadOnly = false
                    };

                    matrix.Columns.Add(column);
                }
            }
            
            if (count < currentCount)
            {
                for (int i = currentCount - 1; i >= count; i--)
                {
                    matrix.Columns.RemoveAt(i);
                }
            }
        }

        public void ChangeRowsCount(int count)
        {
            var currentCount = matrix.Rows.Count;

            if (count == currentCount)
            {
                return;
            }
            
            if (count > currentCount)
            {
                for (int i = currentCount + 1; i <= count; i++)
                {
                    matrix.Rows.Add(GetRowFulfillment().ToArray());
                }
            }
            
            if (count < currentCount)
            {
                for (int i = currentCount - 1; i >= count; i--)
                {
                    matrix.Rows.RemoveAt(i);
                }
            }
        }

        private IEnumerable<string> GetRowFulfillment()
        {
            foreach (var column in matrix.Columns)
            {
                yield return DefaultCellValue;
            }
        }

        public int GetRowIndex(DataRow row)
        {
            return matrix.Rows.IndexOf(row);
        }

        public void ChangeSelectedCell(int rowIndex, int columnIndex)
        {
            selectedCell.RowIndex = rowIndex;
            selectedCell.ColumnIndex = columnIndex;
        }

        public void ChangeSelectedCellValue()
        {
            var value = matrix.Rows[selectedCell.RowIndex][selectedCell.ColumnIndex];
            matrix.Rows[selectedCell.RowIndex][selectedCell.ColumnIndex] = AvailableCellValues.GetNext(value);
        }

        public string GetNextCellValue(string value)
        {
            if (AvailableCellValues.Contains(value))
            {
                return AvailableCellValues.GetNext(value);
            }

            return value;
        }
    }
}
