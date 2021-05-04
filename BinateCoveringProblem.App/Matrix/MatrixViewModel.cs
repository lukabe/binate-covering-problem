using System.Data;
using System.Linq;
using Caliburn.Micro;
using System.Windows.Input;
using System.Windows.Controls;
using BinateCoveringProblem.App.Eventing;
using BinateCoveringProblem.App.Eventing.Events;
using BinateCoveringProblem.App.Matrix.Representation;

namespace BinateCoveringProblem.App.Matrix
{
    public class MatrixViewModel : PropertyChangedBase, IMatrixViewModel
    {
        private readonly IMatrixRepresentation matrixRepresentation;
        private readonly IEventStream eventStream;
        
        private DataView matrixView;
        public DataView MatrixView
        {
            get
            {
                return matrixView;
            }
            set
            {
                if (value == matrixView)
                    return;

                matrixView = value;
                NotifyOfPropertyChange(() => MatrixView);
            }
        }

        public MatrixViewModel(IMatrixRepresentation matrixRepresentation, IEventStream eventStream)
        {
            this.matrixRepresentation = matrixRepresentation;
            this.eventStream = eventStream;

            eventStream.Subscribe<MatrixSizeChanged>(OnMatrixSizeChanged);
        }

        public DataTable ToTable() => MatrixView.ToTable();

        /// <summary>
        /// Raises on each cell click and changes the value view of the selected cell
        /// </summary>
        public void ChangeCellValueView(MouseButtonEventArgs e)
        {
            var cell = e.OriginalSource as TextBlock;
            if (cell != null)
            {
                var value = cell.Text;
                cell.Text = matrixRepresentation.GetNextCellValue(value);
            }
        }

        /// <summary>
        /// Raises on each change of the selected cell
        /// </summary>
        public void OnSelectedCellChanged(SelectedCellsChangedEventArgs e)
        {
            var cell = e.AddedCells.FirstOrDefault();

            if (cell.IsValid)
            {
                var row = (cell.Item as DataRowView).Row;

                var rowIndex = matrixRepresentation.GetRowIndex(row);
                var columnIndex = cell.Column.DisplayIndex;

                matrixRepresentation.ChangeSelectedCell(rowIndex, columnIndex);
                matrixRepresentation.ChangeSelectedCellValue();
                
                eventStream.Publish(new MatrixChanged());
            }
        }

        /// <summary>
        /// Raises on selecting the currently selected cell
        /// </summary>
        public void OnSelectedCellSelected()
        {
            matrixRepresentation.ChangeSelectedCellValue();
            
            eventStream.Publish(new MatrixChanged());
        }

        /// <summary>
        /// Raises on each matrix size change (as many times as there are rows)
        /// </summary>
        public void OnLoadingRow(DataGridRowEventArgs e)
        {
            var index = e.Row.GetIndex() + 1;
            e.Row.Header = $"y{index}";
        }

        private void OnMatrixSizeChanged(MatrixSizeChanged e)
        {
            if (e.ColumnsCount.HasValue)
            {
                matrixRepresentation.ChangeColumnsCount(e.ColumnsCount.Value);
            }

            if (e.RowsCount.HasValue)
            {
                matrixRepresentation.ChangeRowsCount(e.RowsCount.Value);
            }

            MatrixView = matrixRepresentation.ToDataView();
            eventStream.Publish(new MatrixChanged());
        }
    }
}
