using NeuralNets.Helpers;
using NeuralNets.Model.Configuration;
using NeuralNets.Model.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static void AddNeuron(ref Network network)
        {

        }

        private static void AddSynapse(ref Network network)
        {

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
