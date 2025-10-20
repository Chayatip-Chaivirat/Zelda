using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda
{
    public class Enemy
    {
        public Texture2D enemyTex;
        public Vector2 position;
        public int speedLeftRight = 2;
        public int movementDirection = 1;
        public int speedUpDown = 1;

        public Vector2 enemyDestination;
        public Vector2 enemyDirection;
        public Rectangle enemyRec;

        public Enemy(Texture2D enemyTex, Vector2 position)
        {
            this.enemyTex = enemyTex;
            this.position = position;
            enemyRec = new Rectangle(0, 0, 40, 40);

        }
        public void UpDownMovement()
        {
            position.Y += speedUpDown * movementDirection;
            enemyRec.X = (int)position.X;
            enemyRec.Y = (int)position.Y;

            Vector2 enemyPositionY = new Vector2(position.X, enemyRec.Top);
            if (enemyRec.Top == 0 || enemyRec.Bottom == Game1.windowWidthStatic)
            {
                movementDirection *= -1;
            }

            if (!Game1.GetTileAtPosition(enemyPositionY)) //When walkable = true
            {
                movementDirection *= -1;
            }

            Vector2 enemyPositionYBottom = new Vector2(position.X, enemyRec.Bottom);
            if (!Game1.GetTileAtPosition(enemyPositionYBottom))
            {
                movementDirection *= -1;
            }
        }

        public void LeftRightMovement()
        {
            position.X += speedLeftRight * movementDirection;
            enemyRec.X = (int) position.X;
            enemyRec.Y = (int)position.Y;

            Vector2 enemyPositionX = new Vector2(enemyRec.Left, position.Y);
            if(enemyRec.Right == Game1.windowWidthStatic || enemyRec.Left == 0)
            {
                movementDirection *= -1;
            }

            if (!Game1.GetTileAtPosition(enemyPositionX)) //When walkable = true
            {
                movementDirection *= -1;
            }

                Vector2 enemyPositionXRight = new Vector2(enemyRec.Right, position.Y);
            if (!Game1.GetTileAtPosition(enemyPositionXRight))
            {
                movementDirection *= -1;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTex, position, Color.White);
        }
    }
}
