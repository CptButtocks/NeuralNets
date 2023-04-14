using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Maths.Models
{
    public class GenericLineFunction
    {
        public float Slope { get; set; }
        public float Intercept { get; set; }

        public GenericLineFunction(float slope = 1, float intercept = 0)
        {
            Slope = slope;
            Intercept = intercept;
        }

        public float Solve(float x)
        {
            return x * Slope + Intercept;
        }
    }
}
