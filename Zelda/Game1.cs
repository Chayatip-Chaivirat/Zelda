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
        public static int windowHeightStatic;
        public static int windowWidthStatic;

        //======== Enemy ========
        Enemy enemy;
        Vector2 enemyPos;
        List<Enemy> enemyList;

        //======== Player ========
        Player player;
        Vector2 playerPos;

        //======== Tile ========
        public static Tile[,] tileArray;
        public static int tileSize = 40;

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

        //======== Zelda the Princess ========
        Vector2 zeldaPos;

        //======== Key to the Door ========
        Vector2 keyPos;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            int windowHeight = 1120;
            _graphics.PreferredBackBufferHeight = windowHeight;
            windowHeightStatic = windowHeight;

            int windowWidth = 1200;
            _graphics.PreferredBackBufferWidth = windowWidth;
            windowWidthStatic = windowWidth;
            _graphics.ApplyChanges();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.Textures(Content);
            enemyList = new List<Enemy>();

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
                    else if (map[i][j] == 'P') // Player
                    {
                        playerPos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j, i] = new Tile(TextureManager.grassTex, playerPos, true);
                        player = new Player(TextureManager.playerTex, playerPos);
                    }
                    else if (map[i][j] == 'Z') // Zelda the Princess
                    {
                        zeldaPos = new Vector2(j * tileSize, i * tileSize);
                        //tileArray[j, i] = new Tile(TextureManager.stoneFloorTex, zeldaPos, true);
                        tileArray[j, i] = new Tile(TextureManager.zeldaTex, zeldaPos, true);
                    }
                    else if (map[i][j] == 'E') // Enemy 
                    { 
                        enemyPos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j, i] = new Tile(TextureManager.grassTex, enemyPos, true);
                        enemy = new Enemy(TextureManager.enemyTex, enemyPos);
                        enemyList.Add(enemy);
                    }
                    else if (map[i][j] == 'K') // Key to the Door
                    {
                        keyPos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j, i] = new Tile(TextureManager.keyTex, keyPos, true);
                    }
                }
            }
        }

        public static bool GetTileAtPosition(Vector2 position)
        {
            int tileX = (int)position.X / tileSize;
            int tileY = (int)position.Y / tileSize;

            // Prevent out of bounds
            if (tileX < 0 || tileY < 0 || tileX >= tileArray.GetLength(0) || tileY >= tileArray.GetLength(0))
                return false;

            return tileArray[tileX, tileY].isWalkable;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update(gameTime);
            foreach (Enemy ene in enemyList)
            {
                ene.LeftRightMovement();
            }

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

            foreach (Enemy ene in enemyList)
            {
                ene.Draw(_spriteBatch);
            }
            player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
