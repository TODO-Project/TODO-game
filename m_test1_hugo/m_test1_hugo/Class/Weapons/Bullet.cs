using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using m_test1_hugo.Class.Main.interfaces;

namespace m_test1_hugo.Class.Weapons
{
    class Bullet : Sprite, TileCollision
    {
        private float posX, posY;

        public float _angleTir,  distanceParcourue = 0;

        public bool sensPositif;

        public static List<Bullet> BulletList = new List<Bullet> { };

        public Weapon _weapon;

        public Bullet(Weapon weapon, float angleTir)
        {
            this._weapon = weapon;
            this._angleTir = angleTir;

            Position = weapon.Holder.Center;
            sensPositif = weapon.Holder.CO > 0;


            posX = Position.X;
            posY = Position.Y;
            BulletList.Add(this);
            
        }

        public override void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Bullets/"+_weapon.bulletSprite);
        }


        public void Update(GameTime gametime, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            if ((posX >= (Game1.mapWidth * tileSize) || posY >= (Game1.mapHeight * tileSize) || posX < 0 || posY < 0) || TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 0))
                BulletList.Remove(this);
            else
            {
                //int parcouru;
                if (sensPositif)
                {
                    posY += (float)(Math.Sin(_angleTir) * _weapon.bulletSpeed);
                    posX += (float)(Math.Cos(_angleTir) * _weapon.bulletSpeed);
                    Position = new Vector2(posX, posY);
                }
                else
                {
                    posY -= (float)(Math.Sin(_angleTir)) * _weapon.bulletSpeed;
                    posX -= (float)(Math.Cos(_angleTir) ) * _weapon.bulletSpeed;
                    Position = new Vector2(posX, posY);
                }


                for (var j = 0; j < Character.CharacterList.Count; j++)
                {
                    if (Character.CharacterList[j].team._teamNumber != _weapon.Holder.team._teamNumber)
                    {
                        Character currentCharacter = Character.CharacterList[j];


                        if (this.SpriteCollision(currentCharacter.sourceRectangle))
                        {
                            currentCharacter.Health -= this._weapon.Damages;
                            //Console.WriteLine(currentPlayer.Health);
                            BulletList.Remove(this);
                        }
                    }
                }
            }
        }

        public bool SpriteCollision(Rectangle objet)
        {
            return (this.Bounds.Intersects(objet));
        }

        public bool TileCollision(Sprite objet1, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, int direction)
        {
            int tileX = (int)Math.Ceiling(((this.Center.X) / mapWidth) - 1);
            int tileY = (int)Math.Ceiling(((this.Center.Y) / mapHeight) - 1);

            if (tileX > (mapWidth - 1) || tileY > (mapHeight - 1))
            {
                return false;
            }
            else
            {
                return (this.Bounds.Intersects(new Rectangle(tileX * 32, tileY * 32, tileSize, tileSize)) && !collisionLayer.GetTile(tileX, tileY));
            }
            
        }
    }
}
