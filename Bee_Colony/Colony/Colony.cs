using System.Collections.Generic;

namespace Bee_Colony.Colony
{
    internal class Colony
    {
        #region public fields
        public List<double> Position { get; private set; } = new List<double>();
        public List<double> GlobalBestPosition { get; private set; } = new List<double>();
        public double GlobalBestValue { get; private set; } = double.MaxValue;
        #endregion

        #region private fields
        private readonly List<Scout> Scouts = new List<Scout>();
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
