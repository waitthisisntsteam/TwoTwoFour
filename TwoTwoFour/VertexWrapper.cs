using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTwoFour
{
    public class VertexWrapper<T>
    {
        public Vertex<T> Vertex;
        public VertexWrapper<T>? Founder;
        public double DistanceFromStart;
        public double FinalDistance;

        public VertexWrapper(Vertex<T> vertex) 
        { 
            Vertex = vertex;
            Founder = null;
            DistanceFromStart = double.PositiveInfinity;
            FinalDistance = DistanceFromStart;
        }

        public VertexWrapper(Vertex<T> vertex, VertexWrapper<T> founder, double distanceFromStart)
        {
            Vertex = vertex;
            Founder = founder;
            DistanceFromStart = distanceFromStart;
            FinalDistance = double.PositiveInfinity;
        }

        public VertexWrapper(Vertex<T> vertex, VertexWrapper<T> founder, double distanceFromStart, double finaldistance)
        {
            Vertex = vertex;
            Founder = founder;
            DistanceFromStart = distanceFromStart;
            FinalDistance = finaldistance;
        }
    }
}
