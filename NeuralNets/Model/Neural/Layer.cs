using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural
{
    public class Layer : IEnumerable<Node>
    {
        private List<Node> _nodes = new();

        /// <summary>
        /// The depth of the layer within the network
        /// </summary>
        public int Depth { get; set; }

        public Layer(int depth)
        {
            Depth = depth;
        }

        public void Add(Node node) => _nodes.Add(node);
        public void Insert(int index, Node node) => _nodes.Insert(index, node);

        public IEnumerator<Node> GetEnumerator() => _nodes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
    }
}
