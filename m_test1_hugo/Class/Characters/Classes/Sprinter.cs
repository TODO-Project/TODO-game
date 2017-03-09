using System;
using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Characters
{
    class Sprinter : CharacterClass
    {
        //sprinter : peu de HP mais court vite

        public Sprinter()
        {
            MoveSpeed = 6;
            this.DamageBonus= 0;
            this.Health = 80;
        }
    }
}
