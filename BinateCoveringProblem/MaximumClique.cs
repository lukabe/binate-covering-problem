using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinateCoveringProblem
{
    // greedy algorithm to find maximum clique by James McCaffrey
    class MaximumClique
    {
        private Random random = null;

        public List<int> FindMaxClique(MyGraph graph, int maxTime, int targetCliqueSize)
        {
            // you may want to coment-out WriteLine statements for large graphs.
            List<int> clique = new List<int>();
            random = new Random(1);
            int time = 0;
            int timeBestClique = 0;
            int timeRestart = 0;
            int nodeToAdd = -1;
            int nodeToDrop = -1;

            int randomNode = random.Next(0, graph.NumberNodes);
            Console.WriteLine("Adding node " + randomNode);
            clique.Add(randomNode);

            List<int> bestClique = new List<int>();
            bestClique.AddRange(clique);
            int bestSize = bestClique.Count;
            timeBestClique = time;

            List<int> possibleAdd = MakePossibleAdd(graph, clique); // nodes which will increase size of clique
            List<int> oneMissing = MakeOneMissing(graph, clique);

            while (time < maxTime && bestSize < targetCliqueSize)
            {
                ++time;

                bool cliqueChanged = false; // to control branching logic in loop
                if (possibleAdd.Count > 0) // 
                {
                    nodeToAdd = GetNodeToAdd(graph, possibleAdd); // node from possibleAdd which is most connected to nodes in possibleAdd
                    Console.WriteLine("Adding node " + nodeToAdd);
                    clique.Add(nodeToAdd);
                    clique.Sort();
                    cliqueChanged = true;
                    if (clique.Count > bestSize)
                    {
                        bestSize = clique.Count;
                        bestClique.Clear();
                        bestClique.AddRange(clique);
                        timeBestClique = time;
                    }
                } // add node

                if (cliqueChanged == false)
                {
                    if (clique.Count > 0)
                    {
                        nodeToDrop = GetNodeToDrop(graph, clique, oneMissing); // find node in clique which generate max increase in possibleAdd set. if more than one, pick one at random
                        Console.WriteLine("Dropping node " + nodeToDrop);
                        clique.Remove(nodeToDrop);
                        clique.Sort();
                        cliqueChanged = true;
                    }
                } // drop node

                int restart = 2 * bestSize;
                if (time - timeBestClique > restart && time - timeRestart > restart) // restart? if it's been a while since we found a new best clique or did a restart . . .
                {
                    Console.WriteLine("\nRestarting\n");
                    timeRestart = time;
                    int seedNode = random.Next(0, graph.NumberNodes);
                    clique.Clear();
                    Console.WriteLine("Adding node " + seedNode);
                    clique.Add(seedNode);
                } // restart

                possibleAdd = MakePossibleAdd(graph, clique);
                oneMissing = MakeOneMissing(graph, clique);

                //ValidateState(graph, clique, possibleAdd, oneMissing);

            } // main processing loop


            return bestClique;
        } // FindMaxClique

        private List<int> MakePossibleAdd(MyGraph graph, List<int> clique)
        {
            // create list of nodes in graph which are connected to all nodes in clique and therefore will form a larger clique
            // calls helper FormsALargerClique
            List<int> result = new List<int>();

            for (int i = 0; i < graph.NumberNodes; ++i) // each node in graph
            {
                if (FormsALargerClique(graph, clique, i) == true)
                    result.Add(i);
            }
            return result; // could be empty List
        } // MakePossibleAdd

        private bool FormsALargerClique(MyGraph graph, List<int> clique, int node)
        {
            // is node connected to all nodes in clique?
            for (int i = 0; i < clique.Count; ++i) // compare node against each member of clique
            {
                //if (clique[i] == node) return false; // node is aleady in clique so node will not form a larger clique
                if (graph.AreAdjacent(clique[i], node) == false) return false; // node is not connected to one of the nodes in the clique
            }
            return true; // passed all checks
        } // FormsALargerClique

        private int GetNodeToAdd(MyGraph graph, List<int> possibleAdd)
        {
            // find node from a List of allowed and posible add which has max degree in posibleAdd
            // there could be more than one, if so, pick one at random
            //if (possibleAdd == null) throw new Exception("List possibleAdd is null in GetNodeToAdd");
            //if (possibleAdd.Count == 0) throw new Exception("List possibleAdd has Count 0 in GetNodeToAdd");

            if (possibleAdd.Count == 1) // there is only 1 node to choose from
                return possibleAdd[0];

            // examine each node in possibleAdd to find the max degree in possibleAdd (because there could be several nodes tied with max degree)
            int maxDegree = 0;
            for (int i = 0; i < possibleAdd.Count; ++i)
            {
                int currNode = possibleAdd[i];
                int degreeOfCurrentNode = 0;
                for (int j = 0; j < possibleAdd.Count; ++j) // check each node in possibleAdd
                {
                    int otherNode = possibleAdd[j];
                    if (graph.AreAdjacent(currNode, otherNode) == true) ++degreeOfCurrentNode;
                }
                if (degreeOfCurrentNode > maxDegree)
                    maxDegree = degreeOfCurrentNode;
            }

            // now rescan possibleAdd and grab all nodes which have maxDegree
            List<int> candidates = new List<int>();
            for (int i = 0; i < possibleAdd.Count; ++i)
            {
                int currNode = possibleAdd[i];
                int degreeOfCurrentNode = 0;
                for (int j = 0; j < possibleAdd.Count; ++j) // check each node in possibleAdd
                {
                    int otherNode = possibleAdd[j];
                    if (graph.AreAdjacent(currNode, otherNode) == true) ++degreeOfCurrentNode;
                }
                if (degreeOfCurrentNode == maxDegree)
                    candidates.Add(currNode);
            }

            //if (candidates.Count == 0) throw new Exception("candidates List has size 0 just before return in GetNodeToAdd");
            return candidates[random.Next(0, candidates.Count)]; // if candidates has Count 1 we'll get that one node
        } // GetNodeToAdd

        private int GetNodeToDrop(MyGraph graph, List<int> clique, List<int> oneMissing)
        {
            // get a node from clique set, which if dropped, gives the largest increase in PA set size
            // we use the oneMissingb set to determine which clique node to pick
            //if (clique == null) throw new Exception("clique is null in GetNodeToDrop");
            //if (clique.Count == 0) throw new Exception("clique has Count 0 in GetNodeToDrop");

            if (clique.Count == 1)
                return clique[0];

            // scan each node in clique and determine the max possibleAdd size if node removed
            int maxCount = 0; // see explanation below
            for (int i = 0; i < clique.Count; ++i) // each node in clique nodes List
            {
                int currCliqueNode = clique[i];
                int countNotAdjacent = 0;
                for (int j = 0; j < oneMissing.Count; ++j) // each node in the one missing list
                {
                    int currOneMissingNode = oneMissing[j];
                    if (graph.AreAdjacent(currCliqueNode, currOneMissingNode) == false) // we like this
                        ++countNotAdjacent;

                    // if currCliqueNode is not connected to omNode then currCliqueNode is the 'missing'
                    // it would be good to drop this cliqueNode because after dropped from clique
                    // the remaining nodes in the clique will all be connected to the omNode
                    // and so the omNode would become a posibleAdd node and increase PA set size
                    // So the best node to drop from clique will be the one which is least connected
                    // to the nodes in OM
                }

                if (countNotAdjacent > maxCount)
                    maxCount = countNotAdjacent;
            }

            // at this point we know what the max-not-connected count is but there could be several clique nodes which give that size
            List<int> candidates = new List<int>();
            for (int i = 0; i < clique.Count; ++i) // each node in clique
            {
                int currCliqueNode = clique[i];
                int countNotAdjacent = 0;
                for (int j = 0; j < oneMissing.Count; ++j) // each node in the one missing list
                {
                    int currOneMissingNode = oneMissing[j];
                    if (graph.AreAdjacent(currCliqueNode, currOneMissingNode) == false)
                        ++countNotAdjacent;
                }

                if (countNotAdjacent == maxCount) // cxurrent clique node has max count not connected
                    candidates.Add(currCliqueNode);
            }

            //if (candidates.Count == 0) throw new Exception("candidates List has size 0 just before return in GetNodeToDropFromAllowedInClique");
            return candidates[random.Next(0, candidates.Count)]; // must have size of at least 1

        } // GetNodeToDrop

        private List<int> MakeOneMissing(MyGraph graph, List<int> clique)
        {
            // make a list of nodes in graph which are connected to all but one of the nodes in clique
            int count; // number of nodes in clique which are connected to a candidate node. if final count == (clique size - 1) then candidate is a winner
            List<int> result = new List<int>();

            for (int i = 0; i < graph.NumberNodes; ++i) // each node in graph i a candidate
            {
                count = 0;
                if (graph.NumberNeighbors(i) < clique.Count - 1) continue; // node i has too few neighbors to possibly be connected to all but 1 node in clique
                                                                           //if (LinearSearch(clique, i) == true) continue; // node i is in clique. clique is not sorted so use LinearSearch -- consider Sort + BinarySearch
                if (clique.BinarySearch(i) >= 0) continue;
                for (int j = 0; j < clique.Count; ++j) // count how many nodes in clique are connected to candidate i
                {
                    if (graph.AreAdjacent(i, clique[j]))
                        ++count;
                }
                if (count == clique.Count - 1)
                    result.Add(i);
            } // each candidate node i
            return result;

        } // MakeOneMissing

        public string PrintMaxClique(List<int> list)
        {
            string s = "[ ";
            foreach (int i in list)
            {
                s += i + " ";
            }

            s += "] \n";
            return s;
        }

        public int PrintLenMaxClique(List<int> list)
        {
            int len = list.Count();
            return len;
        }
    }
}
