using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Zelda
{
    internal class Tile
    {
        public bool isWalkable;
        Texture2D tileTex;
        Vector2 position;

        public Tile(Texture2D tileTex, Vector2 position, bool isWalkable)
        {
            this.tileTex = tileTex;
            this.position = position;
            this.isWalkable = isWalkable;
        }
    }
}
