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
using m_test1_hugo.Class.Main.Menus.pages;

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
                if (sensPositif)
                    return new Vector2(Inc_X, Inc_Y);
                else
                    return new Vector2(-Inc_X, -Inc_Y);
            }
        }

        public bool sensPositif;

        public Vector2 Origin;

        public static List<Bullet> BulletList = new List<Bullet> { };

        public Weapon _weapon;

        public Bullet(Weapon weapon, float angleTir, bool isNew)
        {
            this._weapon = weapon;
            this._angleTir = angleTir;
            this.Origin = weapon.CanonOrigin;
            Position = Origin;
            sensPositif = weapon.Holder.CO > 0;
            LoadContent(Game1.Content);
            BulletList.Add(this);
            if (GamePage.client != null && isNew)
            {
                GamePage.client.SendNewBullet(weapon.Holder.Id, angleTir);
            }
        }

        public Bullet(Weapon weapon, float angleTir, bool sens, bool isNew)
        {
            this._weapon = weapon;
            this._angleTir = angleTir;
            this.Origin = weapon.CanonOrigin;
            Position = Origin;
            sensPositif = sens;
            LoadContent(Game1.Content);
            BulletList.Add(this);
            if (GamePage.client != null && isNew)
            {
                GamePage.client.SendNewBullet(weapon.Holder.Id, angleTir);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Bullets/" + _weapon.bulletSprite);
            Hitmarker = content.Load<SoundEffect>("audio/weapons/hitmarker/hitmarker");
        }


        public void Update(GameTime gametime, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            if (Position.X <= 0 || Position.Y <= 0 || TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 0) || parcouru >= _weapon.Range)
                BulletList.Remove(this);
            else
            {
                Position += Inc_vector;
                parcouru = (float)(Math.Sqrt(Math.Pow(Origin.X - Position.X, 2) + Math.Pow(Origin.Y - Position.Y, 2)));

                for (var j = 0; j < GamePage.PlayerList.Count; j++)
                {
                    if (GamePage.PlayerList[j].team._teamNumber != _weapon.Holder.team._teamNumber)
                    {
                        Player currentCharacter = GamePage.PlayerList[j];

                        if (this.SpriteCollision(currentCharacter.destinationRectangle))
                        {
                            /*if(_weapon is shotgun && parcouru > 300)
                            {
                                currentCharacter.Health -= (int)(this._weapon.Damages - parcouru /20);
                                Console.WriteLine(parcouru);
                            }
                            else*/
                            if (this._weapon.Damages >= currentCharacter.Health)
                            {
                                _weapon.Holder.Kills++;
                                if(_weapon.Holder == GamePage.player)
                                {
                                    _weapon.Holder.Serie++;
                                    if(_weapon.Holder.Deaths %2 == 0)
                                        _weapon.Holder.PlayVoiceKill(_weapon.Holder.Serie - 1);
                                }
                                
                                _weapon.Holder.team.TeamKills++;
                            }
                             
                            currentCharacter.Health -= (this._weapon.Damages);
                            Hitmarker.Play(0.4f, 0, 0);
                            BulletList.Remove(this);
                        }
                    }
                }
            }
        }
        public void Update(GameTime gametime, int tileSize, int mapWidth, int mapHeight)
        {
            if (Position.X <= 0 || Position.Y <= 0  || parcouru >= _weapon.Range)
                BulletList.Remove(this);
            else
            {
                Position += Inc_vector;
                parcouru = (float)(Math.Sqrt(Math.Pow(Origin.X - Position.X, 2) + Math.Pow(Origin.Y - Position.Y, 2)));
            }
        }

        public bool SpriteCollision(Rectangle objet)
        {
            return (this.Bounds.Intersects(objet));
        }

        public bool TileCollision(Sprite objet1, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, int direction)
        {
            int nextX, nextY, currentX, currentY;

            if (sensPositif)
            {
                nextX = (int)Math.Ceiling((this.Center.X + (int)Inc_X) / tileSize) - 1;
                nextY = (int)Math.Ceiling((this.Center.Y + (int)Inc_Y) / tileSize) - 1;
            }
            else
            {
                nextX = (int)Math.Ceiling((this.Center.X - (int)Inc_X) / tileSize) - 1;
                nextY = (int)Math.Ceiling((this.Center.Y - (int)Inc_Y) / tileSize) - 1;
            }
            currentX = (int)Math.Ceiling((this.Center.X) / tileSize) - 1;
            currentY = (int)Math.Ceiling((this.Center.Y) / tileSize) - 1;
            //Console.WriteLine("currentX: " + nextX + "currentY: " + nextY);
            //Console.WriteLine("nextX: " + nextTileX+ "nextY: " + nextTileY);

            if (nextX > (mapWidth - 1) || nextY > (mapHeight - 1) || nextY <= 1 || nextX <= 1)
            {
                return false;
            }

            else
            {
                return (!collisionLayer.GetTile(nextX, nextY) || !collisionLayer.GetTile(currentX, currentY));
            }

        }

       
        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, _angleTir, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
        }
    }
}