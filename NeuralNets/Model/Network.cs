using NeuralNets.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model
{
    public class Network : IEnumerable<Synapse>, IEnumerable<Neuron>
    {
        private List<Neuron> _inputs { get; set; } = new();
        private List<Neuron> _outputs { get; set; } = new();
        private List<Neuron> _hidden { get; set; } = new();
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

        public Neuron Get(int id) => _neurons.Where(n => n.Id == id).First();

        public void Add(Neuron neuron)
        {
            neuron.Id = _neurons.Count;
            neuron.SetActivation(_activation);
            neuron.SetAggregation(_aggregation);
            _neurons.Add(neuron);
            _hidden.Add(neuron);
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
            _hidden.Add(start);
            _hidden.Add(end);
            _synapses.Add(new Synapse(start, end, weight));
        }

        public void Connect(Neuron start, Neuron end, float weight)
        {
            Neuron startNeuron = _neurons[_neurons.IndexOf(start)];
            Neuron endNeuron = _neurons[_neurons.IndexOf(end)];
            _synapses.Add(new Synapse(startNeuron, endNeuron, weight));
        }

        public float[] Predict(float[] inputs)
        {
            if (inputs.Length != _inputs.Count)
                throw new ArgumentException($"Number of inputs: {inputs.Length} must be equal to the number of input neurons: {_inputs.Count}");

            for (int i = 0; i < inputs.Length; i++)
                _inputs[i].SetOutput(inputs[i]);

            return _outputs.Select(o => o.Output).ToArray();
        }

        internal Network DeepCopy()
        {
            IEnumerable<Neuron> inputs = _inputs.Select(n => n.DeepCopy());
            IEnumerable<Neuron> outputs = _outputs.Select(n => n.DeepCopy());
            Network network = new Network(inputs, outputs, _activation, _aggregation);
            network.AddRange(_hidden.Select(n => n.DeepCopy()));

            foreach(Synapse synapse in _synapses)
            {
                Neuron start = network.Get(synapse.Start.Id);
                Neuron end = network.Get(synapse.End.Id);

                network.Connect(start, end, synapse.Weight);
            }

            return network;
        }

        public IEnumerator<Synapse> GetEnumerator() => _synapses.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _neurons.GetEnumerator();

        IEnumerator<Neuron> IEnumerable<Neuron>.GetEnumerator() => _neurons.GetEnumerator();
    }
}
