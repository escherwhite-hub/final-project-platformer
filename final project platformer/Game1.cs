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
        secondLevel
        
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

       
        Rectangle window, player, spikeRect, keyRect, portalRect;
        Texture2D caveBlockTexture, firsLvlBgTexture, secondLvlBgTexture, background1Texture, background2Texture, background3Texture, spikeTexture, keyTexture,portalTexture ;
        MouseState mouseState;
        KeyboardState keyboardState;
        List<Rectangle> caveBlocks;
        Texture2D RockmanIdleTexture;
        Vector2 rockmanSpeed, keySpeed;
        Vector2 playerPosition;
        float gravity = 0.2f; // This is how fast player accelerated downwards
        float jumpSpeed = 13f; // This will determine the strength of the jump
        bool onGround = false;
        Screen screen;
        SpriteFont title;



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
            caveBlocks.Add(new Rectangle(660, 540, 80, 80));
            caveBlocks.Add(new Rectangle(740, 540, 80, 80));
            caveBlocks.Add(new Rectangle(820, 540, 80, 80));
            caveBlocks.Add(new Rectangle(900, 540, 80, 80));
            caveBlocks.Add(new Rectangle(980, 540, 80, 80));
            caveBlocks.Add(new Rectangle(210, 330, 80, 80));
            caveBlocks.Add(new Rectangle(290, 330, 80, 80));
            caveBlocks.Add(new Rectangle(370, 330, 80, 80));
            caveBlocks.Add(new Rectangle(450, 330, 80, 80));
            caveBlocks.Add(new Rectangle(-40, 170, 80, 80));
            caveBlocks.Add(new Rectangle(40, 170, 80, 80));

            spikeRect = new Rectangle(390, 280, 20, 60);
            keyRect = new Rectangle(20, 105, 30, 60);
            portalRect = new Rectangle(900, 10, 70, 90);

            rockmanSpeed = Vector2.Zero;
            playerPosition = new Vector2(10, 700);
            player = new Rectangle(10, 700, 60, 80);

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
            secondLvlBgTexture = Content.Load<Texture2D>("background4a");
            RockmanIdleTexture = Content.Load<Texture2D>("rockmanidle");
            spikeTexture = Content.Load<Texture2D>("spike");
            keyTexture = Content.Load<Texture2D>("key");
            portalTexture = Content.Load<Texture2D>("portal");
            title = Content.Load<SpriteFont>("title");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardState = Keyboard.GetState();
            this.Window.Title = mouseState.Position.ToString();
            mouseState = Mouse.GetState();

            if (screen == Screen.startScreen)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.firstLevel;
            }
            else if (screen == Screen.firstLevel)
            {
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

                if (!onGround)
                {
                    rockmanSpeed.Y += gravity;
                }
                if (!onGround)
                {
                    rockmanSpeed.Y += gravity;
                    if (rockmanSpeed.Y < 0 && keyboardState.IsKeyUp(Keys.Space))
                        rockmanSpeed.Y /= 1.5f;

                }

                else if (keyboardState.IsKeyDown(Keys.Space) && onGround)
                {
                    rockmanSpeed.Y = -jumpSpeed;
                    onGround = false;
                }
                else
                {
                    rockmanSpeed.Y += gravity;
                }

                playerPosition.Y += rockmanSpeed.Y;
                player.Location = playerPosition.ToPoint();

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

                if (player.Top <= 0)
                {
                    playerPosition.Y += -rockmanSpeed.Y;
                }
                if (player.Left <= 0 || player.Right >= 1000)
                {
                    playerPosition.X += -rockmanSpeed.X;
                }

                if (player.Intersects(spikeRect) || player.Top > 1000)
                {
                    playerPosition.Y = 700;
                    playerPosition.X = 10;
                }

                if (player.Intersects(keyRect))
                {
                    caveBlocks.Add(new Rectangle(330, 125, 80, 80));
                    caveBlocks.Add(new Rectangle(410, 125, 80, 80));
                    caveBlocks.Add(new Rectangle(490, 125, 80, 80));
                    caveBlocks.Add(new Rectangle(570, 125, 80, 80));
                    caveBlocks.Add(new Rectangle(650, 125, 80, 80));
                    caveBlocks.Add(new Rectangle(730, 125, 80, 80));
                    caveBlocks.Add(new Rectangle(810, 125, 80, 80));
                    caveBlocks.Add(new Rectangle(890, 125, 80, 80));
                    caveBlocks.Add(new Rectangle(970, 125, 80, 80));
                }

                if (portalRect.Intersects(player))
                {
                    screen = Screen.secondLevel;
                }

                else if (screen == Screen.secondLevel)
                {

                }








                    base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (screen == Screen.startScreen)
            {
                _spriteBatch.Draw(background1Texture, window, Color.White);
                _spriteBatch.Draw(background2Texture, window, Color.White);
                _spriteBatch.Draw(background3Texture, window, Color.White);
                _spriteBatch.Draw(secondLvlBgTexture, window, Color.White);
                _spriteBatch.DrawString(title, "The Adventures of Rock Man!", new Vector2(50, 150), Color.White);
            }
             else if (screen == Screen.firstLevel)
            {
                _spriteBatch.Draw(background1Texture, window, Color.White);
                _spriteBatch.Draw(background2Texture, window, Color.White);
                _spriteBatch.Draw(background3Texture, window, Color.White);
                _spriteBatch.Draw(firsLvlBgTexture, window, Color.White);
                _spriteBatch.Draw(spikeTexture, new Vector2(380, 277), Color.White);
                foreach (Rectangle platform in caveBlocks)
                    _spriteBatch.Draw(caveBlockTexture, platform, Color.White);
                _spriteBatch.Draw(RockmanIdleTexture, player, Color.White);
                _spriteBatch.Draw(keyTexture, keyRect, Color.White);
                _spriteBatch.Draw(portalTexture, portalRect, Color.White);
            }

            else if (screen == Screen.secondLevel)
            {
                _spriteBatch.Draw(background1Texture, window, Color.White);
                _spriteBatch.Draw(background2Texture, window, Color.White);
                _spriteBatch.Draw(background3Texture, window, Color.White);
                _spriteBatch.Draw(secondLvlBgTexture, window, Color.White);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
