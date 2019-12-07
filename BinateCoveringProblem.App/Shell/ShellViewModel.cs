using BinateCoveringProblem.App.Extensions;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace BinateCoveringProblem.App.Shell
{
    public class ShellViewModel : Screen
    {
        private readonly string[] availableValues = new string[] { "0", "1", "-1" };
        private DataTable matrix;

        public ShellViewModel()
        {
            matrix = new DataTable();

            ColumnsCount = 5;
            RowsCount = 5;
            
        }

        private int rowsCount;
        public int RowsCount
        {
            get
            {
                return rowsCount;
            }
            set
            {
                if (value == rowsCount)
                    return;

                rowsCount = value;
                OnRowsCountChanged();
                NotifyOfPropertyChange(() => RowsCount);
            }
        }

        private int columnsCount;
        public int ColumnsCount
        {
            get
            {
                return columnsCount;
            }
            set
            {
                if (value == columnsCount)
                    return;

                columnsCount = value;
                OnColumnsCountChanged();
                NotifyOfPropertyChange(() => ColumnsCount);
            }
        }

        private DataView inputMatrix;
        public DataView InputMatrix
        {
            get
            {
                return inputMatrix;
            }
            set
            {
                if (value == inputMatrix)
                    return;

                inputMatrix = value;
                NotifyOfPropertyChange(() => InputMatrix);
            }
        }

        private void OnColumnsCountChanged()
        {
            var currentCount = matrix.Columns.Count;

            if (ColumnsCount == currentCount)
            {
                return;
            }
            else if (ColumnsCount > currentCount)
            {
                for (int i = currentCount + 1; i <= ColumnsCount; i++)
                {
                    var column = new DataColumn(i.ToString())
                    {
                        DefaultValue = "0",
                        ReadOnly = false
                    };

                    matrix.Columns.Add(column);
                }
            }
            else if (ColumnsCount < currentCount)
            {
                for (int i = currentCount; i > ColumnsCount; i--)
                {
                    matrix.Columns.Remove(i.ToString());
                }
            }

            InputMatrix = matrix.AsDataView();
        }

        private void OnRowsCountChanged()
        {
            var currentCount = matrix.Rows.Count;

            if (RowsCount == currentCount)
            {
                return;
            }
            else if (RowsCount > currentCount)
            {
                for (int i = currentCount + 1; i <= RowsCount; i++)
                {
                    matrix.Rows.Add(GetRowFulfillment().ToArray());
                }
            }
            else if (RowsCount < currentCount)
            {
                for (int i = currentCount; i > RowsCount; i--)
                {
                    var index = i - 1;
                    var row = matrix.Rows[index];
                    matrix.Rows.Remove(row);
                }
            }

            InputMatrix = matrix.AsDataView();
        }

        private IEnumerable<string> GetRowFulfillment()
        {
            var columnsCount = matrix.Columns.Count;

            foreach (var column in matrix.Columns)
            {
                yield return "0";
            }
        }

        /// <summary>
        /// Raises on each cell click and changes the value view of the selected cell
        /// </summary>
        public void ChangeCellValueView(MouseButtonEventArgs e)
        {
            var cell = e.OriginalSource as TextBlock;
            if (cell != null)
            {
                var value = cell.Text;
                cell.Text = availableValues.GetNext(value);
            }
        }

        private int selectedRowIndex;
        private int selectedColumnIndex;

        /// <summary>
        /// Raises on each change of the selected cell
        /// </summary>
        public void OnSelectedCellChanged(SelectedCellsChangedEventArgs e)
        {
            var cell = e.AddedCells.FirstOrDefault();

            if (cell.IsValid)
            {
                var row = (cell.Item as DataRowView).Row;

                selectedRowIndex = matrix.Rows.IndexOf(row);
                selectedColumnIndex = cell.Column.DisplayIndex;

                ChangeSelectedCellValue();
            }
        }

        /// <summary>
        /// Raises on selecting the currently selected cell
        /// </summary>
        public void OnSelectedCellSelect()
        {
            ChangeSelectedCellValue();
        }

        private void ChangeSelectedCellValue()
        {
            var value = matrix.Rows[selectedRowIndex][selectedColumnIndex];
            matrix.Rows[selectedRowIndex][selectedColumnIndex] = availableValues.GetNext(value);
        }

        public void IncreaseColumnsCount()
        {
            if (ColumnsCount == 20)
            {
                return;
            }

            ColumnsCount++;
        }

        public void DecreaseColumnsCount()
        {
            if (ColumnsCount == 1)
            {
                return;
            }
                
            ColumnsCount--;
        }

        public void IncreaseRowsCount()
        {
            if (RowsCount == 20)
            {
                return;
            }

            RowsCount++;
        }

        public void DecreaseRowsCount()
        {
            if (RowsCount == 1)
            {
                return;
            }

            RowsCount--;
        }
    }
}
