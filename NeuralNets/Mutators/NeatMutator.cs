﻿using NeuralNets.Helpers;
using NeuralNets.Model.Configuration;
using NeuralNets.Model.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNets.Extensions;
using NeuralNets.Model.Neural.Nodes;

namespace NeuralNets.Mutators
{
    public static class NeatMutator
    {
        public static int NodesAdded = 0;
        public static int ConnectionsAdded = 0;
        public static int NodesRemoved = 0;
        public static int ConnectionsRemoved = 0;
        public static int WeightsModified = 0;
        public static void Mutate(ref Network network, UnsupervisedTrainingConfiguration config)
        {
            bool willAddNode = ProbilityHelper.IsTrue(config.AddNodeProbability);
            bool willAddConnection = ProbilityHelper.IsTrue(config.AddConnectionProbability);
            bool willModifyWeight = ProbilityHelper.IsTrue(config.ModifyWeightProbability);

            bool willRemoveNode = ProbilityHelper.IsTrue(config.RemoveNeuronProbability);
            bool willRemoveConnection = ProbilityHelper.IsTrue(config.RemoveSynapseProbability);

            if (willAddNode) AddNode(ref network);
            if(willAddConnection) AddConnection(ref network);
            if (willModifyWeight) ModifyWeight(ref network);
            if(willRemoveNode) RemoveNode(ref network);
            if (willRemoveConnection) RemoveConnection(ref network);
        }

        private static void AddNode(ref Network network)
        {
            Random random = new Random();
            int index = random.Next(0, network.Hidden.Count - 1);
            Layer<Node> layer = network.Hidden[index];
            Node newNode = new Node();
            network.Add(newNode, layer.Depth - 1);
            if(index == 0)
            {
                int leftIndex = random.Next(0, network.Inputs.Count - 1);
                InputNode left = network.Inputs[leftIndex];
                network.Connect(left, newNode);
            }

            else
            {
                int leftIndex = random.Next(0, network.Hidden[index - 1].Count - 1);
                Node left = network.Hidden[index - 1][leftIndex];
                network.Connect(left, newNode);
            }

            Layer<Node> rightLayer = index == network.Hidden.Count - 1 ? network.Outputs : network.Hidden[index + 1];
            int rightIndex = random.Next(0, rightLayer.Count - 1);
            Node right = rightLayer[rightIndex];
            network.Connect(newNode, right);
            NodesAdded++;
        }

        private static void AddConnection(ref Network network)
        {
            List<(Node start, Node end)> pairs = new();
            foreach(InputNode input in network.Inputs)
            {
                foreach(Node node in network.Hidden[0])
                {
                    if (input.Children.Where(c => c.End.Id == node.Id).Count() == 0)
                        pairs.Add((input, node));
                }
            }

            if (network.Hidden.Count > 1)
            {
                for (int i = 0; i < network.Hidden.Count - 1; i++)
                {
                    Layer<Node> leftLayer = network.Hidden[i];
                    Layer<Node> rightLayer = network.Hidden[i + 1];
                    pairs = pairs.Concat(getUnconnectedPairs<Node, Node>(leftLayer, rightLayer)).ToList();
                }
            }

            pairs = pairs.Concat(getUnconnectedPairs<Node, Node>(network.Hidden[network.Hidden.Count - 1], network.Outputs)).ToList();
            Random random = new Random();

            int index = random.Next(0, pairs.Count - 1);
            (Node left, Node right) pair = pairs[index];
            network.Connect(pair.left, pair.right);
            ConnectionsAdded++;
        }

        private static List<(T, D)> getUnconnectedPairs<T, D>(Layer<T> leftLayer, Layer<D> rightLayer) where T : Node where D: Node
        {
            List<(T, D)> pairs = new List<(T, D)> ();
            foreach(T leftNode in leftLayer)
            {
                foreach(D rightNode in rightLayer)
                {
                    if (leftNode.Children.Where(c => c.End.Id == rightNode.Id).Count() == 0)
                        pairs.Add((leftNode, rightNode));
                }
            }

            return pairs;
        }

        private static void ModifyWeight(ref Network network)
        {
            Random random = new Random();
            int index = random.Next(0, network.Connections.Count - 1);
            Connection connection = network.Connections[index];
            float percentage = (float)random.NextDouble() + 0.5f;
            connection.Weight = connection.Weight * percentage;
            WeightsModified++;
        }

        private static void RemoveNode(ref Network network)
        {
            Random random = new Random();
            int index = random.Next(0, network.Hidden.Count - 1);
            int secondIndex = random.Next(0, network.Hidden[index].Count - 1);

            Node node = network.Hidden[index][secondIndex];
            network.Remove(node);
            NodesRemoved++;
        }

        private static void RemoveConnection(ref Network network)
        {
            Random random = new Random();
            int index = random.Next(0, network.Connections.Count - 1);
            Connection connection = network.Connections[index];
            network.Remove(connection);
            ConnectionsRemoved++;
        }
    }
}
