using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinateCoveringProblem
{
    class MyMatrix
    {
        private Dictionary<int, List<int>> _F;
        private List<int> _b;
        public List<int> currSol;

        public MyMatrix() { }

        public MyMatrix(DataGridView matrix)
        {
            ConvertToDictionary(matrix);
            Initialize_b(matrix);
            currSol = new List<int>();
        }

        private void Initialize_b(DataGridView matrix)
        {
            _b = new List<int>();
            for (int i = 1; i <= matrix.ColumnCount; i++)
            {
                _b.Add(i);
            }
        }

        // convert matrix to dictionary
        private void ConvertToDictionary(DataGridView matrix)
        {
            _F = new Dictionary<int, List<int>>();
            int value;

            for (int i = 0; i < matrix.RowCount; i++)
            {
                _F.Add(i + 1, new List<int> { });

                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    if (matrix.Rows[i].Cells[j].Value != null)
                    {
                        value = int.Parse(matrix.Rows[i].Cells[j].Value.ToString());
                        if (value == 1)
                        {
                            _F[i + 1].Add(j + 1);
                        }
                        else if (value == -1)
                        {
                            _F[i + 1].Add(-j - 1);
                        }
                    }
                }
            }
        }

        public List<int> b
        {
            get { return _b; }
        }

        public Dictionary<int, List<int>> F
        {
            get { return _F; }
        }

        // check matrix is binate or unate
        public bool CheckBinate()
        {
            foreach (int i in F.Keys)
            {
                foreach (int j in F[i])
                {
                    if (j < 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // reverse dictionary
        public Dictionary<int, List<int>> ReverseDictionary(Dictionary<int, List<int>> F)
        {
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();
            int k;
            int l;

            foreach (int i in F.Keys)
            {
                foreach (int j in F[i])
                {
                    if (j < 0)
                    {
                        k = -j;
                        l = -i;
                    }
                    else
                    {
                        k = j;
                        l = i;
                    }

                    if (!newF.ContainsKey(k))
                    {
                        newF.Add(k, new List<int>());
                    }
                    newF[k].Add(l);
                }
            }

            newF = SortDictionaryByKeys(newF);
            return newF;
        }

        // sort dictionary by keys
        private Dictionary<int, List<int>> SortDictionaryByKeys(Dictionary<int, List<int>> F)
        {
            var newF = new Dictionary<int, List<int>>();
            var list = F.Keys.ToList();
            list.Sort();

            foreach (var key in list)
            {
                newF.Add(key, F[key]);
            }

            return newF;
        }
    }
}
