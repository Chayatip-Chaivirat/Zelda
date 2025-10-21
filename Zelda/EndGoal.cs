using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda
{
    internal class EndGoal
    {
        Texture2D goalTex;
        public Vector2 goalPos;
        public Rectangle goalHitBox;
        public EndGoal(Vector2 goalPos)
        {
            goalTex = TextureManager.zeldaTex;
            this.goalPos = goalPos;
            goalHitBox = new Rectangle((int)goalPos.X, (int)goalPos.Y, Game1.tileSize, Game1.tileSize);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.zeldaTex, goalPos, Color.White);
        }
    }
}
