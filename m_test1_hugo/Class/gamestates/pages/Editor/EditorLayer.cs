using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static m_test1_hugo.Class.gamestates.pages.Editor.MapEditorPage;

namespace m_test1_hugo.Class.gamestates.pages.Editor
{
    class EditorLayer
    {
        #region attr
        Texture2D TileSet;
        public const int numberOfTiles = 32;
        public EditorTile[,] tiles;
        private Rectangle[,] tilesBounds;
        #endregion

        #region ctor
        public EditorLayer()
        {
            TileSet = Game1.Content.Load<Texture2D>("terrain");
            tiles = new EditorTile[numberOfTiles, numberOfTiles];
            tilesBounds = new Rectangle[numberOfTiles, numberOfTiles];
            init();
        }
        #endregion

        #region prop

        #endregion

        #region methodes
        public void init()
        {
            for (int i = 0; i < numberOfTiles; i++)
            {
                for (int j = 0; j < numberOfTiles; j++)
                {
                    Rectangle rect = new Rectangle(32 * i, 32 * j, 32, 32);
                    tilesBounds[j, i] = rect;
                    tiles[i, j] = new EditorTile(-1);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Update();
            for (int i = 0; i < numberOfTiles; i++)
            {
                for (int j = 0; j < numberOfTiles; j++)
                {
                    EditorTile currentTile = tiles[j, i];
                    if (currentTile.Index != -1)
                    {
                        int row = (int)(currentTile.Index / numberOfTiles);
                        int column = currentTile.Index % numberOfTiles;
                        spriteBatch.Draw(TileSet, tilesBounds[j, i], new Rectangle(column * 32, row * 32, 32, 32), Color.White);
                    }
                }
            }
        }

        public void Update() // TO OPTIMIZE /!\
        {

        }

        #endregion
    }
}
