using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;
using System.Runtime.Intrinsics;

namespace Zelda
{
    public class Game1 : Game   
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        string mapText;
        List<string> result;
        List<int> scoreList;
        public static int windowHeightStatic;
        public static int windowWidthStatic;

        public static int score = 0;

        //======== Enemy ========
        Enemy enemy;
        Vector2 enemyPos;
        public static List<Enemy> enemyList;

        //======== Player ========
        Player player;
        Vector2 playerPos;

        //======== Tile ========
        public static Tile[,] tileArray;
        public static int tileSize = 40;
        Vector2 wallTilePos;
        Vector2 stoneFloorTilePos;
        Vector2 waterTilePos;
        Vector2 soilTilePos;
        Vector2 grassTilePos;
        Vector2 bushTilePos;
        Vector2 bushSTilePos;
        Vector2 bridgeTilePos;
        Vector2 doorTilePos;

        //======== EndGoal ========
        Vector2 zeldaPos;
        EndGoal zeldaThePrincess;

        //======== Key to the Door ========
        Vector2 keyPos;
        Key key;

        //======== Gamestates ========
        static GameState gameState;

        //======== Gamestates: starting ========
        SpriteFont startSpriteFont;

        //======== Gamestates: GameOver ========
        public bool fileIsWritten = false;
        SpriteFont scoreSpriteFont;
        enum GameState
        {
            Starting,
            Playing,
            GameOver
        }
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

            //GameState gameState = GameState.Starting;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.Textures(Content);
            enemyList = new List<Enemy>();
            startSpriteFont = Content.Load<SpriteFont>("Start");
            scoreSpriteFont = Content.Load<SpriteFont>("ScoreFont");

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

        public void WriteScoreToFile(string fileName)
        {
            //StreamWriter sw = new StreamWriter(fileName);
            //List<int> scoreList = new List<int>();
            ReadFromFile(fileName);
            FileStream leaderboard = null;
                try
                {
                    leaderboard = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                    using (StreamWriter sw = new StreamWriter(leaderboard))
                    {
                        sw.WriteLine("\n"+score);
                        fileIsWritten = true;
                    }
                }
                catch
                {
                    Console.Write("Can't write to ScoreLeaderboard file.");
                }
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
                        tileArray[j, i] = new Tile(TextureManager.stoneFloorTex, zeldaPos, true);
                        zeldaThePrincess = new EndGoal(zeldaPos);
                    }
                    else if (map[i][j] == 'E') // Enemy: Left Right movements
                    { 
                        enemyPos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j, i] = new Tile(TextureManager.grassTex, enemyPos, true);
                        enemy = new Enemy(TextureManager.enemyTex, enemyPos, false);
                        enemyList.Add(enemy);
                    }
                    else if (map[i][j] == 'K') // Key to the Door
                    {
                        keyPos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j, i] = new Tile(TextureManager.grassTex, keyPos, true);
                        key = new Key(keyPos);
                    }
                    else if (map[i][j] == 'e') // Enemy: Up Down movements
                    {
                        enemyPos = new Vector2(j * tileSize, i * tileSize);
                        tileArray[j, i] = new Tile(TextureManager.grassTex, enemyPos, true);
                        enemy = new Enemy(TextureManager.enemyTex, enemyPos, true);
                        enemyList.Add(enemy);
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
            KeyPlayerReader.Update();

            //switch (gameState)
            //{
            //    case GameState.Starting:
            //        if (KeyPlayerReader.KeyPressed(Keys.Enter))
            //        {

            //        }
            //        break;

            //    case GameState.Playing:
            //        player.Update(gameTime);
            //        CollisionManager.PlayerEnemyCollision(player);
            //        CollisionManager.PlayerKey(player, key);
            //        foreach (Enemy ene in enemyList)
            //        {
            //            if(ene.movementUp)
            //            {
            //                ene.UpDownMovement();
            //            }
            //            else
            //            {
            //                ene.LeftRightMovement();
            //            }
            //        }

            //        if(player.keyRetrieved) //if player retrived the key => open the door
            //        {
            //            for (int x = 0; x < tileArray.GetLength(0); x++)
            //            {
            //                for(int y = 0; y < tileArray.GetLength(1); y++)
            //                {
            //                    if (tileArray[x,y] != null && tileArray[x,y].tileTex == TextureManager.doorTex)
            //                    {
            //                        tileArray[x, y] = new Tile(TextureManager.openDoorTex, doorTilePos, true);
            //                    }
            //                }
            //            }
            //        }
 
            //        break;
            //    case GameState.GameOver:
            //        WriteScoreToFile(@"ScoreLeaderboard.txt");
            //        break;
            //}

            if (gameState == GameState.Starting)
            {
                if(KeyPlayerReader.KeyPressed(Keys.Enter))
                {
                    gameState = GameState.Playing;
                }
            }

            if(gameState == GameState.Playing)
            {
                player.Update(gameTime);
                CollisionManager.PlayerEnemyCollision(player);
                CollisionManager.PlayerKey(player, key);
                CollisionManager.PlayerEndGoal(player, zeldaThePrincess);
                foreach (Enemy ene in enemyList)
                {
                    if (ene.movementUp)
                    {
                        ene.UpDownMovement();
                    }
                    else
                    {
                        ene.LeftRightMovement();
                    }
                }

                if (player.keyRetrieved) //if player retrived the key => open the door
                {
                    for (int x = 0; x < tileArray.GetLength(0); x++)
                    {
                        for (int y = 0; y < tileArray.GetLength(1); y++)
                        {
                            if (tileArray[x, y] != null && tileArray[x, y].tileTex == TextureManager.doorTex)
                            {
                                tileArray[x, y] = new Tile(TextureManager.openDoorTex, doorTilePos, true);
                            }
                        }
                    }
                }
                if (zeldaThePrincess.acheivedEndGoal)
                {
                    gameState = GameState.GameOver;
                }
            }
            
            if(gameState == GameState.GameOver)
            {
                if(!fileIsWritten)
                {
                    WriteScoreToFile(@"ScoreLeaderboard.txt");
                }

                //Write out the score
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if(gameState == GameState.Starting)
            {
                Vector2 startSpriteFontPos = new Vector2((int) windowWidthStatic /2, (int) windowHeightStatic / 2);
                _spriteBatch.DrawString(startSpriteFont, "Press Enter to Start", startSpriteFontPos, Color.Black);
            }

            if(gameState == GameState.Playing)
            {
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
                key.Draw(_spriteBatch);
                zeldaThePrincess.Draw(_spriteBatch);

                Vector2 scorePos = new Vector2((int) windowWidthStatic/2, 1040);
                _spriteBatch.DrawString(scoreSpriteFont, "Score: " + score, scorePos, Color.Black);
            }
            if(gameState == GameState.GameOver)
            {
                Vector2 scoreFontPos = new Vector2((int) windowWidthStatic/2, (int) windowHeightStatic / 2);
                _spriteBatch.DrawString(scoreSpriteFont, "Your score: " + score, scoreFontPos, Color.Black);
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}