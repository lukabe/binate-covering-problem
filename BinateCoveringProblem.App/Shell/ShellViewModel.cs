using BinateCoveringProblem.App.Shell.Matrix;
using Caliburn.Micro;

namespace BinateCoveringProblem.App.Shell
{
    public class ShellViewModel : Screen
    {
        private static readonly int ColumnsUpperBound = 20;
        private static readonly int ColumnsLowerBound = 1;
        private static readonly int ColumnsDefaultCount = 5;
        private static readonly int RowsUpperBound = 20;
        private static readonly int RowsLowerBound = 1;
        private static readonly int RowsDefaultCount = 5;

        public IMatrixViewModel Matrix { get; }

        public ShellViewModel(IMatrixViewModel matrix)
        {
            this.Matrix = matrix;

            ColumnsCount = ColumnsDefaultCount;
            RowsCount = RowsDefaultCount;
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

        private void OnColumnsCountChanged()
        {
            Matrix.ChangeColumnsCount(ColumnsCount);
        }

        private void OnRowsCountChanged()
        {
            Matrix.ChangeRowsCount(RowsCount);
        }

        public void IncreaseColumnsCount()
        {
            if (ColumnsCount == ColumnsUpperBound)
            {
                return;
            }

            ColumnsCount++;
        }

        public void DecreaseColumnsCount()
        {
            if (ColumnsCount == ColumnsLowerBound)
            {
                return;
            }
                
            ColumnsCount--;
        }

        public void IncreaseRowsCount()
        {
            if (RowsCount == RowsUpperBound)
            {
                return;
            }

            RowsCount++;
        }

        public void DecreaseRowsCount()
        {
            if (RowsCount == RowsLowerBound)
            {
                return;
            }

            RowsCount--;
        }
    }
}
