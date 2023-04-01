using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model
{
    public abstract class UnsupervisedTrainer
    {
        public float MutationCutOffPercentage { get; set; }
        public int Population { get; set; }
        private Network InitialNetwork { get; set; }
        private List<Network> _population = new();
        public UnsupervisedTrainer(float mutationCutOffPercentage, int population, Network network)
        {
            MutationCutOffPercentage = mutationCutOffPercentage;
            Population = population;
            InitialNetwork = network;
            InitializePopulation();
        }

        public abstract float GetFitness(float[] inputs, float[] outputs);
        private void InitializePopulation()
        {
            Random random = new Random();
            for (int i = 0; i < Population; i++)
            {
                Network network = InitialNetwork.DeepCopy();
                foreach (Synapse synapse in network)
                    synapse.Weight = 0.01f * random.Next(0, 100);
                _population.Add(network);
            }
        }
    }
}
