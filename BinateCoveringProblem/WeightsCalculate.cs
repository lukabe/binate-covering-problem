using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinateCoveringProblem
{
    class WeightsCalculate
    {
        private int chosenCol;
        private double maxValue; //

        public WeightsCalculate(Dictionary<int, List<int>> F)
        {
            ChooseCol(F);
        }

        private void ChooseCol(Dictionary<int, List<int>> F)
        {
            Dictionary<int, double> wR = WeightsRows(F);
            Dictionary<int, double> wC = WeightsColumns(F, wR);

            maxValue = wC.Values.Max(); //

            foreach (int i in wC.Keys)
            {
                if (wC[i] == maxValue)
                {
                    chosenCol = i;
                    break;
                }
            }
        }

        private Dictionary<int, double> WeightsRows(Dictionary<int, List<int>> F)
        {
            Dictionary<int, double> wRows = new Dictionary<int, double>();

            foreach (int i in F.Keys)
            {
                wRows.Add(i, (double)1 / (double)F[i].Count());
            }

            return wRows;
        }

        private Dictionary<int, double> WeightsColumns(Dictionary<int, List<int>> F, Dictionary<int, double> WeightsRows)
        {
            MyMatrix matrix = new MyMatrix();
            Dictionary<int, List<int>> revF = matrix.ReverseDictionary(F);

            Dictionary<int, double> wColumns = new Dictionary<int, double>();
            double weight = 0;

            foreach (int i in revF.Keys)
            {
                foreach (int j in revF[i])
                {
                    weight += WeightsRows[j];
                }

                wColumns.Add(i, weight);
                weight = 0;
            }

            return wColumns;
        }

        public int ChosenCol
        {
            get { return chosenCol; }
        }

        public double MaxValue
        {
            get { return maxValue; }
        }
    }
}
