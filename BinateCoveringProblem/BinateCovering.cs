using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinateCoveringProblem
{
    class BinateCovering : UnateCovering
    {
        private bool terminalCase;

        public override List<int> FindCovering(Dictionary<int, List<int>> F, List<int> currSol, List<int> b, RichTextBox rich)
        {
            terminalCase = false;
            int lenCurrSol = currSol.Count();

            rich.Text += PrintDictionary(F) + "\n";
            Reduce(F, currSol, rich);
            /*
            rich.Text += PrintDictionary(F);
            rich.Text += "CurrSol = { " + PrintList(currSol);
            */
            Dictionary<int, List<int>> remMin = RemoveAllMinusRows(F);

            MyGraph graph = new MyGraph(remMin);
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

            if (terminalCase)
            {
                rich.Text += "Terminal case - covering is impossible\n";
                return b;
            }

            rich.Text += "\nWeights calculation:\n";

            WeightsCalculate wCalc = new WeightsCalculate(remMin);
            int c = wCalc.ChosenCol;
            //
            rich.Text += "MaxColWeight: " + wCalc.MaxValue + "\n";
            rich.Text += "ChosenCol: " + c + "\n";
            //

            rich.Text += "\nOption 1:\n";

            Dictionary<int, List<int>> Fn1 = SelectCol(F, c);

            List<int> x = AddSelectColToCurrSol(currSol, c);

            rich.Text += "CurrSol = { " + PrintList(x) + "\n";

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
            List<int> currSol0 = FindCovering(Fn0, currSol, b, rich);
            if (currSol0.Count() < b.Count())
            {
                b = currSol0;
            }

            return b;
        }

        // reduce algorithm
        protected override void Reduce(Dictionary<int, List<int>> F, List<int> currSol, RichTextBox rich)
        {
            Dictionary<int, List<int>> A;
            do
            {
                A = new Dictionary<int, List<int>>(F);
                EssCol(F, currSol, rich);
                DomRow(F, rich);
                DomCol(F, rich);
            }
            while (F != null & !A.OrderBy(pair => pair.Key).SequenceEqual(F.OrderBy(pair => pair.Key)) & !terminalCase);
        }

        // essential column reduce
        protected override void EssCol(Dictionary<int, List<int>> F, List<int> currSol, RichTextBox rich)
        {
            int k;
            int l;
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();

            foreach (int i in F.Keys)
            {
                if (F[i].Count() == 1)
                {
                    //
                    k = F[i].First();

                    foreach (int j in F.Keys)
                    {
                        if (F[j].Count() == 1)
                        {
                            l = F[j].First();
                            if (l == -k)
                            {
                                terminalCase = true;
                                break;
                            }
                        }
                    }

                    if (terminalCase == true)
                    {
                        break;
                    }
                    else
                    {
                        if (k > 0)
                        {
                            currSol.Add(k);
                        }

                        foreach (int j in F.Keys)
                        {
                            if (!F[j].Contains(k))
                            {
                                newF.Add(j, F[j]);
                            }

                            foreach (int c in newF.Keys)
                            {
                                if (newF[c].Contains(-k))
                                {
                                    newF[c].Remove(-k);
                                }
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
        }

        // dominated row reduce
        protected override void DomRow(Dictionary<int, List<int>> F, RichTextBox rich)
        {
            base.DomRow(F, rich);
        }

        // dominated column reduce
        protected override void DomCol(Dictionary<int, List<int>> F, RichTextBox rich)
        {
            MyMatrix matrix = new MyMatrix();
            Dictionary<int, List<int>> revF = matrix.ReverseDictionary(F);

            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> newRevF = new Dictionary<int, List<int>>();

            bool toRemove;

            foreach (int i in revF.Keys)
            {
                foreach (int j in revF.Keys)
                {
                    toRemove = true;
                    if (i != j)
                    {
                        foreach (int c in revF[j])
                        {
                            if (c > 0)
                            {
                                if (!revF[i].Contains(c))
                                {
                                    toRemove = false;
                                    break;
                                }
                            }
                        }

                        if (toRemove)
                        {
                            foreach (int c in revF[i])
                            {
                                if (c < 0)
                                {
                                    if (!revF[j].Contains(c))
                                    {
                                        toRemove = false;
                                        break;
                                    }
                                }
                            }

                            if (toRemove)
                            {
                                foreach (int c in revF[j])
                                {
                                    if (c < 0)
                                    {
                                        foreach (int e in revF.Keys)
                                        {
                                            if (e != j)
                                            {
                                                if (revF[e].Contains(c))
                                                {
                                                    revF[e].Remove(c);
                                                }
                                                else if (revF[e].Contains(-c))
                                                {
                                                    revF[e].Remove(-c);
                                                }
                                            }
                                        }
                                    }
                                }

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
                }
                if (newRevF.OrderBy(pair => pair.Key).SequenceEqual(F.OrderBy(pair => pair.Key)))
                {
                    rich.Text += "DomCol -> " + PrintDictionary(F) + "\n";
                    break;
                }
            }
        }

        /*public bool TerminalCase
        {
            get { return terminalCase; }
        }*/

        protected override Dictionary<int, List<int>> SelectCol(Dictionary<int, List<int>> F, int c)
        {
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();

            foreach (int i in F.Keys)
            {
                if (!F[i].Contains(c))
                {
                    newF.Add(i, F[i]);
                }
            }

            MyMatrix m = new MyMatrix();
            Dictionary<int, List<int>> revNewF = m.ReverseDictionary(newF);
            revNewF.Remove(c);

            Dictionary<int, List<int>> rev2NewF = m.ReverseDictionary(revNewF);

            return rev2NewF;
        }

        protected override List<int> AddSelectColToCurrSol(List<int> currSol, int c)
        {
            return base.AddSelectColToCurrSol(currSol, c);
        }

        protected override Dictionary<int, List<int>> RemoveCol(Dictionary<int, List<int>> F, int c)
        {
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();

            foreach (int i in F.Keys)
            {
                if (!F[i].Contains(-c))
                {
                    newF.Add(i, F[i]);
                }
            }

            foreach (int j in newF.Keys)
            {
                if (newF[j].Contains(c))
                {
                    newF[j].Remove(c);
                }
            }

            return newF;
        }

        private Dictionary<int, List<int>> RemoveAllMinusRows(Dictionary<int, List<int>> F)
        {
            Dictionary<int, List<int>> newF = new Dictionary<int, List<int>>();

            foreach (KeyValuePair<int, List<int>> pair in F)
            {
                newF.Add(pair.Key, pair.Value);
            }

            foreach (int i in F.Keys)
            {
                foreach (int j in F[i])
                {
                    if (j < 0)
                    {
                        newF.Remove(i);
                        break;
                    }
                }
            }

            return newF;
        }
    }
}
