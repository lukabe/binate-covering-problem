using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinateCoveringProblem
{
    class MyGraph
    {
        private Dictionary<int, List<int>> C; // connections dictionary
        private int numberNodes;
        //private int numberEdges;
        private int numberNeighbors;

        public MyGraph(Dictionary<int, List<int>> F)
        {
            LoadGraphConnections(F);
            SetNumberNodes();
            //SetNumberEdges();
        }

        private void LoadGraphConnections(Dictionary<int, List<int>> F)
        {
            C = new Dictionary<int, List<int>>();
            int x = -1;
            int y = -1;

            foreach (int i in F.Keys)
            {
                x++;
                C.Add(x, new List<int>());

                foreach (int j in F.Keys)
                {
                    y++;

                    if (i != j)
                    {
                        if (F[i].Intersect(F[j]).Count() == 0)
                        {
                            C[x].Add(y);
                        }
                    }
                }

                y = -1;
            }
        }

        private void SetNumberNodes()
        {
            numberNodes = C.Keys.Count();
        }

        /*private void SetNumberEdges()
        {
            Dictionary<int, List<int>> newC = new Dictionary<int, List<int>>();
            foreach (KeyValuePair<int, List<int>> pair in C)
            {
                newC.Add(pair.Key, pair.Value);
            }
            int numEdges = 0;

            foreach(int i in newC.Keys)
            {
                numEdges += newC[i].Count();

                foreach(int j in newC.Keys)
                {
                    newC[j].Remove(i);
                }
            }

            numberEdges = numEdges;
        }*/

        public int NumberNodes
        {
            get { return numberNodes; }
        }

        /*public int NumberEdges
        {
            get { return numberEdges; }
        }*/

        public int NumberNeighbors(int node)
        {
            numberNeighbors = C[node].Count();
            return numberNeighbors;
        }

        public bool AreAdjacent(int nodeA, int nodeB)
        {
            if (C[nodeA].Contains(nodeB))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string PrintDictionaryConnections()
        {
            string s = "C = { ";

            foreach (int i in C.Keys)
            {
                s += i + ": [ ";
                foreach (int j in C[i])
                {
                    s += j + " ";
                }
                s += "] ";
            }
            s += "} \n";

            return s;
        }
    }
}
