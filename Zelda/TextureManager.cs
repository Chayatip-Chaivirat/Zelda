using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    internal class TextureManager
    {
        //===== Player =====
        public static Texture2D playerTex;

         //===== Enemy =====
        public static Texture2D enemyTex;

        //===== Key =====
        public static Texture2D keyTex;

        //===== Tile =====
        public static Texture2D wallTex;
        public static Texture2D stoneFloorTex;
        public static Texture2D waterTex;
        public static Texture2D soilTex;
        public static Texture2D grassTex;
        public static Texture2D bushTex;
        public static Texture2D bushSTex;
        public static Texture2D bridgeTex;

        //===== Door =====
        public static Texture2D doorTex;
        public static Texture2D openDoorTex;

        public static void Textures(ContentManager content)
        {
            playerTex = content.Load<Texture2D>("Link_all");

            enemyTex = content.Load<Texture2D>("skelett");

            keyTex = content.Load<Texture2D>("key");

            wallTex = content.Load<Texture2D>("wall");
            stoneFloorTex = content.Load<Texture2D>("stonefloor");
            waterTex = content.Load<Texture2D>("water");
            soilTex = content.Load<Texture2D>("soil");
            grassTex = content.Load<Texture2D>("grass");
            bushTex = content.Load<Texture2D>("bush");
            bushSTex = content.Load<Texture2D>("bushS");
            bridgeTex = content.Load<Texture2D>("bridge");

            doorTex = content.Load<Texture2D>("door");
            openDoorTex = content.Load<Texture2D>("opendoor");
        }
    }
}
