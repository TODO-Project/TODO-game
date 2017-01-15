using m_test1_hugo.Class.Characters.Teams;
using m_test1_hugo.Class.Main.InputSouris;
using m_test1_hugo.Class.Main.interfaces;
using m_test1_hugo.Class.Tile_Engine;
using m_test1_hugo.Class.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main
{
    public class Character : AnimatedSprite, TileCollision, SpriteCollision
    {

        #region mouseposition variables

        public float CA // cote adjacent, relatif a la hauteur
        {
            get { return MouseLib.posY - this.Center.Y; }
        }

        public float CO // cote adjacent, relatif a la largeur
        {
            get { return MouseLib.posX - this.Center.X; }
        }

        public float MouseRotationAngle;

        #endregion


        public static List<Character> CharacterList = new List<Character>();

        #region attributs

        public bool IsDead() { return this.Health <= 0; }

        public CharacterClass classe;

        public Weapon weapon;

        public Team team;

        private int moveSpeed;
        public int MoveSpeed
        {
            get { return moveSpeed;}
            set { moveSpeed = value;}
        }

        private int health;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        private int _maxHealth;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        #region chronos rechargement / rearmement
        // compteur qui se declenche quand on recharge
        private DateTime initReloading;
        public DateTime InitReloading
        {
            get { return initReloading; }
            set { initReloading = value; }
        }

        // compteur qui se declenche quand on rearme
        private DateTime initRearming;
        public DateTime InitRearming
        {
            get { return initRearming; }
            set { initRearming = value; }
        }
        #endregion

        #endregion

        #region methodes deplacement
        public void moveLeft(int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            isMoving = true;
            if (TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 0))
                this.Position = new Vector2(this.Position.X - this.MoveSpeed, this.Position.Y);
        }

        public void moveRight(int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            isMoving = true;
            if (TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 1))
                this.Position = new Vector2(this.Position.X + this.MoveSpeed, this.Position.Y);
        }

        public void moveDown(int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            isMoving = true;
            if (TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 3))
                this.Position = new Vector2(this.Position.X, this.Position.Y + this.MoveSpeed);
        }

        public void moveUp(int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            isMoving = true;
            if (TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 2))
                this.Position = new Vector2(this.Position.X, this.Position.Y - this.MoveSpeed);
        }

        #endregion

        public bool TileCollision(Sprite objet1, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, int direction)
        {
            int tileX = (int)Math.Ceiling(((this.Center.X) / mapWidth) - 1) ;
            int tileY = (int)Math.Ceiling(((this.Center.Y) / mapHeight) - 1);

            switch (direction)
            {
                case 0:     // Gauche
                    if (tileX > 0)
                        tileX--;
                    break;
                case 1:     // Droite
                    if (tileX < mapWidth)
                        tileX++;
                    break;
                case 2:     // Haut
                    if (tileY > 0)
                        tileY--;
                    break;
                case 3:     // Bas
                    if (tileY < mapHeight)
                        tileY++;
                    break;
                default:
                    break;
            }
            return collisionLayer.GetTile(tileX, tileY);
        }

        public bool SpriteCollision(Rectangle objet1)
        {
            return false;
        }

        #region dessin
        public override void LoadContent(ContentManager content)
        {
            if (team._teamNumber == 1)
            {
                LoadContent(content, "playerSP2", 4, 3);
            }
            else if (team._teamNumber == 2)
            {
                LoadContent(content, "moche", 4, 3);
            }

            weapon?.LoadContent(content);
        }

        public void DrawCharacter(SpriteBatch spriteBatch)
        {
            if (this.currentRow != 1)
            {
                this.Draw(spriteBatch);
                this.weapon.Draw(spriteBatch);
            }
            else
            {
                this.weapon.Draw(spriteBatch);
                this.Draw(spriteBatch);
            }
        }

        #endregion

        internal bool CurrentlyRearming()
        {
            if (weapon.NeedRearming)
            {
                DateTime now = DateTime.Now;
                if (now > this.InitRearming.AddMilliseconds(weapon.RearmingTime))
                {
                    weapon.NeedRearming = false;
                    return true;
                }
                else
                {
                    Console.WriteLine("Rearming... Wait");
                }
            }
            return false;
        }

        public void shoot(float AngleTir)
        {
            if (!weapon.NeedReloading)
            {
                if (!this.weapon.isEmpty)
                {
                    if (!this.CurrentlyRearming())
                    {
                        if (!this.weapon.NeedRearming)
                        {
                            float precision = 0;
                            if (weapon.Name == "minigun")
                            {
                                Random rnd = new Random();
                                precision = rnd.Next((int)(-10 * weapon.accuracy_malus), (int)(10 * (weapon.accuracy_malus)));//////
                            }
                            new Bullet(this.weapon, AngleTir + precision / 20);
                            if(weapon.Name == "shotgun")
                            {
                                new Bullet(this.weapon, AngleTir + 0.15f);
                                new Bullet(this.weapon, AngleTir - 0.15f);
                            }
                            this.weapon.CurrentAmmo--;
                            this.weapon.NeedRearming = true;
                            InitRearming = DateTime.Now;
                        }
                    }
                }
            }
        }

        public void UpdateCharacter(GameTime gametime)
        {
            if (weapon.isEmpty)
            {
                if (!weapon.NeedReloading)
                {
                    InitReloading = DateTime.Now;
                    weapon.NeedReloading = true;
                }
            }

            if (weapon.NeedReloading)
            {
                // On récupère la date
                DateTime now = DateTime.Now;

                // On regarde si l'on a dépasé le temps de chargement
                if (now > InitReloading.AddMilliseconds(weapon.ReloadingTime))
                {
                    // On indique que l'on a fini de charger
                    weapon.NeedReloading = false;

                    //On recharge le chargeur 
                    weapon.CurrentAmmo = weapon.MagazineSize;
                }
            }
        }
        //public List<Cloth> Clothing;
    }
}
