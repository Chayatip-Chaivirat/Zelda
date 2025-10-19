using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class Enemy
    {
        public Texture2D enemyTex;
        public Vector2 position;
        public int speed;
        public Vector2 enemyDestination;
        public Vector2 enemyDirection;

        public Enemy(Texture2D enemyTex, Vector2 position)
        {
            this.enemyTex = enemyTex;
            this.position = position;

        }
        public void UpDownMovement(Vector2 direction)
        {
            enemyDestination = direction;
            Vector2 newEnemyDestination = position + enemyDirection * Game1.tileSize;
            if (Game1.GetTileAtPosition(newEnemyDestination))
            {
                position.Y += speed;
            }
        }

        public void LeftRightMovement(int speed)
        {
            position.X += speed;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTex, position, Color.White);
        }
    }
}
