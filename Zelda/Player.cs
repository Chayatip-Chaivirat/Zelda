using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using System.Reflection.Metadata.Ecma335;
namespace Zelda
{
    internal class Player
    {
        public Texture2D playerTex;
        public Vector2 position;
        public Rectangle playerRec = new Rectangle(0, 0, 39, 41);
        int lives = 10;
        public bool keyRetrieved = false; // Maybe in another class???
        public bool attacking = false;
        public int speed = 2;
        public Player(Texture2D playerTex, Vector2 position)
        {
            this.playerTex = playerTex;
            this.position = position;
        }
        public void playerMovement()
        {
            KeyboardState state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                position.X -= speed;
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                position.X += speed;
            }
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                position.Y -= speed;
            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                position.Y += speed;
            }
            if (!attacking && state.IsKeyDown(Keys.Space) || Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //PlayerAttack();
                //PlayerDefault();
                //float attackCoolDown = 1.0f;
                //float attackTimer = 0.0f;
                //if (attackTimer < attackCoolDown)
                //{
                //    PlayerAttack();
                //}
                //else
                //{
                //    PlayerDefault();
                //}
                attacking = true;
                if (attacking)
                {
                    PlayerAttack();
                    attacking = false;
                }
            }
        }
        public void PlayerAttack()
        {
            playerRec = new Rectangle(0,118,39,41); //start the attack animation from here

        }

        public void PlayerDefault()
        {
            playerRec = new Rectangle(0, 0, 39, 41); // player default animation
        }

        public void Update()
        {
            playerMovement();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTex, position, playerRec, Color.White);
        }
    }
}
