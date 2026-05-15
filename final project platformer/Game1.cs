using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace final_project_platformer
{
    enum Screen
    {
        startScreen,
        firstLevel,
        secondLevel,
        deathScreen
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

       
        Rectangle window;
        Texture2D caveBlockTexture, firsLvlBgTexture, background1Texture, background2Texture, background3Texture ;
        MouseState mouseState;
        List<Rectangle> caveBlocks;




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, 1000, 800);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();

            caveBlocks = new List<Rectangle>();
            caveBlocks.Add(new Rectangle(0, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(80, window.Height - 80, 80, 80));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            caveBlockTexture = Content.Load<Texture2D>("caveBlock");
            background1Texture = Content.Load<Texture2D>("background1");
            background2Texture = Content.Load<Texture2D>("background2");
            background3Texture = Content.Load<Texture2D>("background3");
            firsLvlBgTexture = Content.Load<Texture2D>("background4b");
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            this.Window.Title = mouseState.Position.ToString();
            mouseState = Mouse.GetState();


            foreach (Rectangle barrier in caveBlocks)
                if (pacRect.Intersects(barrier))
                    pacRect.Offset(-pacSpeed);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(background1Texture, window, Color.White);
            _spriteBatch.Draw(background2Texture, window, Color.White);
            _spriteBatch.Draw(background3Texture, window, Color.White);
            _spriteBatch.Draw(firsLvlBgTexture, window, Color.White);
            foreach (Rectangle barrier in caveBlocks)
                _spriteBatch.Draw(caveBlockTexture, barrier, Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
