using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Functions
{
    public static class Aggregation
    {
        public static float Sum(float[] inputs) => inputs.Sum();
        public static float Product(float[] inputs)
        {
            float product = inputs[0];
            for (int i = 1; i < inputs.Length; i++)
            {
                product = product * inputs[i];
            }

            return product;
        }
        public static float Median(float[] inputs)
        {
            List<float> orderedInputs = inputs.OrderBy(x => x).ToList();
            int centerIndex = (int)Math.Floor((double)(orderedInputs.Count / 2));
            if (orderedInputs.Count % 2 == 0)
                return (orderedInputs[centerIndex] + orderedInputs[centerIndex - 1]) / 2;
            else
                return orderedInputs[centerIndex];
        }
        public static float Min(float[] inputs) => inputs.Min();
        public static float Max(float[] inputs) => inputs.Max();
        public static float Average(float[] inputs) => inputs.Average();
        public static float MaxAbs(float[] inputs) => inputs.Select(i => MathF.Abs(i)).Max();
    }
}
