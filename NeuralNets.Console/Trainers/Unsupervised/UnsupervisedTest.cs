using NeuralNets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Console.Trainers.Unsupervised
{
    public class UnsupervisedTest : UnsupervisedTrainer
    {
        public UnsupervisedTest(float mutationCutOffPercentage, int population, Network network) : base(mutationCutOffPercentage, population, network)
        {
        }

        public override float GetFitness(float[] inputs, float[] outputs)
        {
            return 0f;
        }
    }
}
