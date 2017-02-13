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
using m_test1_hugo.Class.Main.Menus.pages;
using Microsoft.Xna.Framework.Audio;
using m_test1_hugo.Class.gamestates.tools;

namespace m_test1_hugo.Class.Main
{
    public class Player : Character, Movable
    {
        #region attributs
        public HealthBar healthBar;
        public new ControlLayout Controls;
        public bool updateClothes = false;
        public Cloth[] ClothesList = new Cloth[3];
        public List<SoundEffect> killVoices = new List<SoundEffect> { };
        //public SoundEffect multikill = Game1.Content.Load<SoundEffect>("audio/kills/multikill");

        public float CA, CO;

        public int MoveSpeedBonus;
        private int moveSpeed; 

        public int damageBonus;
        
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

        private int kills;
        
        private int deaths;

        private int serie;
        #endregion

        #region properties
        public int Kills
        {
            get
            {
                return kills;
            }

            set
            {
                if (value >= 0)
                    kills = value;
                else
                    throw new Exception("kills ne peut etre negatif");
            }
        }
        public int Deaths
        {
            get
            {
                return deaths;
            }

            set
            {
                deaths = value;
            }
        }
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
        public int Serie
        {
            get
            {
                return serie;
            }

            set
            {
                serie = value;
            }
        }
        #endregion

        #region constructeur
        public Player(string pseudo, CharacterClass classe, Weapon weapon, Team team, ControlLayout controlLayout, Vector2 Position)
        {
            #region sons
            for(int i = 1; i <= 8; i++)
            {
                killVoices.Add(Game1.Content.Load<SoundEffect>("audio/kills/"+i+"kill"));
            }
           
            #endregion
            this.weapon = weapon;
            if(weapon != null)
            {
                weapon.Holder = this;
            }
            this.classe = classe;
            this.Health = classe.Health;
            GamePage.PlayerList.Add(this);
            this.team = team;
            this.MaxHealth = Health;
            this.Controls= controlLayout;
            this.Position = Position;
            healthBar = new HealthBar(this);
            Console.WriteLine(Position);
            if (pseudo != "")
                this.Pseudo = pseudo;
            else
                this.Pseudo = "Jean-kevin";
            LoadContent(Game1.Content);
        }
        public Player(CharacterClass classe, Weapon weapon, Team team, ControlLayout controlLayout, Vector2 Position)
        {
            this.weapon = weapon;
            if (weapon != null)
            {
                weapon.Holder = this;
            }
            this.classe = classe;
            this.Health = classe.Health;
            if (GamePage.PlayerList != null)
                GamePage.PlayerList.Add(this);
            this.team = team;
            this.MaxHealth = Health;
            this.Controls = controlLayout;
            this.Position = Position;
            healthBar = new HealthBar(this);
            Console.WriteLine(Position);
            Pseudo = "Jean-kevin";
            LoadContent(Game1.Content);
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
        public void Control(GameTime gametime)
        {
            if (Controls.Shoot)
            {
                shoot(MouseRotationAngle, WeaponPicker.CO > 0);
                if(weapon.type != "auto")
                {
                    if(weapon is Shotgun)
                        weapon.RearmingTime = 1000;
                    else
                        weapon.RearmingTime = 400;
                }
                if (weapon.CurrentAmmo <=0)
                    weapon.CurrentAmmo = weapon.MagazineSize;
            }
            this.releasedGachette = true;
        }
        #endregion

        public void Update(GameTime gametime)
        {
            UpdateSprite(gametime); // update du sprite animé
           
            if (weapon != null)
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
            {
                GamePage.PlayersToDraw.Remove(this);
                Deaths++;
                Serie = 0;
            } 
        }
        
        public void Respawn(Vector2 spawn, List<Player> liste)
        {
            this.Position = spawn;
            this.Health = this.MaxHealth;
            liste.Add(this);
        }

        public void PlayVoiceKill(int serie)
        {
            if(serie < killVoices.Count)
            {
                killVoices[serie].Play();
            }
        }
    }
}
