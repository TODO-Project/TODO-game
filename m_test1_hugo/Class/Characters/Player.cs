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
using Lidgren.Network;

namespace m_test1_hugo.Class.Main
{
    /// <summary>
    /// un joueur humain, controlable par l'utilisateur
    /// </summary>
    public class Player : Character, Movable
    {
        #region attributs
        public HealthBar healthBar; //chaque joueur possede une barre de vie
        public new ControlLayout Controls; // on peut choisir un mode de controle (azerty/qwerty/manette)
        public bool updateClothes = false; //booléen qui se déclenche lorsqu'on change de vêtements

        /// <summary>
        /// tableau de vêtements :
        ///     1- T-shirt
        ///     2- Pantalon
        ///     3- Chaussures
        /// </summary>
        public Cloth[] ClothesList = new Cloth[3];

        public List<SoundEffect> killVoices = new List<SoundEffect> { };

        /// <summary>
        /// CA : Coté Adjacent, utilisé en trigo pour la rotation de l'arme
        /// CO : Côté opposé
        /// </summary>
        public float CA, CO;

        public int MoveSpeedBonus;
        private int moveSpeed; 

        public int damageBonus;

        private int kills;
        
        private int deaths;

        private int serie;

        private long id;

        private float remoteCursorX;
        private float remoteCursorY;
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

        public int MaxHealth
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

        public long Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public float RemoteCursorX
        {
            get
            {
                return remoteCursorX;
            }

            set
            {
                remoteCursorX = value;
            }
        }

        public float RemoteCursorY
        {
            get
            {
                return remoteCursorY;
            }

            set
            {
                remoteCursorY = value;
            }
        }


        #endregion

        #region constructeur
        public Player(CharacterClass classe, Weapon weapon, Team team, ControlLayout controlLayout, Vector2 Position)
        {
            #region sons
            for (int i = 1; i <= 8; i++)
            {
                killVoices.Add(Game1.Content.Load<SoundEffect>("audio/kills/" + i + "kill"));
            }

            #endregion
            this.weapon = weapon;
            if (weapon != null)
            {
                weapon.Holder = this;
            }
            this.classe = classe;
            this.Health = classe.Health;
            this.team = team;
            this.MaxHealth = Health;
            this.Controls = controlLayout;
            this.Position = Position;
            healthBar = new HealthBar(this);
            Console.WriteLine(Position);
            Pseudo = "Jean-kevin";
            LoadContent(Game1.Content);
            team.TeamPlayerList.Add(this);
        }


        /// <summary>
        /// même constructeur, avec la possibilité de choisir un pseudo
        /// </summary>
        /// <param name="classe"></param>
        /// <param name="weapon"></param>
        /// <param name="team"></param>
        /// <param name="controlLayout"></param>
        /// <param name="Position"></param>
        public Player(string pseudo, CharacterClass classe, Weapon weapon, Team team, ControlLayout controlLayout, Vector2 Position)
            :this(classe, weapon, team, controlLayout, Position)
        {
            if (pseudo != "" && pseudo != null)
                this.Pseudo = pseudo;
            else
                this.Pseudo = "Jean-kevin";
        }

        public Player(string pseudo, CharacterClass classe, Weapon weapon, Team team, ControlLayout controlLayout, Vector2 Position, long id)
            : this (pseudo, classe, weapon, team, controlLayout, Position)
        {
            Id = id;
        }
        #endregion

        #region deplacement + MouseRotation
        public void Control(GameTime gametime, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            if (this == GamePage.player)
            {
                Update(gametime);
                if (Controls is Azerty || Controls is Qwerty)
                {
                    CA = -(this.Center.Y - Controls.CursorPosY);
                    CO = -(this.Center.X - Controls.CursorPosX);
                }

                MouseRotationAngle = (float)(Math.Atan(CA / CO));

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
                
            }
            else
            {
                Update(gametime);
            }
            #endregion
        }

        /// <summary>
        /// ici, on gère le personnage présent dans le menu
        /// </summary>
        /// <param name="gametime"></param>
        public void Control(GameTime gametime)
        {
            if (Controls.Shoot)
            {
                shoot(MouseRotationAngle, WeaponPicker.CO > 0);
                if(weapon.tir != Weapon.methodeTir.auto)
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
                {
                    shoot(MouseRotationAngle);
                }

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
                // si le personnage est mort, on l'enleve de la liste des joueurs a dessiner mais pas de la liste des 
                // personnages car il est toujours en jeu et peut réapparaitre
                GamePage.PlayersToDraw.Remove(this);
                Deaths++;
                Serie = 0;
                GamePage.client.SendDeathMessage(Id);
            } 
        }

        /// <summary>
        /// Méthode pour pouvoir réapparaitre a une position "spawn" donnée
        /// </summary>
        /// <param name="spawn"></param>
        public void Respawn(Vector2 spawn, List<Player> liste)
        {
            this.Position = spawn;
            this.Health = this.MaxHealth;
            liste.Add(this);//on ajoute le joueur a la liste afin qu'il puisse etre dessiné a nouveau
        }

        /// <summary>
        /// méthode permettant de jouer un son qui diffère en fonction de la série d'éliminations en cours
        /// </summary>
        /// <param name="serie"></param>
        public void PlayVoiceKill(int serie)
        {
            if(serie < killVoices.Count)
            {
                killVoices[serie].Play();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Player))
            {
                return false;
            }
            Player p = obj as Player;
            return Id == p.Id;
        }
    }
}
