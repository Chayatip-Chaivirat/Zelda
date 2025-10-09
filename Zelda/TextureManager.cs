using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    internal class TextureManager
    {
        //===== Player =====
        Texture2D playerTex;

        //===== Enemy =====
        Texture2D enemyTex;

        public void Textures(ContentManager content)
        {
            playerTex = content.Load<Texture2D>("Link_all");
            enemyTex = content.Load<Texture2D>("skelett");
        }
    }
}
