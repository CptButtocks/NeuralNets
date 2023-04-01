using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model
{
    public class Network
    {
        private List<Neuron> _inputs { get; set; } = new();
        private List<Neuron> _outputs { get; set; } = new();
        private List<Neuron> _neurons { get; set; } = new();
        private List<Synapse> _synapses { get; set; } = new();
        private Func<float, float> _activation { get; set; }
        private Func<float[], float> _aggregation { get; set; }
        public Network(IEnumerable<Neuron> inputs, IEnumerable<Neuron> outputs, Func<float, float> activation, Func<float[], float> aggregation)
        {
            _inputs = inputs.ToList();
            _outputs = outputs.ToList();

            _neurons.AddRange(inputs);
            _neurons.AddRange(outputs);
            _activation = activation;
            _aggregation = aggregation;
        }

        public void Add(Neuron neuron)
        {
            neuron.SetActivation(_activation);
            neuron.SetAggregation(_aggregation);
            _neurons.Add(neuron);
        }

        public void AddRange(IEnumerable<Neuron> neurons)
        {
            foreach (Neuron neuron in neurons)
                Add(neuron);
        }

        public void Add(Neuron start, Neuron end, float weight)
        {
            start.SetActivation(_activation);
            end.SetActivation(_activation);
            start.SetAggregation(_aggregation);
            end.SetAggregation(_aggregation);
            _neurons.Add(start);
            _neurons.Add(end);
            _synapses.Add(new Synapse(start, end, weight));
        }

        public void Connect(Neuron start, Neuron end, float weight)
        {
            Neuron startNeuron = _neurons[_neurons.IndexOf(start)];
            Neuron endNeuron = _neurons[_neurons.IndexOf(end)];
            _synapses.Add(new Synapse(startNeuron, endNeuron, weight));
        }

        public float[] GetPrediction(IEnumerable<float> inputs)
        {
            List<float> inputValues = inputs.ToList();
            if (inputValues.Count != _inputs.Count)
                throw new ArgumentException($"Number of inputs: {inputValues.Count} must be equal to the number of input neurons: {_inputs.Count}");

            for (int i = 0; i < inputValues.Count; i++)
                _inputs[i].SetOutput(inputValues[i]);

            return _outputs.Select(o => o.Output).ToArray();
        }
    }
}
