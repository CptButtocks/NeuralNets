using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural
{
    public class Network
    {
        public int LayerCount => _hidden.Count + 2;
        public int NodeCount => _nodes.Count;
        private List<Node> _nodes { get; set; } = new();
        private List<Layer> _hidden { get; set; } = new();
        private Layer _inputs { get; set; } = new(0);
        private Layer _outputs { get; set; } = new(1);
        private List<Connection> _connections = new();

        public Network(int layers)
        {
            for (int i = 0; i < layers; i++)
            {
                Layer layer = new Layer(i + 1);
                _hidden.Add(layer);
            }
        }

        public Layer this[int index]
        {
            get
            {
                if (index == 0)
                    return _inputs;
                else if (index < LayerCount)
                    return _hidden[index - 1];
                else
                    return _outputs;
            }

            set
            {
                if (index == 0)
                    _inputs = value;
                else if (index < LayerCount)
                    _hidden[index - 1] = value;
                else
                    _outputs = value;
            }
        }

        public void Add(Layer layer)
        {
            layer.Depth = _hidden.Count + 1;
            _hidden.Add(layer);

            foreach(Node node in layer)
                _nodes.Add(node);
        }

        public void Add(Node node, int depth)
        {
            if (depth < 0 || depth >= LayerCount)
                throw new IndexOutOfRangeException($"The given depth: {depth} is below 0 or exceeds the layer count: {LayerCount}");

            node.Layer = depth;
            if (depth == 0)
                _inputs.Add(node);
            else if (depth < LayerCount)
                _hidden[depth - 1].Add(node);
            else
                _outputs.Add(node);

            _nodes.Add(node);
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

        public Network DeepCopy()
        {
            Network network = new(_hidden.Count);

            foreach (Node input in _inputs)
                network.Add(input.DeepCopy(), 0);
            foreach (Node output in _outputs)
                network.Add(output.DeepCopy(), LayerCount - 1);

            foreach(Layer layer in _hidden)
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
    }
}
