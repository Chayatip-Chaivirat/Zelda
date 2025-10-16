using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class Enemy
    {
        public Texture2D enemyTex;
        public Vector2 position;
        public Rectangle enemyRec;
        public int speed;

        public Enemy(Texture2D enemyTex, Vector2 position, Rectangle enemyRec)
        {
            this.enemyTex = enemyTex;
            this.position = position;
            this.enemyRec = enemyRec;
        }

        public void Update()
        {

        }

        public void UpDownMovement(int speed)
        {
            position.Y += speed;
        }

        public void LeftRightMovement(int speed)
        {
            position.X += speed;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTex, position, enemyRec, Color.White);
        }
    }
}
