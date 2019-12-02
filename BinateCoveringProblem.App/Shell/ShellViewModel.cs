using BinateCoveringProblem.App.Extensions;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;

namespace BinateCoveringProblem.App.Shell
{
    public class ShellViewModel : Screen
    {
        private readonly string[] availableValues = new string[] { "0", "1", "-1" };

        public ShellViewModel()
        {
            RowsCount = 5;
            ColumnsCount = 5;
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
                OnSourceChanged();
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
                OnSourceChanged();
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

        private void OnSourceChanged()
        {
            var data = new DataTable();

            var columns = new List<string>();
            for (int i = 1; i <= ColumnsCount; i++)
            {
                data.Columns.Add(i.ToString());
                columns.Add("0");
            }

            for (int i = 1; i <= RowsCount; i++)
            {
                var row = data.NewRow();
                data.Rows.Add(columns.ToArray());
            }

            InputMatrix = data.DefaultView;
        }

        public void ChangeCellValue(MouseButtonEventArgs e)
        {
            var cell = e.OriginalSource as TextBlock;
            if (cell != null)
            {
                var value = cell.Text;

                cell.Text = availableValues.GetNext(value);
            }
        }

        public void IncreaseColumnsCount()
        {
            ColumnsCount++;
        }

        public void DecreaseColumnsCount()
        {
            ColumnsCount--;
        }

        public void IncreaseRowsCount()
        {
            RowsCount++;
        }

        public void DecreaseRowsCount()
        {
            RowsCount--;
        }
    }
}
