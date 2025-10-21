using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Zelda
{
    public class Player
    {
        public Texture2D playerTex;
        public Vector2 position;
        public Rectangle playerSourceRec;
        public Rectangle playerHitbox;
        public int lives = 10;
        public Vector2 playerDirection;
        public float speed;
        public bool playerMoving;
        public Vector2 playerDestination;

        public bool keyRetrieved = false; 
        public bool attacking = false;
        public Player(Texture2D playerTex, Vector2 position)
        {
            this.playerTex = playerTex;
            this.position = position;
            playerSourceRec = new Rectangle(0, 0, 39, 41);
            playerHitbox = new Rectangle((int)position.X, (int)position.Y, 39, 41);
            playerDirection = new Vector2(0, 0);
            speed = 200;
            playerMoving = false;
            playerDestination = Vector2.Zero;
        }
        public void Update(GameTime gametime)
        {
            KeyPlayerReader.Update();
            attacking = false;

            if (!playerMoving)
            {
                if (KeyPlayerReader.KeyPressed(Keys.A) || KeyPlayerReader.KeyPressed(Keys.Left))
                {
                    playerSourceRec = new Rectangle(0, 0, 39, 41);
                    ChangeDirection(new Vector2(-1, 0));
                }
                else if (KeyPlayerReader.KeyPressed(Keys.D) || KeyPlayerReader.KeyPressed(Keys.Right))
                {
                    playerSourceRec = new Rectangle(0,0, 39, 41);
                    ChangeDirection(new Vector2(1,0));
                }
                else if (KeyPlayerReader.KeyPressed(Keys.W) || KeyPlayerReader.KeyPressed(Keys.Up))
                {
                    playerSourceRec = new Rectangle(0 ,80, 39, 41);
                    ChangeDirection(new Vector2(0, -1));
                }
                else if (KeyPlayerReader.KeyPressed(Keys.S) || KeyPlayerReader.KeyPressed(Keys.Down))
                {
                    playerSourceRec = new Rectangle(0,40, 39, 41);
                    ChangeDirection(new Vector2(0,1));
                }
                else if (KeyPlayerReader.KeyPressed(Keys.Space) || KeyPlayerReader.LeftClick())
                {
                    if (!attacking)
                    {
                        PlayerAttack();
                    }
                }
            }
            else
            {
                position += playerDirection * speed * (float)gametime.ElapsedGameTime.TotalSeconds;
                playerHitbox.X = (int)position.X;
                playerHitbox.Y = (int)position.Y;

                if (Vector2.Distance(position, playerDestination) < 1)
                {
                    position = playerDestination;
                    playerMoving = false;
                }
            }
        }

        public void ChangeDirection(Vector2 direction)
        {
            playerDirection = direction;
            Vector2 newPlayerDestination = position + playerDirection * Game1.tileSize;

            if (Game1.GetTileAtPosition(newPlayerDestination)) // If the tile is walkable
            {
                playerDestination = newPlayerDestination;
                playerMoving = true;
            }
        }
        public bool PlayerAttack()
        {
            playerSourceRec = new Rectangle(0,118,39,41); //start the attack animation from here
            return attacking = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTex, position, playerSourceRec, Color.White);
        }
    }
}
