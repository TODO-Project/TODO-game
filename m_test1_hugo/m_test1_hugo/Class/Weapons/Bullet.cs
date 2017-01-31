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
using Microsoft.Xna.Framework.Audio;

namespace m_test1_hugo.Class.Weapons
{
    class Bullet : Sprite, TileCollision
    {
        private SoundEffect Hitmarker;
        private float parcouru;

        public float _angleTir;

        public float Inc_X
        {
            get
            {
                return (float)(Math.Cos(_angleTir)) * _weapon.bulletSpeed;
            }
        }

        public float Inc_Y
        {
            get
            {
                return (float)(Math.Sin(_angleTir)) * _weapon.bulletSpeed;
            }
        }

        public Vector2 Inc_vector
        {
            get
            {
                if(sensPositif)
                    return new Vector2(Inc_X, Inc_Y);
                else
                    return new Vector2(-Inc_X, -Inc_Y);
            }
        }

        public bool sensPositif;

        public Vector2 Origin;

        public static List<Bullet> BulletList = new List<Bullet> { };

        public Weapon _weapon;

        public Bullet(Weapon weapon, float angleTir)
        {
            this._weapon = weapon;
            this._angleTir = angleTir;
            this.Origin = weapon.Holder.Center;
            Position = weapon.Holder.Center;
            sensPositif = weapon.Holder.CO > 0;

            BulletList.Add(this);            
        }

        public override void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Bullets/"+_weapon.bulletSprite);
            Hitmarker = content.Load<SoundEffect>("audio/weapons/hitmarker/hitmarker");
        }


        public void Update(GameTime gametime, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            if (Position.X <= 0 || Position.Y <= 0 || TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 0) || parcouru >= _weapon.Range)
                BulletList.Remove(this);
            else
            {
                Position += Inc_vector;

                parcouru = (float)(Math.Sqrt( Math.Pow(Origin.X-Position.X, 2) + Math.Pow(Origin.Y-Position.Y, 2) ));

                for (var j = 0; j < Character.CharacterList.Count; j++)
                {
                    if (Character.CharacterList[j].team._teamNumber != _weapon.Holder.team._teamNumber)
                    {
                        Character currentCharacter = Character.CharacterList[j];

                        if (this.SpriteCollision(currentCharacter.destinationRectangle))
                        {
                            currentCharacter.Health -= (this._weapon.Damages);
                            Hitmarker.Play();
                            BulletList.Remove(this);
                            Console.WriteLine(currentCharacter.Health);
                            Console.WriteLine(this._weapon.Damages);
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
            int tileX = (int)Math.Ceiling(((this.Center.X) / tileSize) - 1);
            int tileY = (int)Math.Ceiling(((this.Center.Y) / tileSize) - 1);

            if (tileX > (mapWidth - 1) || tileY > (mapHeight - 1))
            {
                return false;
            }
            else
            {
                return (this.Bounds.Intersects(new Rectangle(tileX * 32, tileY * 32, tileSize, tileSize)) && !collisionLayer.GetTile(tileX, tileY));
            }
            
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, _angleTir, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
