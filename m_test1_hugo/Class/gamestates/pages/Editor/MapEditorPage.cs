using m_test1_hugo.Class.Main.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace m_test1_hugo.Class.gamestates.pages.Editor
{
    class MapEditorPage : MenuPage
    {
        TileSelection tileSelector;

        public MapEditorPage()
        {
           buttons.Add(new SmallButton("ok"));
            buttons[0].Position = new Vector2(400, 0);
           TileSelector = new TileSelection();
        }

        #region prop
        internal TileSelection TileSelector
        {
            get
            {
                return tileSelector;
            }

            set
            {
                tileSelector = value;
            }
        }
        #endregion

        public override MenuPage Action()
        {
            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            TileSelector.Draw(spriteBatch);
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
           
        }

        public override void Update()
        {
            // vide
        }
    }
}
