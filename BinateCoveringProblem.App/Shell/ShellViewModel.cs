using Caliburn.Micro;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;

namespace BinateCoveringProblem.App.Shell
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            
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

        private DataView testSource;
        public DataView TestSource
        {
            get
            {
                return testSource;
            }
            set
            {
                if (value == testSource)
                    return;

                testSource = value;
                NotifyOfPropertyChange(() => TestSource);
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

            TestSource = data.DefaultView;
        }

        public void ChangeCellValue(MouseButtonEventArgs e)
        {
            var cell = e.OriginalSource as TextBlock;
            if (cell != null)
            {
                var value = cell.Text;
                cell.Text = value == "0" ? "1" : "0";
            }
        }
    }
}
