using System;
using System.Collections.Generic;

namespace Styles_Functions
{
    public static class Functions
    {
        /// <summary>
        /// Calculates the value of the sphere function
        /// </summary>
        /// <param name="listX">X values for calculation</param>
        /// <returns>F(x)</returns>
        public static double GetSphereValue(List<double> listX)
        {
            double result = 0;
            foreach (double x in listX)
            {
                result += x * x;
            }
            return result;
        }

        /// <summary>
        /// Calculates the value of the Rastrigin function
        /// </summary>
        /// <param name="listX">X values for calculation</param>
        /// <returns>F(x)</returns>
        public static double GetRastriginValue(List<double> listX)
        {
            double result = 0;
            foreach (double x in listX)
            {
                result += (x * x) - (10 * Math.Cos(2 * Math.PI * x));
            }
            result *= 10 * listX.Count;
            return result;
        }

        /// <summary>
        /// Calculates the value of the Schwefel function
        /// </summary>
        /// <param name="listX">X values for calculation</param>
        /// <returns>F(x)</returns>
        public static double GetSchwefelValue(List<double> listX)
        {
            double result = 0;
            foreach (double x in listX)
            {
                result += -1 * x * Math.Sin(Math.Sqrt(Math.Abs(x)));
            }
            return result;
        }
    }
}
