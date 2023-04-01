using NeuralNets.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model
{
    public class Neuron
    {
        private float? _output { get; set; }
        public int Id { get; set; }
        public float Output => _output ?? GetOutput();
        public float[] Inputs => Predecesors.Select(s => s.Start.Output).ToArray();
        public List<Synapse> Predecesors { get; } = new List<Synapse>();
        public List<Synapse> Succesors { get; } = new List<Synapse>();
        private Func<float, float> _activation { get; set; }
        private Func<float[], float> _aggregation { get; set; }

        public Neuron()
        {
            _activation = defaultActivation;
            _aggregation = defaultAggregation;
        }

        public Neuron(Func<float, float> activation, Func<float[], float> aggregation)
        {
            _activation = activation;
            _aggregation = aggregation;
        }

        public void SetOutput(float output) => _output = output;
        internal void SetActivation(Func<float, float> activation) => _activation = activation;
        internal void SetAggregation(Func<float[], float> aggregation) => _aggregation = aggregation;
        private float defaultActivation(float input) => input;
        private float defaultAggregation(float[] inputs) => inputs.Sum();
        private float GetOutput()
        {
            float[] weightedInputs = Predecesors.Select(p => p.Output).ToArray();
            float aggregate = _aggregation(weightedInputs);
            return _activation(aggregate);
        }
        internal Neuron DeepCopy()
        {
            return new Neuron(_activation, _aggregation);
        }
    }
}
