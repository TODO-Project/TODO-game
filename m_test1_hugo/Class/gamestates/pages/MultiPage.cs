using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using m_test1_hugo.Class.Main.Menus.tools;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.gamestates.tools;
using m_test1_hugo.Class.Characters.Teams;
using m_test1_hugo.Class.Weapons;
using m_test1_hugo.Class.Network;
using Lidgren.Network;
using m_test1_hugo.Class.gamestates.pages.Editor;
using m_test1_hugo.Class.Characters;
using m_test1_hugo.Class.Characters.Classes;

namespace m_test1_hugo.Class.Main.Menus.pages
{
    class MultiPage:MenuPage
    {
        private bool complete = false;
        private Texture2D bgTexture = Game1.Content.Load<Texture2D>("bg");
        private List<Button> buttonsClass;
        private Vector2 bgPosition = Vector2.Zero;
        KeyboardState kb;
        private TextInput PseudoInput, IpInput;
        private WeaponPicker wpPicker;
        private CharacterClass activeClass;
        //private TeamPicker tPicker;
        private Vector2 wpPickerPosition;

        public MultiPage()
        {
            buttonsClass = new List<Button>();

            buttons.Add(new Button("Create Game"));
            buttons.Add(new Button("Join Game"));
            buttons.Add(new Button("Back"));

            #region classes
            Button c1 = new SmallButton("sprinter");
            c1.Position = new Vector2(Game1.WindowWidth/2-c1.Width-135, 200);
            c1.value = new Sprinter();
            buttonsClass.Add(c1);

            Button c2 = new SmallButton("Tank");
            c2.Position = new Vector2(c1.Position.X + 100, c1.Position.Y);
            c2.value = new Tank();
            buttonsClass.Add(c2);
            #endregion

            PseudoInput = new TextInput("Ton pseudo :", 16);
            PseudoInput.Position = new Vector2(80, Game1.WindowHeight/2-50);
            wpPickerPosition = new Vector2(800, Game1.WindowHeight / 2 - 50);
            wpPicker = new WeaponPicker(wpPickerPosition);
            IpInput = new TextInput("Server IP (join)", 16);
            IpInput.Position = new Vector2(1000, Game1.WindowHeight/ 2 - 50);
            /*tPicker = new TeamPicker(new Vector2(775, Game1.WindowHeight / 2 - 90));
            tPicker.Position = new Vector2(Game1.WindowWidth/2+150, Game1.WindowHeight/2 -50);*/
        }


        public override void Update()
        {

        }

        public override MenuPage Action()
        {
            if(complete)
            {
                if (buttons[0].leftClick())
                {
                    GamePage.server = new Network.Server();
                    GamePage.server.Start();
                    GamePage.client = new Network.Client(Client.GetLocalIPAddress(), 12345);
                    GamePage.client.Start();

                    return new GamePage(Weapon.List[wpPicker.WeaponCount], PseudoInput.Value, wpPicker.ActiveTeam, activeClass);
                }
                else if (buttons[1].leftClick())
                {
                    GamePage.client = new Network.Client(IpInput.Value, 12345);
                    GamePage.client.Start();
                    return new GamePage(Weapon.List[wpPicker.WeaponCount], PseudoInput.Value, wpPicker.ActiveTeam, activeClass);
                }
            }

            if (buttons[2].leftClick())
                return new MainPage();
            
            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bgTexture, bgPosition, Color.White);
            kb = Keyboard.GetState();
            var currentY = 50;
            var currentX = 0;

            for (var i = 0; i < Bullet.BulletList.Count; i++)
            {
                Bullet currentBullet = (Bullet)Bullet.BulletList[i];
                currentBullet.Update(Game1.gameTime, 32, Game1.WindowWidth, Game1.WindowHeight);
                currentBullet.Draw(spriteBatch);
            }

            foreach (Button button in buttons)
            {
                if(button.Position == Vector2.Zero)
                {
                    button.Position = new Vector2(currentX, currentY);
                    currentY += 130;
                }
                button.Draw(spriteBatch);
            }

            foreach (Button button in buttonsClass)
            {
                if(button.leftClick() && !button.selected)
                {
                    foreach (Button button2 in buttonsClass)
                        button2.selected = false;

                    button.selected = true;
                    complete = true;
                    activeClass = (CharacterClass)button.value;
                }

                Color color = button.selected ? Color.Red : Color.White;

                spriteBatch.Draw(button.texture, button.Position, color);
                button.DrawText(Game1.spriteBatch);
            }

            PseudoInput.Draw(spriteBatch);
            IpInput.Draw(spriteBatch);
            wpPicker.Update();
        }
    }
}
