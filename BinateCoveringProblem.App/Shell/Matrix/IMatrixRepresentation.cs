using System.Data;

namespace BinateCoveringProblem.App.Shell.Matrix
{
    public interface IMatrixRepresentation
    {
        void ChangeColumnsCount(int count);
        void ChangeRowsCount(int count);
        int GetRowIndex(DataRow row);
        void ChangeSelectedCell(int rowIndex, int columnIndex);
        void ChangeSelectedCellValue();
        string GetNextCellValue(string value);
        DataView ToDataView();
    }
}
