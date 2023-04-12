using NeuralNets.Model.Configuration;
using NeuralNets.Model.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Trainers
{
    public abstract class UnsupervisedTrainer
    {
        protected UnsupervisedTrainingConfiguration _configuration { get; set; }
        private Network InitialNetwork { get; set; }
        private List<Genome> _population = new();
        public UnsupervisedTrainer(Network network, UnsupervisedTrainingConfiguration configuration)
        {
            _configuration = configuration;
            InitialNetwork = network;
            InitializePopulation();
        }

        public abstract float GetFitness(float[][] inputs, float[][] outputs);

        public Genome Train(int iterations, float[][] inputs)
        {
            InitializePopulation();
            
            for (int i = 0; i < iterations; i++)
            {
                foreach(Genome genome in _population)
                {
                    List<float[]> outputs = new();
                    foreach (float[] input in inputs)
                        outputs.Add(genome.Network.Predict(input));

                    genome.Fitness = GetFitness(inputs, outputs.ToArray());
                }

                //Do some culling on the population in order to select the fittest members and mutate the remaining members
                if(i < iterations - 1)
                    MutatePopulation();
            }

            return _population.OrderByDescending(g => g.Fitness).First();
        }

        protected virtual void MutatePopulation()
        {
            _population.OrderByDescending(g => g.Fitness);
            List<Genome> elites = GetElites();
            List<Genome> extinct = GetExtinctGenomes();

            List<Genome> newPopulation = new();
            foreach (Genome elite in elites)
                newPopulation.Add(elite.Reproduce(_configuration));

            foreach(Genome genome in _population)
            {
                newPopulation.Add(genome.Reproduce(_configuration));
                if (newPopulation.Count >= _configuration.Population)
                    break;
            }

            int missingPopulationMembers = _configuration.Population - newPopulation.Count;
            for (int i = 0; i < missingPopulationMembers; i++)
            {
                if (i < elites.Count)
                    newPopulation.Add(elites[i].Reproduce(_configuration));
                else
                    newPopulation.Add(_population[i - elites.Count].Reproduce(_configuration));
            }

            _population = newPopulation;
        }

        private List<Genome> GetElites()
        {
            int eliteGenomeCount = (int)Math.Ceiling((100 / _population.Count) * _configuration.PopulationElitismPercentage);
            List<Genome> elites = _population.OrderByDescending(g => g.Fitness).Take(eliteGenomeCount).ToList();
            foreach (Genome elite in elites)
                _population.Remove(elite);

            return elites;
        }

        private List<Genome> GetExtinctGenomes()
        {
            int extinctGenomeCount = (int)Math.Ceiling((100 / _population.Count) * _configuration.PopulationCutOffPercentage);
            List<Genome> extinctGenomes = _population.OrderBy(g => g.Fitness).Take(extinctGenomeCount).ToList();
            foreach(Genome extinctGenome in extinctGenomes)
                _population.Remove(extinctGenome);

            return extinctGenomes;
        }

        private void InitializePopulation()
        {
            Random random = new Random();
            for (int i = 0; i < _configuration.Population; i++)
            {
                Network network = InitialNetwork.DeepCopy();
                foreach (Connection synapse in (IEnumerable<Connection>)network)
                    synapse.Weight = 0.01f * random.Next(0, 100);
                _population.Add(new Genome(network, 1));
            }
        }
    }
}
