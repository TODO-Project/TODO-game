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
using m_test1_hugo.Class.Characters.Teams;

namespace m_test1_hugo.Class.Main
{
    public class Player : Character, Movable
    {

        #region attributs

        #endregion

        #region constructeur
        public Player(Character classe, Weapon weapon, Team team)
        {
            this.weapon = weapon;
            weapon.Holder = this;
            MoveSpeed = classe.MoveSpeed - weapon.MovingMalus;
            this.Health = classe.Health;
            CharacterList.Add(this);
            this.team = team;
            this.MaxHealth = Health;
        }
        #endregion


        #region deplacement + MouseRotation
        public void Control(GameTime gametime, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            Update(gametime);

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
                moveDown(tileSize, mapWidth, mapHeight, collisionLayer);
            }

            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z))
            {
                moveUp(tileSize, mapWidth, mapHeight, collisionLayer);
            }

            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                moveRight(tileSize, mapWidth, mapHeight, collisionLayer);
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

        #region rechargement de l'


        #region methodes 

        internal bool CurrentlyRearming()
            {
                if (weapon.NeedRearming )
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
            if (!weapon.NeedReloading)
            {
                if (!this.weapon.isEmpty)
                {
                    if (!this.CurrentlyRearming())
                    {
                        if (!this.weapon.NeedRearming)
                        {
                            Random rnd = new Random();
                            float precision = rnd.Next((int)(-10 * weapon.accuracy_malus), (int)(10 * (weapon.accuracy_malus)));//////
                            new Bullet(this.weapon, MouseRotationAngle + precision / 20);
                            this.weapon.CurrentAmmo--;
                            this.weapon.NeedRearming = true;
                            InitRearming = DateTime.Now;
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        public void Update(GameTime gametime)
        {

            this.UpdateSprite(gametime); // update du sprite animé

            if (Game1.ms.LeftButton == ButtonState.Pressed )
                shoot();

            

            if (Game1.kb.IsKeyDown(Keys.R) && !weapon.isFull)
            {
                if (!weapon.NeedReloading)
                {
                    InitReloading = DateTime.Now;
                    weapon.NeedReloading = true;
                }
            }
            UpdateCharacter(gametime);


            if (IsDead())
                CharacterList.Remove(this);
        }

    }
}
