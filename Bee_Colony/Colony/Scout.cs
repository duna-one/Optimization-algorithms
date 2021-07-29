using Styles_Functions;
using System.Collections.Generic;

namespace Bee_Colony
{
    /// <summary>
    /// This is your scout. He loves you <3
    /// </summary>
    internal class Scout
    {
        #region public Fields
        public List<double> Position { get; private set; } = new List<double>(); // Current scuot position
        public double BestValue { get; private set; } = double.MaxValue;
        public List<double> BestValuePosition { get; private set; } = new List<double>();
        #endregion

        #region private Fields
        private readonly double SearchRadius;
        private readonly int CheckPoints;
        private readonly int FunctionID;
        #endregion

        #region public functions

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Scout position</param>
        /// <param name="searchRadius">Search radius</param>
        /// <param name="checkPoints">The number of random points to check</param>
        /// <param name="functionID">Function ID</param>
        public Scout(List<double> position, double searchRadius, int checkPoints, int functionID)
        {
            Position = position;
            SearchRadius = searchRadius;
            CheckPoints = checkPoints;
            FunctionID = functionID;
        }

        /// <summary>
        /// Searches for the best value of the function at one of the random points.
        /// Random points are specified in the parameter <see cref="CheckPoints"/>
        /// </summary>
        public void Exploration()
        {
            List<double> position;
            double currValue;
            for (int i = 0; i < CheckPoints; i++)
            {
                position = GetRandomPosition();
                currValue = GetValue(position);
                if (currValue < BestValue)
                {
                    BestValuePosition = position;
                    BestValue = currValue;
                }
            }
        }
        #endregion

        #region private functions

        /// <summary>
        /// Calculates a random location, within the radius that is specified in <see cref="SearchRadius"/>
        /// </summary>
        /// <returns>Coordinates of the calculated location</returns>
        private List<double> GetRandomPosition()
        {
            List<double> result = new List<double>();
            for (int i = 0; i < Position.Count; i++)
            {
                result.Add(new System.Random().Next((int)(Position[i] - SearchRadius),
                                                    (int)(Position[i] + SearchRadius)) +
                           new System.Random().NextDouble());
            }
            return result;
        }

        /// <summary>
        /// Calculates the value of the function in the specified coordinates
        /// </summary>
        /// <param name="position">Coordinates in which to calculate the value of the function</param>
        /// <returns>Value of the function</returns>
        private double GetValue(List<double> position)
        {
            switch (FunctionID)
            {
                case 0:
                    return Functions.GetSphereValue(position);
                case 1:
                    return Functions.GetRastriginValue(position);
                case 2:
                    return Functions.GetSchwefelValue(position);
                default:
                    throw new System.Exception("The resulting ID of the function does not exist. ID: " + FunctionID);
            }
        }
        #endregion
    }
}
