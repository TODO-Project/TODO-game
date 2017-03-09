using m_test1_hugo.Class.Main.Menus;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace m_test1_hugo.Class.gamestates.pages.Editor
{
    class SmallButton : Button
    {
        public SmallButton(string text) 
            : base(text)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            texture = Game1.Content.Load<Texture2D>("menu/smallButton");
        }
    }
}
