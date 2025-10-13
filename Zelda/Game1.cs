using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Zelda
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        string mapText;

        //======== Enemy ========
        Enemy enemy;
        Vector2 enemyPos;
        Rectangle enemyRec;

        //======== Enemy ========
        Player player;
        Vector2 playerPos;
        Rectangle playerRec;

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

            //StreamReader sr = new StreamReader(@"gameMap.txt");
            //mapText = sr.ReadLine();
            //sr.Close();

            //======== Enemy ========
            enemyPos = new Vector2 (0,0);
            enemyRec = new Rectangle(0,0,39,39);
            enemy = new Enemy(TextureManager.enemyTex, enemyPos, enemyRec);

            //======== Player ========
            playerPos = new Vector2(0,0);
            player = new Player(TextureManager.playerTex, playerPos );
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
