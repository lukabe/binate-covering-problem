using Caliburn.Micro;
using BinateCoveringProblem.App.Eventing;
using BinateCoveringProblem.App.Eventing.Events;

namespace BinateCoveringProblem.App.Matrix.Settings
{
    public class MatrixSettingsViewModel : PropertyChangedBase, IMatrixSettingsViewModel
    {
        private static readonly int ColumnsUpperBound = 20;
        private static readonly int ColumnsLowerBound = 1;
        private static readonly int ColumnsDefaultCount = 5;
        private static readonly int RowsUpperBound = 20;
        private static readonly int RowsLowerBound = 1;
        private static readonly int RowsDefaultCount = 5;

        private readonly IEventStream eventStream;

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

        public MatrixSettingsViewModel(IEventStream eventStream)
        {
            this.eventStream = eventStream;
        }

        public void Initialize()
        {
            ColumnsCount = ColumnsDefaultCount;
            RowsCount = RowsDefaultCount;
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

        private void OnColumnsCountChanged()
        {
            eventStream.Publish(new MatrixSizeChanged
            {
                ColumnsCount = ColumnsCount
            });
        }

        private void OnRowsCountChanged()
        {
            eventStream.Publish(new MatrixSizeChanged
            {
                RowsCount = RowsCount
            });
        }
    }
}
