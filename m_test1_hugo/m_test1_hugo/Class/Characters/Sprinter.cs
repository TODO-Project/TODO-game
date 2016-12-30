using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Characters
{
    class Sprinter : Character
    {
        public Sprinter()
        {
            MoveSpeed = 5;
            CharacterClass = this.GetType().Name;
            this.DamageMultiplier = 1;
            this.Health = 80;
            this.Armor = 0;
            //this.Clothing = ();

        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }
    }
}
