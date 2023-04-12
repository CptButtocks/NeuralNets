using NeuralNets.Model.Configuration;
using NeuralNets.Mutators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural
{
    public class Genome
    {
        public int Generation { get; set; }
        public float Fitness { get; set; }
        public Network Network { get; set; }

        public Genome(Network network, int generation)
        {
            Network = network;
            Generation = generation;
        }

        public Genome Reproduce(UnsupervisedTrainingConfiguration configuration)
        {
            Network network = Network.DeepCopy();
            NeatMutator.Mutate(ref network, configuration);

            return new Genome(Network.DeepCopy(), Generation + 1); ;
        }
    }
}
