using NeuralNets.Model.Configuration;
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
            throw new NotImplementedException();
        }
    }
}
