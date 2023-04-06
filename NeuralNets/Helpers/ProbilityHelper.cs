using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Helpers
{
    public static class ProbilityHelper
    {
        public static bool IsTrue(float probability)
        {
            Random random = new Random();

            return random.NextDouble() < probability;
        }
    }
}
