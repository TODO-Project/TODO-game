using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Main.outils_dev_jeu.Affects;
using m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette;
using Microsoft.Xna.Framework.Audio;

namespace m_test1_hugo.Class.Bonuses
{
    class MagicBox : Bonus
    {
        public bool isOpen = false;
        public bool Validated = false;
        public bool TimerStarted = false;
        public bool FoundRandomWeapon = false;
        public bool pressButtonMsg = false;
        private bool tempoStarted = false;
        public bool musicPlayed = false;

        private int currentFake = 0;
        private int tempoDuration = 1500; // milliseconds
        private int chronoDuration = 5000; // milliseconds
        

        DateTime chrono, tempo;
        Weapon randomWeapon;
        WeaponPic weaponPic, fakewPic;
        Player currentPlayer = Game1.player;
        SoundEffect music;

        public override void interract(Player player)
        {
            CharacterAffect.WeaponChange(player, randomWeapon);
            WeaponPic.WeaponPicList.Remove(weaponPic);
        }

        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "Bonus/magicBox", 2, 1);
            music = content.Load<SoundEffect>("audio/bonus/magicbox");
        }

        public MagicBox()
        {
            name = "magicBox";
            BonusList.Add(this);
            currentRow = 1;
        }

        public void RandomWeapon()
        {
            Random rnd = new Random();
            int RandInt = rnd.Next(Weapon.List.Count<Weapon>());
            randomWeapon = Weapon.List[RandInt];
            weaponPic = new WeaponPic(randomWeapon, Position);
            chrono = DateTime.Now;
            TimerStarted = true;
            FoundRandomWeapon = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.SpriteCollision(currentPlayer.destinationRectangle))
            {
                this.pressButtonMsg = true;
                if (Game1.kb.IsKeyDown(Keys.E) && !isOpen)
                    isOpen = true;
            }
            else
            {
                this.pressButtonMsg = false;
            }
            if (isOpen)
            {
                if (!musicPlayed)
                {
                    music.Play();
                    musicPlayed = true;
                }
                this.pressButtonMsg = false;
                currentRow = 0; // sprite ouvert
                if (!FoundRandomWeapon) // on va afficher les armes dans l'ordre 
                {

                    if (!tempoStarted)
                    {
                        tempo = DateTime.Now;
                        tempoStarted = true;
                        fakewPic = new WeaponPic(Weapon.List[currentFake], Position);
                    }
                    else
                    {
                        if (currentFake < Weapon.List.Length)
                        {
                            if (DateTime.Now > tempo.AddMilliseconds(tempoDuration))
                            {
                                tempoStarted = false;
                                WeaponPic.WeaponPicList.Remove(fakewPic);
                                currentFake++;
                                if (currentFake >= Weapon.List.Length) // si on a affiche toutes les armes, on en prend une vraie au hasard
                                {
                                    RandomWeapon();
                                }
                            }
                        }
                    }
                }
                else if (SpriteCollision(currentPlayer.destinationRectangle)) // si le joueur est sur la boite et qu'il peut prendre l'arme
                {
                    weaponPic.takeWeaponMsg = true;
                    if (Game1.kb.IsKeyDown(Keys.E))
                    {
                        interract(currentPlayer);
                        BonusList.Remove(this);
                    }
                }

                if (TimerStarted)
                {
                    if (DateTime.Now > chrono.AddMilliseconds(chronoDuration))
                    {
                        WeaponPic.WeaponPicList.Remove(weaponPic);
                        BonusList.Remove(this);
                    }
                    if (!SpriteCollision(currentPlayer.destinationRectangle))
                    {
                        weaponPic.takeWeaponMsg = false;
                    }
                }
            }
        }
    }
}
