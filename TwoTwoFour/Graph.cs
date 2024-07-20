using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TwoTwoFour
{
    public class Graph<T>
    {
        public List<Vertex<T>> Verticies { get; set; }
        public int VertexCount => Verticies.Count;

        public Graph()
        {
            Verticies = new();
        }

        public void AddVertex(Vertex<T> vertex) 
        { 
            if (vertex != null && vertex.NeighborCount <= 0 && !Verticies.Contains(vertex))
            {
                Verticies.Add(vertex);
            }
        }

        public bool AddEdge(Vertex<T> start, Vertex<T> end, double weight)
        {
            if (start != null && end != null && Verticies.Contains(start) && Verticies.Contains(end))
            {
                start.Neighbors.Add(new Edge<T>(start, end, weight));
                end.Neighbors.Add(new Edge<T>(end, start, weight));
                return true;
            }
            return false;
        }

        public Vertex<T>? Search(T value)
        {
            for (int i = 0; i < VertexCount; i++)
            {
                if (Verticies[i].Data.Equals(value))
                {
                    return Verticies[i];
                }
            }
            return null;
        }

        public VertexWrapper<T> BFSSelector(List<VertexWrapper<T>> frontier)
        {
            VertexWrapper<T> popped = frontier[0];
            frontier.RemoveAt(0);
            return popped;
        }
        public VertexWrapper<T> DFSSelector(List<VertexWrapper<T>> frontier)
        {
            VertexWrapper<T> dequeued = frontier[^1];
            frontier.RemoveAt(frontier.Count - 1);
            return dequeued;
        }
        public VertexWrapper<T> DijkstraSelector(List<VertexWrapper<T>> frontier)
        {           
            VertexWrapper<T>? optimalVertexWrapper = frontier[0];
            double minimumDistance = optimalVertexWrapper.DistanceFromStart;
            int optimalVertexWrapperIndex = 0;

            for (int i = 1; i < frontier.Count; i++)
            {
                if (minimumDistance > frontier[i].DistanceFromStart)
                {
                    minimumDistance = frontier[i].DistanceFromStart;
                    optimalVertexWrapper = frontier[i];
                    optimalVertexWrapperIndex = i;
                }
            }

            frontier.RemoveAt(optimalVertexWrapperIndex);
            return optimalVertexWrapper;
        }
        public VertexWrapper<T> AStarSelector(List<VertexWrapper<T>> frontier)
        {
            VertexWrapper<T>? optimalVertexWrapper = frontier[0];
            double minimumDistance = optimalVertexWrapper.DistanceFromStart;
            int optimalVertexWrapperIndex = 0;

            for (int i = 1; i < frontier.Count; i++)
            {
                if (minimumDistance > frontier[i].FinalDistance)
                {
                    minimumDistance = frontier[i].FinalDistance;
                    optimalVertexWrapper = frontier[i];
                    optimalVertexWrapperIndex = i;
                }
            }

            frontier.RemoveAt(optimalVertexWrapperIndex);
            return optimalVertexWrapper;
        }
        public List<Vertex<T>>? TraversalSearch(Vertex<T> start, Vertex<T> end, Func<List<VertexWrapper<T>>, VertexWrapper<T>> selector)
        {
            if (Verticies.Contains(start) && Verticies.Contains(end))
            {
                List<Vertex<T>> result = new();
                List<VertexWrapper<T>> frontier = new();
                HashSet<Vertex<T>> visited = new();

                VertexWrapper<T> current = new VertexWrapper<T>(start, null, 0);
                frontier.Add(current);

                visited.Add(start);
                while (frontier.Count > 0)
                {
                    current = selector(frontier);

                    foreach (var n in current.Vertex.Neighbors)
                    {
                        if (!visited.Contains(n.EndingPoint))
                        {
                            visited.Add(n.EndingPoint);

                            double tentativeDistance = current.DistanceFromStart + n.Weight;
                            frontier.Add(new VertexWrapper<T>(n.EndingPoint, current, tentativeDistance));
                        }
                        
                        //Recursive Descent here, pop off path just done, then  get the next, repeat
                        if (current.Vertex.Equals(end))
                        {
                            VertexWrapper<T> runner = current;
                            while (runner != null)
                            {
                                result.Add(runner.Vertex);
                                runner = runner.Founder;
                            }
                            result.Reverse();
                            return result;
                        }
                    }
                }
            }
            return null;
        }
    }
}
