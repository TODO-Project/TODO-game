using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace m_test1_hugo.Class.Weapons
{
    class Sniper : Weapon
    {
        public Character Holder
        {
            get { return Holder; }
            set { Holder = value; }
        }

        public new Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sniper");
        }

        public Sniper(Character holder)
        {
            this.Name = "sniper";
            /* this.Height = height;
             this.Width = width;*/
            this.ReloadingTime = 5000;
            this.MagazineSize = 3;
            this.RearmingTime = 2000;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 100;
            this.Holder = holder;
        }
    }
}