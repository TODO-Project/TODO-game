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

namespace m_test1_hugo.Class.Bonuses
{
    class MagicBox : Bonus
    {
        public bool isOpen = false;
        public bool Validated = false;
        public bool TimerStarted = false;
        public bool TempoStarted = false;
        public bool FoundRandomWeapon = false;
        public bool pressButtonMsg = false;


        DateTime chrono;
        Weapon randomWeapon;
        WeaponPic weaponPic;

        public override void interract(Character character)
        {
            CharacterAffect.WeaponChange(Game1.player, randomWeapon);
            WeaponPic.WeaponPicList.Remove(weaponPic);
        }

        public override void LoadContent(ContentManager content)
        {
            //texture = content.Load<Texture2D>("Bonus/magicBox");
            LoadContent(content, "Bonus/magicBox", 2, 1);
        }

        public MagicBox()
        {
            name = "magicBox";
            BonusList.Add(this);
        }

        public void FakePic()
        {
            /*for(var i=0; i<Weapon.List.Count<Weapon>();)
            {
                if (!TempoStarted)
                {
                    InitTempo = DateTime.Now;
                    fakePic = new WeaponPic(Weapon.List[i], Position);
                    TempoStarted = true;
                }
                else
                {
                    if(DateTime.Now > InitTempo.AddMilliseconds(tempo))
                    {
                        WeaponPic.WeaponPicList.Remove(fakePic);
                        TempoStarted = false;
                        i++;
                    }
                }
            }*/
            Random rnd = new Random();
            int RandInt = rnd.Next(Weapon.List.Count<Weapon>());
            randomWeapon = Weapon.List[RandInt];
            weaponPic = new WeaponPic(randomWeapon, Position);
            DateTime Tempo = DateTime.Now;
            FoundRandomWeapon = true;
        }

        public override void Update(GameTime gameTime)
        {
            Player currentPlayer = Game1.player;

                if (this.SpriteCollision(currentPlayer.destinationRectangle) && Game1.kb.IsKeyDown(Keys.E) && !isOpen)
                    isOpen = true;

                if (isOpen)
                { 
                    currentRow = 0; // sprite ouvert

                    if (!FoundRandomWeapon)
                    {
                        FakePic();
                    }


                    if (Game1.kb.IsKeyUp(Keys.E))
                        Validated = true;
                    if (Validated && SpriteCollision(currentPlayer.destinationRectangle)) // si on valide le choix de l'arme
                    {
                        pressButtonMsg = false;
                        weaponPic.takeWeaponMsg = true;
                        if (Game1.kb.IsKeyDown(Keys.E))
                        {
                            interract(currentPlayer);
                            BonusList.Remove(this);
                        }

                    }
                    else
                        weaponPic.takeWeaponMsg = false;

                    if (!TimerStarted)
                    {
                        chrono = DateTime.Now;
                        TimerStarted = true;
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        if(now > chrono.AddSeconds(10) )
                        {
                            BonusList.Remove(this);
                            WeaponPic.WeaponPicList.Remove(weaponPic);
                        }
                    }
                }
                else
                {
                    if (SpriteCollision(currentPlayer.destinationRectangle))
                    {
                        pressButtonMsg = true;
                    }
                    else
                        pressButtonMsg = false;
                    currentRow = 1; // ferme
                }
            }
        }
    }
