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
    public class Key
    {
        //Class for items such as key, power-ups
        public Texture2D keyTex;
        public  Vector2 keyPos;
        public Rectangle keyHitbox;
        public Key(Vector2 keyPos)
        {
            Texture2D keyTex = TextureManager.keyTex;
            this.keyPos = keyPos;
            keyHitbox = new Rectangle((int)keyPos.X, (int)keyPos.Y, Game1.tileSize, Game1.tileSize);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.keyTex, keyPos, Color.White);
        }
    }
}
