using System.Data;
using System.Linq;
using Caliburn.Micro;
using System.Windows.Input;
using System.Windows.Controls;

namespace BinateCoveringProblem.App.Shell.Matrix
{
    public class MatrixViewModel : PropertyChangedBase, IMatrixViewModel
    {
        private readonly IMatrixRepresentation matrixRepresentation;
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

        public MatrixViewModel(IMatrixRepresentation matrixRepresentation)
        {
            this.matrixRepresentation = matrixRepresentation;
        }

        public void ChangeColumnsCount(int count)
        {
            matrixRepresentation.ChangeColumnsCount(count);

            MatrixView = matrixRepresentation.ToDataView;
        }

        public void ChangeRowsCount(int count)
        {
            matrixRepresentation.ChangeRowsCount(count);

            MatrixView = matrixRepresentation.ToDataView;
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
            }
        }

        /// <summary>
        /// Raises on selecting the currently selected cell
        /// </summary>
        public void OnSelectedCellSelected()
        {
            matrixRepresentation.ChangeSelectedCellValue();
        }
    }
}
