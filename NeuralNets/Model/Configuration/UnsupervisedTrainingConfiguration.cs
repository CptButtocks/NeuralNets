using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Configuration
{
    public class UnsupervisedTrainingConfiguration
    {
        public float PopulationCutOffPercentage { get; set; } = 0.1f;
        public float PopulationElitismPercentage { get; set; } = 0.1f;

        public float AddNodeProbability { get; set; } = 0.1f;
        public float AddConnectionProbability { get; set; } = 0.1f;
        public float ModifyWeightProbability { get; set; } = 0.1f;

        public float RemoveNeuronProbability { get; set; } = 0.05f;
        public float RemoveSynapseProbability { get; set; } = 0.05f;

        public int Population { get; set; } = 100;
    }
}
