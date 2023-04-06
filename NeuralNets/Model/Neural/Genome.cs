using NeuralNets.Model.Configuration;
using NeuralNets.Mutators;

namespace NeuralNets.Model.Neural
{
    public class Genome
    {
        private int _generation { get; set; }
        private Network _network { get; set; }

        public int Generation => _generation;
        public Network Network => _network;
        public float Fitness { get; set; }

        public Genome(Network network, int generation)
        {
            _network = network;
            _generation = generation;
        }

        public Genome Reproduce(UnsupervisedTrainingConfiguration config)
        {
            Network network = _network.DeepCopy();
            int generation = _generation + 1;

            //Mutate the network. Replace with generic function
            NeatMutator.Mutate(ref network, config);

            return new(network, generation);
        }
    }
}
