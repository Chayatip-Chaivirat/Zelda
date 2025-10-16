using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Zelda
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        string mapText;
        List<string> result;

        //======== Enemy ========
        Enemy enemy;
        Vector2 enemyPos;
        Rectangle enemyRec;

        //======== Player ========
        Player player;
        Vector2 playerPos;

        //======== Tile ========
        public static Tile[,] tileArray;
        public static int tileSize = 25;

        Tile wallTile;
        Vector2 wallTilePos;

        Tile stoneFloorTile;
        Vector2 stoneFloorTilePos;

        Tile waterTile;
        Vector2 waterTilePos;

        Tile soilTile;
        Vector2 soilTilePos;

        Tile grassTile;
        Vector2 grassTilePos;

        Tile bushTile;
        Vector2 bushTilePos;

        Tile bushSTile;
        Vector2 bushSTilePos;

        Tile bridgeTile;
        Vector2 bridgeTilePos;

        Tile doorTile;
        Vector2 doorTilePos;

        Tile openDoorTile;
        Vector2 openDoorTilePos;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.Textures(Content);

            //======== Enemy ========
            enemyPos = new Vector2 (0,0);
            enemyRec = new Rectangle(0,0,39,39);
            enemy = new Enemy(TextureManager.enemyTex, enemyPos, enemyRec);

            //======== Player ========
            playerPos = new Vector2(200,250);
            player = new Player(TextureManager.playerTex, playerPos );

            //======== Tile ========
            CreateMap(@"gameMap.txt");
        }
        public List<string> ReadFromFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            List<string> result = new List<string>();

            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                result.Add(line);
            }
            sr.Close();
            return result;
        }

        public void CreateMap(string fileName)
        {
            List<string> map = ReadFromFile(fileName);
            tileArray = new Tile[map[0].Length, map.Count];

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    if (map[i][j] == '*') // Wall
                    {
                        wallTilePos = new Vector2(j*tileSize, i*tileSize);
                        tileArray[j,i] = new Tile(TextureManager.wallTex, wallTilePos, false);
                    }
                    else if (map[i][j] == '-') // Water
                    {
                        waterTilePos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.waterTex, waterTilePos, false);
                    }

                    else if (map[i][j] == 'G') // Grass
                    {
                        grassTilePos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.grassTex, grassTilePos, true);
                    }
                    else if (map[i][j] == 'S') // Soil
                    {
                        soilTilePos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.soilTex, soilTilePos, true);
                    }
                    else if (map[i][j] == 'B') // Bridge
                    {
                        bridgeTilePos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.bridgeTex, bridgeTilePos, true);
                    }
                    else if (map[i][j] == '#') // Stone Floor
                    {
                        stoneFloorTilePos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.stoneFloorTex,stoneFloorTilePos, true);
                    }
                    else if (map[i][j] == '|') // Door
                    {
                        doorTilePos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.doorTex, doorTilePos, false);
                    }
                    else if (map[i][j] == '/') // Open Door
                    {
                        openDoorTilePos= new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.openDoorTex, openDoorTilePos, true);
                    }
                    else if (map[i][j] == '+') // Bush
                    {
                        bushTilePos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.bushTex, bushTilePos, false);
                    }
                    else if (map[i][j] == '?') // BushS
                    {
                        bushSTilePos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j,i] = new Tile(TextureManager.bushSTex, bushSTilePos, false);
                    }
                }
            }
        }

        public static bool GetTileAtPosition(Vector2 position)
        {
            return tileArray[(int)position.X / tileSize, (int)position.Y / tileSize].isWalkable=false;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (Tile tile in tileArray)
            {
                if (tile != null)
                {
                    tile.Draw(_spriteBatch);
                }
            }
            player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
