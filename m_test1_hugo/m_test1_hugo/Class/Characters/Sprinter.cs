using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Characters
{
    class Sprinter : Character
    {
        public Sprinter(string pseudo)
        {
            MoveSpeed = 1.5;
            CharacterClass = this.GetType().Name;
            this.DamageMultiplier = 1;
            this.Pseudo = pseudo;
            this.Health = 100;
            this.MagicalProtection = 10;
            this.PhysicalProtection = 10;
            
            //this.Clothing = ();
            //this.hitbox = ();

        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }
    }
}
