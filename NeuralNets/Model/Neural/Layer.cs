using NeuralNets.Model.Neural.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural
{
    public class Layer<T> : IEnumerable<T> where T : Node
    {
        private List<T> _nodes = new();

        /// <summary>
        /// The depth of the layer within the network
        /// </summary>
        public int Depth { get; set; }

        public Layer(int depth)
        {
            Depth = depth;
        }

        public T this[int index] => _nodes[index];

        public void Add(T node) => _nodes.Add(node);
        public void AddRange(IEnumerable<T> nodes) => _nodes.AddRange(nodes);
        public void Insert(int index, T node) => _nodes.Insert(index, node);

        public IEnumerator<T> GetEnumerator() => _nodes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
    }
}
