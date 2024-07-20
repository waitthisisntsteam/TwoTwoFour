using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTwoFour
{
    public class Edge <T>
    {
        public Vertex<T> StartingPoint;
        public Vertex<T> EndingPoint;
        public double Weight;

        public Edge(Vertex<T> startingPoint, Vertex<T> endingPoint, double weight)
        => (StartingPoint, EndingPoint, Weight) = (startingPoint, endingPoint, weight);
    }
}
