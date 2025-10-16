using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using System.Reflection.Metadata.Ecma335;
namespace Zelda
{
    public class Player
    {
        public Texture2D playerTex;
        public Vector2 position;
        public Rectangle playerRec;
        public int lives = 10;
        public Vector2 playerDirection;
        public float speed;
        public bool playerMoving;
        public Vector2 playerDestination;

        public bool keyRetrieved = false; // Maybe in another class???
        public bool attacking = false;
        public Player(Texture2D playerTex, Vector2 position)
        {
            this.playerTex = playerTex;
            this.position = position;
            playerRec = new Rectangle(0, 0, 39, 41);
            playerDirection = new Vector2(0, 0);
            speed = 200;
            playerMoving = false;
            playerDestination = Vector2.Zero;
        }
        public void Update(GameTime gametime)
        {
            KeyboardState state = Keyboard.GetState();
            attacking = false;

            if (!playerMoving)
            {
                if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
                {
                    playerRec = new Rectangle(0, 0, 39, 41);
                    ChangeDirection(new Vector2(-1, 0));
                }
                else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
                {
                    playerRec = new Rectangle(0,0, 39, 41);
                    ChangeDirection(new Vector2(1,0));
                }
                else if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
                {
                    playerRec = new Rectangle(0 ,80, 39, 41);
                    ChangeDirection(new Vector2(0, -1));
                }
                else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
                {
                    playerRec = new Rectangle(0,40, 39, 41);
                    ChangeDirection(new Vector2(0,1));
                }
                else if (!attacking && state.IsKeyDown(Keys.Space) || Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (!attacking)
                    {
                        PlayerAttack();
                    }
                }
            }
        }

        public void ChangeDirection(Vector2 direction)
        {
            playerDirection = direction;
            Vector2 newPlayerDestination = position + playerDirection * Game1.tileSize;
            if (Game1.GetTileAtPosition(newPlayerDestination))
            {
                playerDestination = newPlayerDestination;
                playerMoving = true;
            }
        }
        public bool PlayerAttack()
        {
            playerRec = new Rectangle(0,118,39,41); //start the attack animation from here
            return attacking = true;
        }

        public void PlayerDefault()
        {
                playerRec = new Rectangle((int)position.X, (int)position.Y, 39, 41); // player default animation
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTex, position, playerRec, Color.White);
        }
    }
}
