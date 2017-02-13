using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.outils_dev_jeu
{
    // From http://stackoverflow.com/questions/20676185/xna-monogame-getting-the-frames-per-second
    public class FrameCounter
    {
        public FrameCounter() { }

        public long TotalFrames
        {
            get;
            private set;
        }

        public float TotalSeconds
        {
            get;
            private set;
        }

        public float AverageFPS
        {
            get;
            private set;
        }

        public float CurrentFPS
        {
            get;
            private set;
        }

        public const int MAX_SAMPLES = 100;

        private Queue<float> sampleBuffer = new Queue<float>();

        public bool Update(float deltaTime)
        {
            CurrentFPS = 1.0f / deltaTime;
            sampleBuffer.Enqueue(CurrentFPS);

            if (sampleBuffer.Count > MAX_SAMPLES)
            {
                sampleBuffer.Dequeue();
                AverageFPS = sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFPS = CurrentFPS;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
            return true;
        }
    }
}
