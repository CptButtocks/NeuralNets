using NeuralNets.Helpers;
using NeuralNets.Model.Configuration;
using NeuralNets.Model.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNets.Extensions;

namespace NeuralNets.Mutators
{
    public static class NeatMutator
    {
        public static void Mutate(ref Network network, UnsupervisedTrainingConfiguration config)
        {
            bool willAddNeuron = ProbilityHelper.IsTrue(config.AddNeuronProbability);
            bool willAddSynapse = ProbilityHelper.IsTrue(config.AddSynapseProbability);
            bool willModifyWeight = ProbilityHelper.IsTrue(config.ModifyWeightProbability);

            bool willRemoveNeuron = ProbilityHelper.IsTrue(config.RemoveNeuronProbability);
            bool willRemoveSynapse = ProbilityHelper.IsTrue(config.RemoveSynapseProbability);
        }

        private static (Neuron a, Neuron b) GetRandomNeuronPair(Network network)
        {
            IEnumerable<Neuron> neurons = (IEnumerable<Neuron>)network;
            if (neurons.Count() < 2)
                throw new ArgumentException("Expected a network with more than 1 neuron");
            Neuron a = neurons.Random<Neuron>();
            Neuron b = neurons.Random<Neuron>();

            if(a.Id == b.Id)
            {
                while (a.Id == b.Id)
                    b = neurons.Random<Neuron>();
            }

            return (a, b);
        }

        private static void AddNeuron(ref Network network)
        {
            Neuron neuron = new Neuron();
            (Neuron a, Neuron b) neuronPair = GetRandomNeuronPair(network);
            Synapse predecesor = new Synapse(neuronPair.a, neuron);
            Synapse succesor = new Synapse(neuron, neuronPair.b);
            
            Random random = new Random();
            predecesor.Weight = (float)random.NextDouble();
            succesor.Weight = (float)random.NextDouble();

            neuron.Predecesors.Add(predecesor);
            neuron.Succesors.Add(succesor);

            network.Add(neuron);
        }

        private static void AddSynapse(ref Network network)
        {
            (Neuron a, Neuron b) neuronPair = GetRandomNeuronPair(network);
        }

        private static void ModifyWeight(ref Network network)
        {

        }

        private static void RemoveNeuron(ref Network network)
        {

        }

        private static void RemoveSynapse(ref Network network)
        {

        }
    }
}
