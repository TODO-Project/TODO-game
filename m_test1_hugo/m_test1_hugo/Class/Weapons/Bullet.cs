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

        public float _angleTir,  distanceParcourue = 0;

        public bool sensPositif;

        public static List<Bullet> BulletList = new List<Bullet> { };

        private Weapon _weapon;

        public Bullet(Weapon weapon, float angleTir)
        {
            this._weapon = weapon;
            this._angleTir = angleTir;
            Position = weapon.Holder.Center;
            posX = Position.X;
            posY = Position.Y;
            BulletList.Add(this);
            sensPositif = weapon.Holder.CO > 0;
        }

        public override void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Bullets/"+_weapon.bulletSprite);
        }


        public void Update(GameTime gametime)
        {
            if (posX >= Game1.WindowWidth || posY >= Game1.WindowHeight || posX < 0 || posY < 0)
                BulletList.Remove(this);
            else
            {
                if(sensPositif)
                {
                    posY += (float)(Math.Sin(_angleTir) * _weapon.Range)/ (1/_weapon.bulletSpeed);
                    posX += (float)(Math.Cos(_angleTir) * _weapon.Range) / (1/_weapon.bulletSpeed);
                    Position = new Vector2(posX, posY);
                }
                else
                {
                    posY -= (float)(Math.Sin(_angleTir) * _weapon.Range) / (1 / _weapon.bulletSpeed);
                    posX -= (float)(Math.Cos(_angleTir) * _weapon.Range) / (1 / _weapon.bulletSpeed);
                    Position = new Vector2(posX, posY);
                }
            }
        }
    }
}
