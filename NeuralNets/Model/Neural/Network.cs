using NeuralNets.Functions;
using NeuralNets.Model.Neural.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural
{
    public class Network : IEnumerable<Node>, IEnumerable<Connection>
    {
        public int LayerCount => _hidden.Count + 2;
        public int NodeCount => _nodes.Count;
        private List<Node> _nodes { get; set; } = new();
        private List<Layer<Node>> _hidden { get; set; } = new();
        private Layer<InputNode> _inputs { get; set; } = new(0);
        private Layer<Node> _outputs { get; set; } = new(1);
        private List<Connection> _connections = new();

        /// <summary>
        /// The activation function applied to the aggregated inputs
        /// </summary>
        private Func<float, float> _activationFunction { get; set; } = Activation.ReLU;

        /// <summary>
        /// The aggregation function applied to the inputs
        /// </summary>
        private Func<float[], float> _aggregationFunction { get; set; } = Aggregation.Sum;

        public Network(int layers)
        {
            for (int i = 0; i < layers; i++)
            {
                Layer<Node> layer = new Layer<Node>(i + 1);
                _hidden.Add(layer);
            }
        }

        public Network(Layer<InputNode> inputs, Layer<Node> outputs, Func<float, float> activationFunction, Func<float[], float> aggregationFunction)
        {
            _inputs = inputs;
            _outputs = outputs;
            _activationFunction = activationFunction;
            _aggregationFunction = aggregationFunction;
        }

        public void Add(Layer<Node> layer)
        {
            layer.Depth = _hidden.Count + 1;
            _hidden.Add(layer);

            foreach(InputNode node in layer)
                _nodes.Add(node);
        }

        public void Add(Node node, int depth)
        {
            _hidden[depth].Add(node);
        }

        public void AddRange(IEnumerable<Node> nodes, int depth)
        {
            _hidden[depth].AddRange(nodes);
        }

        public void Connect(Node start, Node end)
        {
            Random random = new Random();
            float weight = (float)random.NextDouble();
            float bias = (float)random.NextDouble();

            Connect(start, end, weight, bias);
        }

        public void Connect(Node start, Node end, float weight, float bias)
        {
            Connection connection = new Connection(start, end, weight, bias);
            _connections.Add(connection);
        }

        protected void Connect(int startId, int endId, float weight, float bias)
        {
            if (startId == endId)
                throw new ArgumentException("Nodes with identical ID's cannnot be connected");
            Node? start = _nodes.Where(n => n.Id == startId).FirstOrDefault();
            if (start == null)
                throw new ArgumentNullException($"Start node with ID {startId} cannot be found");

            Node? end = _nodes.Where(n => n.Id == endId).FirstOrDefault();

            if (end == null)
                throw new ArgumentNullException($"End node with ID {endId} cannot be found");

            Connect(start, end, weight, bias);
        }

        public float[] Predict(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
                _inputs[i].SetInput(inputs[i]);

            return _outputs.Select(o => o.Output).ToArray();
        }

        public Network DeepCopy()
        {
            Network network = new(_hidden.Count);

            foreach (InputNode input in _inputs)
                network.Add(input.DeepCopy(), 0);
            foreach (InputNode output in _outputs)
                network.Add(output.DeepCopy(), LayerCount - 1);

            foreach(Layer<Node> layer in _hidden)
            {
                foreach(Node node in layer)
                {
                    network.Add(node.DeepCopy(), layer.Depth);
                }
            }

            foreach(Connection connection in _connections)
                network.Connect(connection.Start.Id, connection.End.Id, connection.Weight, connection.Bias);

            return network;
        }

        IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

        IEnumerator<Connection> IEnumerable<Connection>.GetEnumerator() => _connections.GetEnumerator();

        IEnumerator<Node> IEnumerable<Node>.GetEnumerator() => _nodes.GetEnumerator();
    }
}
