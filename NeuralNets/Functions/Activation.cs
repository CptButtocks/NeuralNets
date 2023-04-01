using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Functions
{
    public static class Activation
    {
        public static float Linear(float input) => input;
        public static float BinaryStep(float input) => input < 0 ? 0 : 1;
        public static float Sigmoid(float input)
        {
            float k = MathF.Exp(input);
            return k / (1 + k);
        }

        public static float TanH(float input)
        {
            float a = MathF.Exp(input);
            float b = MathF.Exp(-input);

            return (a - b) / (a + b);
        }

        public static float DTanH(float input)
        {
            throw new NotImplementedException();
        }
        public static float ReLU(float input) => MathF.Max(0, input);
        public static float DyingReLU(float input) => MathF.Max(0.1f * input, input);
        public static float Swish(float input) => input * Sigmoid(input);
    }
}
