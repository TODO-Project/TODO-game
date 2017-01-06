using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m_test1_hugo.Class.Weapons;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Main.interfaces;
using m_test1_hugo.Class.Main.InputSouris;

namespace m_test1_hugo.Class.Main
{
    public class Player : Character, Movable
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

        #region attributs
        public Weapon weapon;
        public static List<Player> PlayerList = new List<Player>();
        #endregion

        #region constructeur
        public Player(Character classe, Weapon weapon)
        {
            this.weapon = weapon;
            weapon.Holder = this;
            MoveSpeed = classe.MoveSpeed;
        }
        #endregion

        #region dessin
        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "playerSP2", 4, 3);
            weapon.LoadContent(content);
        }
        public void DrawPlayer(SpriteBatch spriteBatch)
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

        #region deplacement + MouseRotation
        public void MovePlayer(GameTime gametime, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            this.Update(gametime);

            MouseRotationAngle = (float)(Math.Atan(CA / CO));
            //Console.WriteLine(MouseRotationAngle);

            KeyboardState state = Keyboard.GetState();

            isMoving = false; // pas en mouvement

            #region mouvement du personnage 

            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
            {
                moveLeft(tileSize, mapWidth, mapHeight, collisionLayer);
            }

            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                moveDown();
            }

            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z))
            {
                moveUp();
            }

            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                moveRight();
            }
            #endregion

            #region rotation du perso en fonction de la souris 

            if (MouseLib.posX > Center.X && MouseLib.posY > Center.Y) // bas droit
            {
                if (MouseLib.posX - Center.X > MouseLib.posY - Center.Y) // droit
                {
                    currentRow = 2;
                    // Console.WriteLine("1.1");
                }

                else // bas
                {
                    currentRow = 0;
                    // Console.WriteLine("1.2");
                }
            }
            else if (MouseLib.posX < Center.X && MouseLib.posY < Center.Y)// haut gauche
            {
                if (Center.X - MouseLib.posX < Center.Y - MouseLib.posY) // gauche
                {
                    // Console.WriteLine("2.1");
                    currentRow = 1;
                }

                else // haut
                {
                    currentRow = 3;
                    //  Console.WriteLine("2.2");
                }

            }
            else if (MouseLib.posX < Center.X && MouseLib.posY > Center.Y) // bas gauche
            {
                if ((Center.X - MouseLib.posX) > MouseLib.posY - Center.Y) // gauche
                {
                    currentRow = 3;
                    //  Console.WriteLine("3.1");
                }

                else // bas
                {
                    currentRow = 0;
                    // Console.WriteLine("3.2");
                }

            }
            else if (MouseLib.posX > Center.X && MouseLib.posY < Center.Y)// haut droit
            {
                if (MouseLib.posX - Center.X > (Center.Y - MouseLib.posY)) // droit
                {
                    // Console.WriteLine("4.1");
                    currentRow = 2;
                }

                else // bas
                {
                    currentRow = 1;
                    //Console.WriteLine("4.2");
                }

            }
            #endregion
        }
        #endregion

        #region methodes liees a l'arme

        #region rechargement de l'arme

            #region Attributs
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

            #region methodes 

            internal bool CurrentlyReloading()
            {
                if (weapon.NeedReloading)
                {
                    // On récupère la date
                    DateTime now = DateTime.Now;

                    // On regarde si l'on a dépasé le temps de chargement
                    if (now > this.InitReloading.AddMilliseconds(weapon.ReloadingTime))
                    {
                        // On indique que l'on a fini de chargé
                        weapon.NeedReloading = false;

                        //On recharge le chargeur 
                        weapon.CurrentAmmo = weapon.MagazineSize;

                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Reloading... you must wait !");
                    }
                }
                return false;
            }

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


            #endregion

        #endregion

        #region methodes liees au tir

        public void shoot()
        {
            if (!this.CurrentlyReloading())
            {
                if (!this.weapon.isEmpty)
                {
                    /* TO DO --  instanciation d'une balle -- */
                    if (!this.CurrentlyRearming())
                    {
                        if (!this.weapon.NeedRearming)
                        {
                            Console.WriteLine("pan ! : il reste " + (this.weapon.CurrentAmmo - 1).ToString() + " munitions");
                            new Bullet(this.weapon, MouseRotationAngle);
                            this.weapon.CurrentAmmo--;
                            this.weapon.NeedRearming = true;
                            InitRearming = DateTime.Now;
                        }
                    }
                }
                if (this.weapon.isEmpty)
                {
                    if (!this.weapon.NeedReloading)
                    {
                        Console.WriteLine("clic !");

                        // On indique que l'on a besoin de recharger 
                        this.weapon.NeedReloading = true;

                        // On enregistre la date et l'heure du rechargement
                        this.InitReloading = DateTime.Now;   
                    }
                }
            }
        }

        #endregion

        #endregion

    }
}
