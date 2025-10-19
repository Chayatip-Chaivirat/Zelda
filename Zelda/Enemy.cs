using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class Enemy
    {
        public Texture2D enemyTex;
        public Vector2 position;
        public int speed = 1;
        public int movementDirection = 1;
        public Vector2 enemyDestination;
        public Vector2 enemyDirection;

        public Enemy(Texture2D enemyTex, Vector2 position)
        {
            this.enemyTex = enemyTex;
            this.position = position;

        }
        public void UpDownMovement()
        {
            position.Y += speed * movementDirection;

            Vector2 enemyPositionY = new Vector2(position.X, position.Y + (movementDirection * Game1.tileSize / 3));
            if (!Game1.GetTileAtPosition(enemyPositionY))
            {
                movementDirection *= -1;
            }
        }

        public void LeftRightMovement()
        {
            position.X += speed * movementDirection;

            Vector2 enemyPositionX = new Vector2(position.X + (movementDirection * Game1.tileSize / 2), position.Y);
            if (!Game1.GetTileAtPosition(enemyPositionX))
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
