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

        public Synapse(Neuron start, Neuron end) : this(start, end, 1) { }

        public Synapse(Neuron start, Neuron end, float weight)
        {
            Start = start;
            End = end;
            Weight = weight;

            start.Succesors.Add(this);
            end.Predecesors.Add(this);
        }

        public override string ToString()
        {
            return $"{Start} --> {End} @ {Weight}";
        }
    }
}
