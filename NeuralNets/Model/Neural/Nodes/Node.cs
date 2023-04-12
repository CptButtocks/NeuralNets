using NeuralNets.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural.Nodes
{
    public class Node
    {
        /// <summary>
        /// Unique ID for the Node in the network
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The layer of the network the node resides in
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        /// The output of the node
        /// </summary>
        public virtual float Output
        {
            get
            {
                float aggregate = AggregationFunction.Invoke(Inputs);
                return ActivationFunction.Invoke(aggregate);
            }
        }

        /// <summary>
        /// The inputs of the node
        /// </summary>
        public float[] Inputs => Parents.Select(s => s.Output).ToArray();

        /// <summary>
        /// The activation function applied to the aggregated inputs
        /// </summary>
        public Func<float, float> ActivationFunction { get; set; } = Activation.ReLU;

        /// <summary>
        /// The aggregation function applied to the inputs
        /// </summary>
        public Func<float[], float> AggregationFunction { get; set; } = Aggregation.Sum;

        /// <summary>
        /// The predessesing Nodes in the network
        /// </summary>
        public List<Connection> Parents { get; set; } = new();

        /// <summary>
        /// The succesing nodes in the network
        /// </summary>
        public List<Connection> Children { get; set; } = new();


        public virtual Node DeepCopy()
        {
            return new Node()
            {
                Id = Id,
                ActivationFunction = ActivationFunction,
                AggregationFunction = AggregationFunction,
            };
        }
    }
}
