using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class Tile
    {
        public bool isWalkable;
        public Texture2D tileTex;
        public Vector2 position;
        public Tile(Texture2D tileTex, Vector2 position, bool isWalkable)
        {
            this.tileTex = tileTex;
            this.position = position;
            this.isWalkable = isWalkable;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileTex,position, Color.White);
        }
    }
}
