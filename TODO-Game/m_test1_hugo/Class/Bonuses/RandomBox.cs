using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Bonuses
{
    public abstract class RandomBox :Bonus
    {

        public abstract bool isOpen { get; set; }
        public abstract bool pressButtonMsg { get; set; }
        public abstract bool FoundRandom { get; set; }
        public abstract bool Validated { get; set; }
        public abstract bool TimerStarted { get; set; }
        public abstract int chronoDuration { get; set; }
        public abstract bool musicPlayed { get; set; }
        public abstract DateTime chrono { get; set; }
        public abstract SoundEffect sound { get; set; }

        public RandomBox(Vector2 position)
        {
            Position = position;
            isOpen = false;
            pressButtonMsg = false;
            FoundRandom = false;
            Validated = false;
            TimerStarted = false;
            musicPlayed = false;
            LoadContent(Game1.Content);
        }
    }
}
