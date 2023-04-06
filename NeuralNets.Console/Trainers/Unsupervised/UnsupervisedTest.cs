using NeuralNets.Model;
using NeuralNets.Model.Configuration;
using NeuralNets.Model.Neural;
using NeuralNets.Model.Trainers;

namespace NeuralNets.Console.Trainers.Unsupervised
{
    public class UnsupervisedTest : UnsupervisedTrainer
    {
        public UnsupervisedTest(Network network, UnsupervisedTrainingConfiguration configuration) : base(network, configuration)
        {
        }

        public override float GetFitness(float[][] inputs, float[][] outputs)
        {
            return 0f;
        }
    }
}
