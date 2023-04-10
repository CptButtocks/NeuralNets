using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural
{
    public class Layer : IEnumerable<Neuron>
    {
        private List<Neuron> _neurons;

        public int Count => _neurons.Count;

        public Layer()
        {
            _neurons = new List<Neuron>();
        }

        public Neuron this[int index]
        {
            get => _neurons[index]; set => _neurons[index] = value; 
        }

        public Layer(IEnumerable<Neuron> neurons)
        {
            _neurons = new List<Neuron>();
            _neurons.AddRange(neurons);
        }

        public void Add(Neuron neuron)
        {
            _neurons.Add(neuron);
        }

        public void Insert(int index, Neuron neuron)
        {
            _neurons.Insert(index, neuron);
        }

        public void Remove(Neuron neuron)
        {
            _neurons.Remove(neuron);
        }

        public IEnumerator<Neuron> GetEnumerator() => _neurons.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _neurons.GetEnumerator();
    }
}
