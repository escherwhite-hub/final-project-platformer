using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

       
        Rectangle window, player;
        Texture2D caveBlockTexture, firsLvlBgTexture, background1Texture, background2Texture, background3Texture ;
        MouseState mouseState;
        KeyboardState keyboardState;
        List<Rectangle> caveBlocks;
        Texture2D RockmanIdleTexture;
        Vector2 rockmanSpeed;
        Vector2 playerPosition;
        float gravity = 0.2f; // This is how fast player accelerated downwards
        float jumpSpeed = 13f; // This will determine the strength of the jump
        bool onGround = false;



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
            caveBlocks.Add(new Rectangle(160, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(420, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(500, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(580, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(660, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(740, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(820, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(900, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(980, window.Height - 80, 80, 80));
            caveBlocks.Add(new Rectangle(660,540, 80, 80));
            

            rockmanSpeed = Vector2.Zero;
            playerPosition = new Vector2(10, 10);
            player = new Rectangle(10, 10, 60, 80);

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
            RockmanIdleTexture = Content.Load<Texture2D>("rockmanidle");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardState = Keyboard.GetState();
            this.Window.Title = mouseState.Position.ToString();
            mouseState = Mouse.GetState();

            rockmanSpeed.X = 0f;
            if (keyboardState.IsKeyDown(Keys.A))
                rockmanSpeed.X += -4f;
            if (keyboardState.IsKeyDown(Keys.D))
                rockmanSpeed.X += 4f;
            
            playerPosition.X += rockmanSpeed.X;
            player.Location = playerPosition.ToPoint();
            foreach (Rectangle platform in caveBlocks)
                if (player.Intersects(platform))
                {
                    playerPosition.X += -rockmanSpeed.X;
                    player.Location = playerPosition.ToPoint();
                }

            foreach (Rectangle platform in caveBlocks)
                if (player.Intersects(platform))
                {
                    if (rockmanSpeed.Y > 0f) // player lands on platform
                    {
                        onGround = true;
                        rockmanSpeed.Y = 0;
                        playerPosition.Y = platform.Top - player.Height;
                    }
                    else // hits bottom of platform
                    {
                        rockmanSpeed.Y = 0;
                        playerPosition.Y = platform.Bottom;
                    }
                    player.Location = playerPosition.ToPoint();
                }

            if (!onGround)
            {
                rockmanSpeed.Y += gravity;
            }
            else
            {
                rockmanSpeed.Y += gravity;
            }

                playerPosition.Y += rockmanSpeed.Y;
            player.Location = playerPosition.ToPoint();

            if (!onGround)
            {
                rockmanSpeed.Y += gravity;
            }
           
            else if (keyboardState.IsKeyDown(Keys.Space) && onGround)
            {
                rockmanSpeed.Y = -jumpSpeed;
                onGround = false;
            }
          if (player.Top > window.Height)
            {
                Exit();
            }



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
            foreach (Rectangle platform in caveBlocks)
                _spriteBatch.Draw(caveBlockTexture, platform, Color.White);
            _spriteBatch.Draw(RockmanIdleTexture, player, Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
