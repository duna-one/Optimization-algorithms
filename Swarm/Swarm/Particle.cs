using System;
using System.Collections.Generic;

namespace Swarm
{
    internal class Particle
    {
        public List<double> Velocity { get; private set; }
        public List<double> Position { get; private set; }
        public double CurrentValue { get; private set; }
        public double BestValue { get; private set; }
        public List<double> BestValuePosition { get; private set; }

        private readonly double FP;
        private readonly double FG;
        private readonly int FunctionID;

        public Particle(int N, double FP, double FG, int FunctionID, List<int> maxPosition, List<int> minPosition)
        {
            Random rnd = new Random();

            this.FP = FP;
            this.FG = FG;
            this.FunctionID = FunctionID;
            BestValue = double.MaxValue;
            Velocity = new List<double>();
            Position = new List<double>();
            BestValuePosition = new List<double>();

            for (int i = 0; i < N; i++)
            {
                Position.Add(rnd.Next(minPosition[i], maxPosition[i]) + rnd.NextDouble());
                Velocity.Add(0);
                BestValuePosition.Add(0);
            }
        }

        public void NextIteration(List<double> GlobalBestPosition)
        {
            UpdatePosition();
            UpdateCurrentValue();
            UpdateBestValue();
            UpdateVelocity(GlobalBestPosition);
        }

        private void UpdatePosition()
        {
            for (int i = 0; i < Position.Count; i++)
            {
                Position[i] += Velocity[i];
            }
        }

        private void UpdateVelocity(List<double> GlobalBestPosition)
        {
            Random rnd = new Random();
            double f = FP + FG;
            double rP = rnd.NextDouble();
            double rG = rnd.NextDouble();
            double k = rnd.NextDouble();
            double X = 2 * k / Math.Abs(2 - f - Math.Sqrt((f * f) - (4 * f)));

            if (f < 4)
            {
                throw new Exception("f must be > 4");
            }

            for (int i = 0; i < Velocity.Count; i++)
            {
                Velocity[i] = X * (Velocity[i] + (FP * rP * (BestValuePosition[i] - Position[i])) + (FG * rG * (GlobalBestPosition[i] - Position[i])));
            }
        }

        private void UpdateCurrentValue()
        {
            switch (FunctionID)
            {
                case 0:
                    CurrentValue = Functions.GetSphereValue(Position);
                    break;
                case 1:
                    CurrentValue = Functions.GetRastriginValue(Position);
                    break;
                case 2:
                    CurrentValue = Functions.GetSchwefelValue(Position);
                    break;
                default:
                    Exception exception = new System.Exception("An incorrect ID of the function was received");
                    throw exception;
            }
        }

        private void UpdateBestValue()
        {
            if (CurrentValue < BestValue)
            {
                BestValue = CurrentValue;
                for (int i = 0; i < Position.Count; i++)
                {
                    BestValuePosition[i] = Position[i];
                }
            }
        }
    }
}