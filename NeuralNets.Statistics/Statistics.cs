using System.Numerics;

namespace NeuralNets.Statistics
{
    public static class Statistics
    {
        public static float GetMean(IEnumerable<float> points)
        {
            float sum = 0;
            int count = 0;
            foreach(float point in points)
            {
                sum += point;
                count++;
            }   

            return sum / count;
        }

        public static float GetMean(Vector2[] points)
        {
            return GetMean(points.Select(p => p.Y));
        }

        public static float GetMean(float[][] points, int index)
        {
            return GetMean(points.Select(p => p[index]));
        }
    }
}