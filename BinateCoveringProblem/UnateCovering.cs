using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinateCoveringProblem
{
    class UnateCovering
    {
        public virtual List<int> FindCovering(Dictionary<int, List<int>> F, List<int> currSol, List<int> b, RichTextBox rich)
        {
            int lenCurrSol = currSol.Count();

            rich.Text += PrintDictionary(F) + "\n";
            Reduce(F, currSol, rich);
            /*
            rich.Text += PrintDictionary(F);
            rich.Text += "CurrSol = { " + PrintList(currSol);
            */
            MyGraph graph = new MyGraph(F);
            MaximumClique clique = new MaximumClique();

            int maxTime = 20; // use 20 for small demo file; use about 1,000-10,000 for benchmark problem.
            int targetCliqueSize = graph.NumberNodes; // max-clique size for any graph is the size of the graph -- graph is one giant clique.

            List<int> maxClique = clique.FindMaxClique(graph, maxTime, targetCliqueSize);
            int L = maxClique.Count() + lenCurrSol;

            //rich.Text += "MaxClique = { " + PrintList(maxClique);
            //rich.Text += "\nL: " + L + "\n";


            /*MyMatrix matrix = new MyMatrix();
            Dictionary<int, List<int>> revF = matrix.ReverseDictionary(F);
            //
            rich.Text += "Reverse" + PrintDictionary(revF);
            */

            if (L >= b.Count())
            {
                return b;
            }

            if (F.Keys.Count() == 0)
            {
                return currSol;
            }

            rich.Text += "\nWeights calculation:\n";

            WeightsCalculate wCalc = new WeightsCalculate(F);
            int c = wCalc.ChosenCol;
            //
            rich.Text += "MaxColWeight: " + wCalc.MaxValue + "\n";
            rich.Text += "ChosenCol: " + c + "\n";
            //

            rich.Text += "\nOption 1:\n";

            Dictionary<int, List<int>> Fn1 = SelectCol(F, c);

            List<int> x = AddSelectColToCurrSol(currSol, c);

            rich.Text += "CurrSol = { " + PrintList(currSol) + "\n";

            List<int> currSol1 = FindCovering(Fn1, x, b, rich);
            if (currSol1.Count() < b.Count())
            {
                b = currSol1;
                if (b.Count() == L)
                {
                    return b;
                }
            }

            rich.Text += "\nOption 0:\n";

            Dictionary<int, List<int>> Fn0 = RemoveCol(F, c);
            //
            //rich.Text += "Fn0 " + PrintDictionary(Fn0);
            //
            List<int> currSol0 = FindCovering(Fn0, currSol, b, rich); // currSol -> y
            if (currSol0.Count() < b.Count())
            {
                b = currSol0;
            }

            return b;
        }

        // reduce algorithm
        protected virtual void Reduce(Dictionary<int, List<int>> F, List<int> currSol, RichTextBox rich)
        {
            Dictionary<int, List<int>> A;
            do
            {
                A = new Dictionary<int, List<int>>(F);
                EssCol(F, currSol, rich);
                DomRow(F, rich);
                DomCol(F, rich);
            }
            while (F != null & !A.OrderBy(pair => pair.Key).SequenceEqual(F.OrderBy(pair => pair.Key)));
        }

        // essential column reduce
        protected virtual void EssCol(Dictionary<int, List<int>> F, List<int> currSol, RichTextBox rich)
        {
            int k;
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();

            foreach (int i in F.Keys)
            {
                if (F[i].Count() == 1)
                {
                    k = F[i].First();
                    currSol.Add(k);

                    foreach (int j in F.Keys)
                    {
                        if (!F[j].Contains(k))
                        {
                            newF.Add(j, F[j]);
                        }
                    }

                    F.Clear();
                    foreach (KeyValuePair<int, List<int>> pair in newF)
                    {
                        F.Add(pair.Key, pair.Value);
                    }

                    rich.Text += "EssCol -> " + PrintDictionary(F) + "\n";
                    rich.Text += "CurSol = { " + PrintList(currSol) + "\n";
                    break;
                }
            }
        }

        // dominated row reduce
        protected virtual void DomRow(Dictionary<int, List<int>> F, RichTextBox rich)
        {
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();

            foreach (int i in F.Keys)
            {
                foreach (int j in F.Keys)
                {
                    if (i != j)
                    {
                        if (F[i].Intersect(F[j]).Count() == F[i].Count())
                        {
                            foreach (int k in F.Keys)
                            {
                                if (k != j)
                                {
                                    newF.Add(k, F[k]);
                                }
                            }

                            F.Clear();
                            foreach (KeyValuePair<int, List<int>> pair in newF)
                            {
                                F.Add(pair.Key, pair.Value);
                            }

                            break;
                        }
                    }
                }
                if (newF.OrderBy(pair => pair.Key).SequenceEqual(F.OrderBy(pair => pair.Key)))
                {
                    rich.Text += "DomRow -> " + PrintDictionary(F) + "\n";
                    break;
                }
            }
        }

        // dominated column reduce
        protected virtual void DomCol(Dictionary<int, List<int>> F, RichTextBox rich)
        {
            MyMatrix matrix = new MyMatrix();
            Dictionary<int, List<int>> revF = matrix.ReverseDictionary(F);

            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> newRevF = new Dictionary<int, List<int>>();

            foreach (int i in revF.Keys)
            {
                foreach (int j in revF.Keys)
                {
                    if (i != j)
                    {
                        if (revF[i].Intersect(revF[j]).Count() == revF[j].Count())
                        {
                            foreach (int k in revF.Keys)
                            {
                                if (k != j)
                                {
                                    newF.Add(k, revF[k]);
                                }
                            }


                            F.Clear();
                            newRevF = matrix.ReverseDictionary(newF);
                            foreach (KeyValuePair<int, List<int>> pair in newRevF)
                            {
                                F.Add(pair.Key, pair.Value);
                            }

                            break;
                        }
                    }
                }
                if (newRevF.OrderBy(pair => pair.Key).SequenceEqual(F.OrderBy(pair => pair.Key)))
                {
                    rich.Text += "DomCol -> " + PrintDictionary(F) + "\n";
                    break;
                }
            }
        }

        protected virtual Dictionary<int, List<int>> SelectCol(Dictionary<int, List<int>> F, int c)
        {
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();

            foreach (int i in F.Keys)
            {
                if (!F[i].Contains(c))
                {
                    newF.Add(i, F[i]);
                }
            }

            return newF;
        }

        protected virtual List<int> AddSelectColToCurrSol(List<int> currSol, int c)
        {
            List<int> x = new List<int>();

            foreach (int i in currSol)
            {
                x.Add(i);
            }

            x.Add(c);

            return x;
        }

        protected virtual Dictionary<int, List<int>> RemoveCol(Dictionary<int, List<int>> F, int c)
        {
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();

            foreach (KeyValuePair<int, List<int>> pair in F)
            {
                newF.Add(pair.Key, pair.Value);
            }

            foreach (int i in F.Keys)
            {
                if (newF[i].Contains(c))
                {
                    newF[i].Remove(c);
                }
            }

            return newF;
        }

        public string PrintList(List<int> list)
        {
            string s = "";
            foreach (int i in list)
            {
                s += i + " ";
            }
            s += "}";

            return s;
        }

        public string PrintDictionary(Dictionary<int, List<int>> F)
        {
            string s = "F = { ";
            foreach (int i in F.Keys)
            {
                s += i + ": [ ";
                foreach (int j in F[i])
                {
                    s += j + " ";
                }
                s += "] ";
            }
            s += "}";

            return s;
        }
    }
}
