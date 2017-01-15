using m_test1_hugo.Class.Main;
using m_test1_hugo.Class.Main.interfaces;
using m_test1_hugo.Class.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Characters.Teams
{
    class Bot : Character
    {
    
        #region attributs

        #endregion

        #region constructeur
        public Bot(Character classe, Weapon weapon, Team team)
        {
            this.weapon = weapon;
            weapon.Holder = this;
            MoveSpeed = classe.MoveSpeed - weapon.MovingMalus;
            this.Health = classe.Health;
            CharacterList.Add(this);
            this.team = team;
            this.MaxHealth = Health;
        }
        #endregion

        #region dessin
        public override void LoadContent(ContentManager content)
        {
            if (team._teamNumber == 1)
            {
                LoadContent(content, "playerSP2", 4, 3);
            }
            else if (team._teamNumber == 2)
            {
                LoadContent(content, "moche", 4, 3);
            }

            weapon?.LoadContent(content);
        }

        #endregion

        #region deplacement + MouseRotation
        public void Control(GameTime gametime, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer)
        {
            Update(gametime);

            MouseRotationAngle = 0.2f;

            isMoving = false; // pas en mouvement

            #region mouvement du personnage 

            /*if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
            {
                moveLeft(tileSize, mapWidth, mapHeight, collisionLayer);
                currentRow = 1;
            }

            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                moveDown(tileSize, mapWidth, mapHeight, collisionLayer);
                currentRow = 0;
            }

            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z))
            {
                moveUp(tileSize, mapWidth, mapHeight, collisionLayer);
                currentRow = 3;
            }

            if (state.IsKeyDown(Keys.F1) || state.IsKeyDown(Keys.D))
            {
                moveRight(tileSize, mapWidth, mapHeight, collisionLayer);
                currentRow = 2;
            }*/
            #endregion
        }
        #endregion

        #region methodes liees a l'arme

        #region rechargement de l'arme

        #region methodes 

        



        #endregion

        #endregion

        #region methodes liees au tir

        

        #endregion

        #endregion

        public void Update(GameTime gametime)
        {
            this.UpdateSprite(gametime); // update du sprite animé

            MouseRotationAngle = 0.005f;

            if (IsDead())
                CharacterList.Remove(this);
        }
    }
}
