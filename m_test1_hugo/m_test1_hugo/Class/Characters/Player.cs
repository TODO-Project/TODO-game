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
using m_test1_hugo.Class.Characters.Teams;
using entrainementProjet1.Class.Main;
using m_test1_hugo.Class.ControlLayouts;
using m_test1_hugo.Class.Main.Menus.healthBar;

namespace m_test1_hugo.Class.Main
{
    public class Player : Character, Movable
    {
        public new ControlLayout Controls;
        public bool updateClothes = false;
        public Cloth[] ClothesList = new Cloth[3];

        public HealthBar healthBar;

        public float CA, CO;

        public int MoveSpeedBonus;

        private int moveSpeed;
        public override int MoveSpeed
        {
            get
            {
                moveSpeed = classe.MoveSpeed;
                if (ClothesList[2] != null)
                    moveSpeed += ClothesList[2].Bonus;

                if (weapon != null)
                    moveSpeed -= weapon.MovingMalus;

                return moveSpeed + MoveSpeedBonus;
            }

            set
            {
                moveSpeed = value;
            }
        }

        public int damageBonus;
        public int DamageBonus
        {
            get
            {
                if (ClothesList[0] != null)
                    return classe.DamageBonus + ClothesList[0].Bonus;
                else
                    return classe.DamageBonus;
            }
        }

        public new int MaxHealth
        {
            get
            {
                if (ClothesList[1] != null)
                    return _maxHealth + ClothesList[1].Bonus;
                else
                    return _maxHealth;

            }
            set { _maxHealth = value; }
        }

        #region constructeur
        public Player(CharacterClass classe, Weapon weapon, Team team, ControlLayout controlLayout, Vector2 Position)
        {
            this.weapon = weapon;
            if(weapon != null)
            {
                weapon.Holder = this;
            }
            this.classe = classe;
            this.Health = classe.Health;
            CharacterList.Add(this);
            this.team = team;
            this.MaxHealth = Health;
            this.Controls= controlLayout;
            this.Position = Position;
            healthBar = new HealthBar(this);
        }
        #endregion

        #region deplacement + MouseRotation
        public void Control(GameTime gametime, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            Update(gametime);
            if(Controls is Azerty || Controls is Qwerty)
            {
                CA = -(this.Center.Y - Controls.CursorPosY);
                CO = -(this.Center.X - Controls.CursorPosX);
            }

            MouseRotationAngle = (float)(Math.Atan(CA / CO ));

            isMoving = false; // pas en mouvement

            #region mouvement du personnage 

            if (Controls.MoveLeft)
            {
                moveLeft(tileSize, mapWidth, mapHeight, collisionLayer);
            }

            if (Controls.MoveDown)
            {
                moveDown(tileSize, mapWidth, mapHeight, collisionLayer);
            }

            if (Controls.MoveUp)
            {
                moveUp(tileSize, mapWidth, mapHeight, collisionLayer);
            }

            if (Controls.MoveRight)
            {
                moveRight(tileSize, mapWidth, mapHeight, collisionLayer);
            }
            #endregion

            #region rotation du perso en fonction de la souris 

            //Console.WriteLine("CA :" + CA + " CO :" + CO );

            if (Controls.CursorPosX > Center.X && Controls.CursorPosY > Center.Y) // bas droit
            {
                if (Controls.CursorPosX - Center.X > Controls.CursorPosY - Center.Y) // droit
                {
                    currentRow = 2;
                     //Console.WriteLine("1.1");
                }

                else // bas
                {
                    currentRow = 0;
                     //Console.WriteLine("1.2");
                }
            }
            else if (Controls.CursorPosX < Center.X && Controls.CursorPosY < Center.Y)// haut gauche
            {
                if (Center.X - Controls.CursorPosX < Center.Y - Controls.CursorPosY) // gauche
                {
                    //Console.WriteLine("2.1");
                    currentRow = 1;
                }

                else // haut
                {
                    currentRow = 3;
                    //Console.WriteLine("2.2");
                }

            }
            else if (Controls.CursorPosX < Center.X && Controls.CursorPosY > Center.Y) // bas gauche
            {
                //Console.WriteLine("ok3");
                if (Math.Abs((Controls.CursorPosX - Center.X)) > Math.Abs((Controls.CursorPosY - Center.Y))) // gauche
                {
                    currentRow = 3;
                    //Console.WriteLine("3.1");               
                }

                else // bas
                {
                    currentRow = 0;
                    //Console.WriteLine("3.2");
                }

            }
            else if (Controls.CursorPosX > Center.X && Controls.CursorPosY < Center.Y)// haut droit
            {
                //Console.WriteLine("ok4");
                if (Controls.CursorPosX - Center.X > (Center.Y - Controls.CursorPosY)) // droit
                {
                    //Console.WriteLine("4.1");
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

        public void Update(GameTime gametime)
        {
            this.UpdateSprite(gametime); // update du sprite animé
            
            if(weapon != null)
            {
                if (Controls.Shoot)
                    shoot(MouseRotationAngle);

                if (Controls.Reload && !weapon.isFull)
                {
                    if (!weapon.NeedReloading)
                    {
                        InitReloading = DateTime.Now;
                        weapon.NeedReloading = true;
                    }
                }
            }

            UpdateCharacter(gametime);

            if (IsDead())
                CharacterList.Remove(this);
        }
    }
}
