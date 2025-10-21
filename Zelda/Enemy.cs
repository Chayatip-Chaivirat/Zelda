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
        public int speed;
        public int movementDirection = 1;
        public Rectangle enemyHitbox;

        public Vector2 enemyDestination;
        public Vector2 enemyDirection;
        public Rectangle enemySourceRec;
        public bool movementUp;

        public Enemy(Texture2D enemyTex, Vector2 position, bool movementUp)
        {
            this.enemyTex = enemyTex;
            this.position = position;
            this.movementUp = movementUp;
            enemySourceRec = new Rectangle(0, 0, 40, 40);
            enemyHitbox = new Rectangle((int)position.X, (int)position.Y, 40,40);

            if (!movementUp)
            {
                speed = 2;
            }
            else 
            {
                speed = 1;
            }
        }

        //public void Movement(bool movementUp) // Doesn't work
        //{
        //    position.Y += speed * movementDirection;
        //    position.X += speed * movementDirection;
        //    enemySourceRec.X = (int)position.X;
        //    enemySourceRec.Y = (int)position.Y;

        //    if (movementUp)
        //    {
        //        Vector2 enemyPositionY = new Vector2(position.X, enemySourceRec.Top);
        //        if (enemySourceRec.Top == 0 || enemySourceRec.Bottom == Game1.windowWidthStatic)
        //        {
        //            movementDirection *= -1;
        //        }

        //        if (!Game1.GetTileAtPosition(enemyPositionY)) //When walkable = true
        //        {
        //            movementDirection *= -1;
        //        }

        //        Vector2 enemyPositionYBottom = new Vector2(position.X, enemySourceRec.Bottom);
        //        if (!Game1.GetTileAtPosition(enemyPositionYBottom))
        //        {
        //            movementDirection *= -1;
        //        }
        //    }
        //    else
        //    {
        //        Vector2 enemyPositionX = new Vector2(enemySourceRec.Left, position.Y);
        //        if (enemySourceRec.Right == Game1.windowWidthStatic || enemySourceRec.Left == 0)
        //        {
        //            movementDirection *= -1;
        //        }

        //        if (!Game1.GetTileAtPosition(enemyPositionX)) //When walkable = true
        //        {
        //            movementDirection *= -1;
        //        }

        //        Vector2 enemyPositionXRight = new Vector2(enemySourceRec.Right, position.Y);
        //        if (!Game1.GetTileAtPosition(enemyPositionXRight))
        //        {
        //            movementDirection *= -1;
        //        }
        //    }
           
        //}
        public void UpDownMovement()
        {
            position.Y += speed * movementDirection;
            enemyHitbox.X = (int)position.X;
            enemyHitbox.Y = (int)position.Y;
            enemySourceRec.X = (int)position.X;
            enemySourceRec.Y = (int)position.Y;

            Vector2 enemyPositionY = new Vector2(position.X, enemySourceRec.Top);
            if (enemySourceRec.Top == 0 || enemySourceRec.Bottom == Game1.windowWidthStatic)
            {
                movementDirection *= -1;
            }

            if (!Game1.GetTileAtPosition(enemyPositionY)) //When walkable = true
            {
                movementDirection *= -1;
            }

            Vector2 enemyPositionYBottom = new Vector2(position.X, enemySourceRec.Bottom);
            if (!Game1.GetTileAtPosition(enemyPositionYBottom))
            {
                movementDirection *= -1;
            }
        }

        public void LeftRightMovement()
        {
            position.X += speed * movementDirection;
            enemyHitbox.X = (int)position.X;
            enemyHitbox.Y = (int)position.Y;
            enemySourceRec.X = (int)position.X;
            enemySourceRec.Y = (int)position.Y;

            Vector2 enemyPositionX = new Vector2(enemySourceRec.Left, position.Y);
            if (enemySourceRec.Right == Game1.windowWidthStatic || enemySourceRec.Left == 0)
            {
                movementDirection *= -1;
            }

            if (!Game1.GetTileAtPosition(enemyPositionX)) //When walkable = true
            {
                movementDirection *= -1;
            }

            Vector2 enemyPositionXRight = new Vector2(enemySourceRec.Right, position.Y);
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
