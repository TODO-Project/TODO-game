using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Main
{
    class Player : Character
    {
        

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }
    }
}
