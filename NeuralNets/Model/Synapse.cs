using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model
{
    public class Synapse
    {
        public Neuron Start { get; set; }
        public Neuron End { get; set; }
        public float Weight { get; set; }
        public float Output => Start.Output * Weight;
        public Synapse(Neuron a, Neuron b, float weight)
        {
            Start = a;
            End = b;
            Weight = weight;

            a.Succesors.Add(this);
            b.Predecesors.Add(this);
        }
    }
}
