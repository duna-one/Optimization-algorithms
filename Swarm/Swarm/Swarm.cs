using System.Collections.Generic;

namespace Swarm
{
    internal class Swarm
    {
        public List<Particle> Particles { get; private set; }
        public List<double> GlobalBestPosition { get; private set; }
        public double GlobalBestValue { get; private set; }

        public Swarm(int particlesCount, int N, double FP, double FG, byte FunctionID, List<int> maxPosition, List<int> minPosition)
        {
            Particles = new List<Particle>();
            for (int i = 0; i < particlesCount; i++)
            {
                Particles.Add(new Particle(N, FP, FG, FunctionID, maxPosition, minPosition));
            }
            GlobalBestValue = double.MaxValue;
            FindBestGlobal_ValuePosition();
        }

        public void NextIteration()
        {
            for(int i=0; i<Particles.Count; i++)
            {
                Particles[i].NextIteration(GlobalBestPosition);
            }
            FindBestGlobal_ValuePosition();
        }

        private void FindBestGlobal_ValuePosition()
        {
            foreach (Particle particle in Particles)
            {
                if (particle.BestValue < GlobalBestValue)
                {
                    GlobalBestValue = particle.BestValue;
                    for (int i = 0; i < particle.BestValuePosition.Count; i++)
                    {
                        GlobalBestPosition[i] = particle.BestValuePosition[i];
                    }
                }
            }
        }
    }
}
