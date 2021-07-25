using System.Collections.Generic;

namespace Swarm
{
    internal class SwarmClass
    {
        public List<Particle> Particles { get; private set; }
        public List<double> GlobalBestPosition { get; private set; }
        public double GlobalBestValue { get; private set; }

        public SwarmClass(int particlesCount, int N, double FP, double FG, int FunctionID, List<int> maxPosition, List<int> minPosition)
        {
            Particles = new List<Particle>();
            for (int i = 0; i < particlesCount; i++)
            {
                Particles.Add(new Particle(N, FP, FG, FunctionID, maxPosition, minPosition));
            }
            GlobalBestValue = double.MaxValue;
            GlobalBestPosition = new List<double>();
            for(int i=0; i<N; i++)
            {
                GlobalBestPosition.Add(0);
            }
            FindBestGlobal_ValuePosition();
        }

        public void NextIteration()
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].NextIteration(GlobalBestPosition);
            }
            FindBestGlobal_ValuePosition();
        }

        public List<List<double>> GetResult()
        {
            List<List<double>> result = new List<List<double>>();
            List<Particle> buffer = new List<Particle>();
            Particle BestParticle;

            foreach (Particle particle in Particles)
            {
                buffer.Add(particle);
            }

            for (int i = 0; i < 4; i++)
            {
                BestParticle = buffer[0];
                foreach (Particle particle in Particles)
                {
                    if (particle.CurrentValue < BestParticle.CurrentValue)
                    {
                        BestParticle = particle;
                    }
                }
                result.Add(new List<double>());
                foreach (double coordinate in BestParticle.Position)
                {
                    result[i].Add(coordinate);
                }
                buffer.Remove(BestParticle);
            }
            return result;
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
