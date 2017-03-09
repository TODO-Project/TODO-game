/*using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using m_test1_hugo.Class.Characters.Teams;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Characters;
using m_test1_hugo.Class.ControlLayouts;
using m_test1_hugo.Class.Main.outils_dev_jeu.ControlLayouts;
using m_test1_hugo.Class.Main.Menus.pages;
using m_test1_hugo.Class.Weapons;

namespace m_test1_hugo.Class.gamestates.tools
{
    class TeamPicker : Sprite
    {
        private bool hold;
        private Color activeColor;
        
        private SpriteFont font;
        private int count;
        public Team activeTeam;
        //Texture2D UpArrow, DownArrow;
        

        #region properties
        public Color ActiveColor
        {
            get
            {
                return activeColor;
            }

            set
            {
                activeColor = value;
            }
        }

        public SpriteFont Font
        {
            get
            {
                return font;
            }

            set
            {
                font = value;
            }
        }

        public bool Hold
        {
            get
            {
                return hold;
            }

            set
            {
                hold = value;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

      

        #endregion

        public TeamPicker(Vector2 Position)
        {
            
            this.Position = Position;
            LoadContent(Game1.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            
          
            Font = content.Load<SpriteFont>("font");
            TextPosition = new Vector2(Center.X, Position.Y + 50);
            TeamNamePosition  = new Vector2(Center.X, Center.Y + 50);
            
        }

        public void Update()
        {
            
        }

        public  new void Draw(SpriteBatch spriteBatch)
        {
            //(float)Math.Tan(act - Mouse.GetState().Position.Y/Mouse.GetState().Position.X);
            Console.WriteLine(ActivePlayer.MouseRotationAngle);
            
            
            
            //spriteBatch.DrawString(Font, activeTeam._name, TeamNamePosition, ActiveColor);
        }
    }
}
*/