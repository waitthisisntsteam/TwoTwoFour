using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTwoFour
{
    public class Vertex<T>
    {
        public List<Edge<T>> Neighbors { get; set; }
        public int NeighborCount => Neighbors.Count;
        public T Data;

        public Vertex(T data)
        {
            Data = data;
            Neighbors = new();
        }
    }
}
