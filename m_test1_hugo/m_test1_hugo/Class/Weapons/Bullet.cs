using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace m_test1_hugo.Class.Weapons
{
    class Bullet : Sprite
    {
        private float posX, posY;

        public static List<Bullet> BulletList = new List<Bullet> { };

        public Bullet(Vector2 CanonOrigin)
        {
            Position = CanonOrigin;
            posX = Position.X;
            posY = Position.Y;
            Console.WriteLine("baaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaalle");
            BulletList.Add(this);
        }

        public override void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("bullet");
        }


        public void Update(GameTime gametime)
        {
            if (posX >= Game1.WindowWidth || posY >= Game1.WindowHeight || posX < 0 || posY < 0)
                BulletList.Remove(this);
            else
            {
                posX += 10;
                posY += 10;
                Position = new Vector2(posX, posY);
            }
        }
    }
}
