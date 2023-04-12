using NeuralNets.Model.Neural.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural
{
    public class Connection
    {
        public Node Start { get; set; }
        public Node End { get; set; }
        public float Weight { get; set; }
        public float Bias { get; set; }
        public float Input => Start.Output;
        public float Output => Input * Weight + Bias;

        public Connection(Node start, Node end, float weight, float bias)
        {
            Start = start;
            End = end;
            Weight = weight;
            Bias = bias;

            Start.Children.Add(this);
            End.Parents.Add(this);
        }
    }
}
