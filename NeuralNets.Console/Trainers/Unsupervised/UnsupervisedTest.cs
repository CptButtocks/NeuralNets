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
            float fitness = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                float[] numbers = inputs[i];
                float a = numbers[0];
                float b = numbers[1];

                float correctPrediction = a + b;

                float prediction = outputs[i][0];

                float error = MathF.Abs(correctPrediction - prediction);
                fitness += 20 - error;
            }

            return fitness;
        }
    }
}
