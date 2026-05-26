using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
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
        Texture2D caveBlockTexture, firsLvlBgTexture, secondLvlBgTexture, background1Texture, background2Texture, background3Texture, spikeTexture, keyTexture, portalTexture, wallportalTexture;
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
        private bool islocked = false;
        private bool gateopen = false;
        private bool keycollected = false;


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
            wallportalTexture = Content.Load<Texture2D>("wallportal");

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
           
            // start screen
            if (screen == Screen.startScreen)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.firstLevel;
            }

            //first level
            else if (screen == Screen.firstLevel)
            {

                spikeRect = new Rectangle(390, 280, 20, 60);

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
                    keycollected = true;
                    
                }

                if (portalRect.Intersects(player))
                {
                    screen = Screen.secondLevel;
                    playerPosition = new Vector2(10, 10);
                    keycollected = false;
                }

            }
            //second level
            else if (screen == Screen.secondLevel)
            {
                spikeRect = new Rectangle(810, 280, 20, 60);
                
                if (player.Left <= 0 && player.Y < 125 || player.Right >= 1000 || player.Left <= 0 && player.Y > 330)
                {
                    playerPosition.X += -rockmanSpeed.X;
                }

                caveBlocks.Clear();
                portalRect = new Rectangle(10, 10, 70, 90);
                caveBlocks.Add(new Rectangle(0, 125, 80, 80));
                caveBlocks.Add(new Rectangle(80, 125, 80, 80));
                caveBlocks.Add(new Rectangle(160, 125, 80, 80));
                caveBlocks.Add(new Rectangle(240, 125, 80, 80));
                caveBlocks.Add(new Rectangle(310, 125, 80, 80));
                caveBlocks.Add(new Rectangle(390, 125, 80, 80));
                caveBlocks.Add(new Rectangle(470, 125, 80, 80));
                caveBlocks.Add(new Rectangle(0, 330, 80, 80));
                caveBlocks.Add(new Rectangle(80, 330, 80, 80));
                caveBlocks.Add(new Rectangle(160, 330, 80, 80));
                caveBlocks.Add(new Rectangle(240, 330, 80, 80));
                caveBlocks.Add(new Rectangle(310, 330, 80, 80));
                caveBlocks.Add(new Rectangle(390, 330, 80, 80));
                caveBlocks.Add(new Rectangle(470, 330, 80, 80));
                caveBlocks.Add(new Rectangle(550, 330, 80, 80));
                caveBlocks.Add(new Rectangle(630, 330, 80, 80));
                caveBlocks.Add(new Rectangle(710, 330, 80, 80));
                caveBlocks.Add(new Rectangle(790, 330, 80, 80));
                caveBlocks.Add(new Rectangle(870, 330, 80, 80));
                caveBlocks.Add(new Rectangle(950, 330, 80, 80));
                
                if (player.Right <= 470 && player.Y > 130)
                {
                    islocked = true;
                }

                if (islocked)
                {
                    caveBlocks.Add(new Rectangle(470, 250, 80, 80));
                    keyRect = new Rectangle(570, 260, 30, 60);
                }

                if (player.Right < -1)
                {
                    playerPosition.X = 930;
                    playerPosition.Y = 250;

                }

                if (player.Intersects(keyRect))
                {
                   gateopen = true;
                    keycollected = true;
                }
                if (gateopen)
                {
                    caveBlocks.Remove(new Rectangle(870, 330, 80, 80));
                    caveBlocks.Remove(new Rectangle(950, 330, 80, 80));
                }

                if (player.Intersects(spikeRect) || player.Top > 1000)
                {
                    playerPosition.Y = 10;
                    playerPosition.X = 10;
                }

            }

                base.Update(gameTime);
            
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
                if (keycollected == false)
                {
                    _spriteBatch.Draw(keyTexture, keyRect, Color.White);
                }
                _spriteBatch.Draw(portalTexture, portalRect, Color.White);
            }

            else if (screen == Screen.secondLevel)
            {
                _spriteBatch.Draw(background1Texture, window, Color.White);
                _spriteBatch.Draw(background2Texture, window, Color.White);
                _spriteBatch.Draw(background3Texture, window, Color.White);
                _spriteBatch.Draw(secondLvlBgTexture, window, Color.White);
                _spriteBatch.Draw(portalTexture, portalRect, Color.White);
                foreach (Rectangle platform in caveBlocks)
                    _spriteBatch.Draw(caveBlockTexture, platform, Color.White);
                _spriteBatch.Draw(RockmanIdleTexture, player, Color.White);
                
                if (islocked)
                {
                    if (keycollected == false)
                    {
                        _spriteBatch.Draw(keyTexture, keyRect, Color.White);
                    }
                    _spriteBatch.Draw(wallportalTexture, new Vector2(-3, 205), Color.White);
                }
                _spriteBatch.Draw(spikeTexture, new Vector2(800, 277), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
