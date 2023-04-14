using NeuralNets.Maths.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Statistics.Regression
{
    /// <summary>
    /// Linear regression class
    /// </summary>
    public static class LinearRegression
    {
        /// <summary>
        /// Get the sum of squared residuals from a dataset.
        /// </summary>
        /// <param name="points">A float[] of float[] points where index 0 of any item is X and index 1 of any item is Y</param>
        /// <returns>The sum of squared residuals for a given line through the dataset</returns>
        public static float GetSumOfSquaredResiduals(Vector2[] points, GenericLineFunction function)
        {
            float sum = 0;

            foreach(Vector2 point in points)
            {
                float y = function.Solve(point.X);
                sum += MathF.Pow(y - point.Y, 2);
            }

            return sum;
        }

        /// <summary>
        /// Compute the least squared linear function for a given set of points
        /// <see href="http://www.csharphelper.com/howtos/howto_linear_least_squares.html">Source</see>
        /// </summary>
        /// <param name="points"></param>
        /// <returns>The linear function that best matches the given set of points</returns>
        public static GenericLineFunction GetLeastSquares(Vector2[] points)
        {
            float S1 = points.Length;
            float sX = 0;
            float sY = 0;
            float sXX = 0;
            float sXY = 0;

            foreach(Vector2 point in points)
            {
                sX += point.X;
                sY += point.Y;
                sXX += point.X * point.X;
                sXY += point.X * point.Y;
            }

            float slope = (sXY * S1 - sX * sY) / (sXX * S1 - sX * sX);
            float intercept = (sXY * sX - sY * sXX) / (sX * sX - S1 * sXX);

            return new GenericLineFunction(slope, intercept);
        }

        public static float GetSumOfSquaresAroundMean(Vector2[] points)
        {
            float mean = Statistics.GetMean(points);
            float sumSquaredOfMean = 0;
            foreach (Vector2 point in points)
                sumSquaredOfMean += MathF.Pow(point.Y - mean, 2);

            return sumSquaredOfMean;
        }

        public static float GetVariationAroundMean(Vector2[] points)
        {
            return GetSumOfSquaresAroundMean(points) / points.Length;
        }

        public static float GetVariationAroundFit(Vector2[] points, GenericLineFunction fit)
        {
            float sumOfSquaredResidals = GetSumOfSquaredResiduals(points, fit);

            return sumOfSquaredResidals / points.Length;
        }

        public static float GetVariationAroundFit(Vector2[] points)
        {
            GenericLineFunction leastSquares = GetLeastSquares(points);
            return GetVariationAroundFit(points, leastSquares);
        }

        public static float GetRSquared(Vector2[] points)
        {
            GenericLineFunction leastSquares = GetLeastSquares(points);
            float sumOfSquaresResidals = GetSumOfSquaredResiduals(points, leastSquares);
            float sumOfSquaresAroundMean = GetSumOfSquaresAroundMean(points);

            return (sumOfSquaresAroundMean - sumOfSquaresResidals) / sumOfSquaresAroundMean;
        }
    }
}
