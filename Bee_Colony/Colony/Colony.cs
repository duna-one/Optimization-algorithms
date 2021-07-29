using System.Collections.Generic;

namespace Bee_Colony
{
    /// <summary>
    /// This is your bee colony. Be careful c: <br/>
    /// Beeeeeeeeeeeeeeeeeeeeeeeeeee
    /// </summary>
    internal class Colony
    {
        #region public fields
        public List<double> Position { get; private set; } = new List<double>(); // Current position of colony
        public List<double> GlobalBestPosition { get; private set; } = new List<double>();
        public double GlobalBestValue { get; private set; } = double.MaxValue;
        public readonly List<Scout> Scouts = new List<Scout>();
        #endregion

        #region private fields
        private readonly int SearchRadius;
        private readonly int CheckPoints;
        private readonly int FunctionID;
        private readonly int ScoutsCount;
        #endregion

        public Colony(List<double> position, int searchRadius, int scoutsCount, int checkPoints, int functionID)
        {
            Position = position;
            SearchRadius = searchRadius;
            CheckPoints = checkPoints;
            FunctionID = functionID;
            ScoutsCount = scoutsCount;

            for (int i = 0; i < ScoutsCount; i++)
            {
                Scouts.Add(new Scout(GetRandomPosition(), SearchRadius, CheckPoints, FunctionID));
            }
        }

        /// <summary>
        /// Searches best value position and moves colony to this position
        /// </summary>
        public void Exploration()
        {
            foreach (Scout scout in Scouts)
            {
                scout.Exploration();
                if (scout.BestValue < GlobalBestValue)
                {
                    GlobalBestValue = scout.BestValue;
                    GlobalBestPosition = scout.BestValuePosition;
                }
            }

            Position = GlobalBestPosition;
            Scouts.Clear();
            for (int i = 0; i < ScoutsCount; i++)
            {
                Scouts.Add(new Scout(GetRandomPosition(), SearchRadius, CheckPoints, FunctionID));
            }
        }

        /// <summary>
        /// OMG IT'S RANDOM
        /// </summary>
        /// <returns>List of random position coordinates</returns>
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
    }
}
