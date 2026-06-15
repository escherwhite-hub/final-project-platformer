using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace final_project_platformer
{
    enum Screen
    {
        startScreen,
        firstLevel,
        secondLevel,
        thirdLevel,
        fourthLevel,
        finalLevel
        
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

       
        Rectangle window, player, spikeRect, keyRect, portalRect, mushroomRect, roofspikeRect, skeletonRect, startbuttonRect, exitbuttonRect;
        Texture2D caveBlockTexture, firsLvlBgTexture, secondLvlBgTexture, background1Texture, background2Texture, background3Texture, spikeTexture, keyTexture, portalTexture, wallportalTexture, floorPortalTexture, mushroomTexture, roofspikeTexture, darknessTexture, skeletonTexture, startbuttonTexture, exitbuttonTexture, signTexture;
        MouseState mouseState;
        KeyboardState keyboardState;
        List<Rectangle> caveBlocks;
        List<Rectangle> mushrooms;
        List<Rectangle> spikes;

        Texture2D RockmanIdleTexture, rockmanJumpTexture, rockmanTexture, rockmanFallingTexture,RockmanIdlechainTexture;
        Vector2 rockmanSpeed, skeletonSpeed;
        Vector2 playerPosition;
        float gravity = 0.2f; // This is how fast player accelerated downwards
        float jumpSpeed = 9.2f; // This will determine the strength of the jump
        bool onGround = false;
        Screen screen;
        SpriteFont title, info;
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

            mushrooms = new List<Rectangle>();
            mushrooms.Add(new Rectangle(160, 665, 80, 60));
            mushrooms.Add(new Rectangle(420, 315, 80, 60));

            spikes = new List<Rectangle>();
            spikes.Add(new Rectangle(390, 280, 20, 60));

            keyRect = new Rectangle(20, 105, 30, 60);
            portalRect = new Rectangle(900, 10, 70, 90);

            rockmanSpeed = Vector2.Zero;
            playerPosition = new Vector2(10, 700);
            player = new Rectangle(10, 700, 60, 80);
            roofspikeRect = new Rectangle(610, 180, 45, 220);
            skeletonRect = new Rectangle(20, 117, 20, 60);
            skeletonSpeed = new Vector2(1,0);
            startbuttonRect = new Rectangle(400, 400, 200,90);
            exitbuttonRect = new Rectangle(400, 510, 200, 90);

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
            mushroomTexture = Content.Load<Texture2D>("mushroom");
            roofspikeTexture = Content.Load<Texture2D>("roofspike");
            title = Content.Load<SpriteFont>("title");
            info = Content.Load<SpriteFont>("info");
            wallportalTexture = Content.Load<Texture2D>("wallportal");
            floorPortalTexture = Content.Load<Texture2D>("floorportal");
            rockmanJumpTexture = Content.Load<Texture2D>("rockmanjump");
            RockmanIdlechainTexture = Content.Load<Texture2D>("rockmanidlechain");
            rockmanFallingTexture = Content.Load<Texture2D>("falling");
            darknessTexture = Content.Load<Texture2D>("darkness");
            skeletonTexture = Content.Load<Texture2D>("skeleton");
            startbuttonTexture = Content.Load<Texture2D>("startbutton");
            exitbuttonTexture = Content.Load<Texture2D>("exitbutton");
            signTexture = Content.Load<Texture2D>("sign");
            rockmanTexture = rockmanJumpTexture;
            


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
                rockmanTexture = rockmanJumpTexture;
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

            if (rockmanSpeed.Y > 1)
            {
                rockmanTexture = rockmanFallingTexture;
            }

            playerPosition.Y += rockmanSpeed.Y;
            player.Location = playerPosition.ToPoint();

            foreach (Rectangle platform in caveBlocks)
                if (player.Intersects(platform))
                {
                    if (rockmanSpeed.Y > 0f) // player lands on platform
                    {
                        onGround = true;
                        rockmanTexture = RockmanIdleTexture; 
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
                if (startbuttonRect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.firstLevel;

                if (exitbuttonRect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                    Exit();


            }

            //first level
            else if (screen == Screen.firstLevel)
            {
                
                

                if (player.Left <= 0 || player.Right >= 1000)
                {
                    playerPosition.X += -rockmanSpeed.X;
                }
                foreach (Rectangle spike in spikes)
                    if (player.Intersects(spike))
                    {
                        playerPosition.Y = 700;
                        playerPosition.X = 10;
                    }
                if ( player.Top > 1000)
                {
                    playerPosition.Y = 700;
                    playerPosition.X = 10;
                }
                if (player.Intersects(keyRect) && !keycollected)
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
                spikes.Clear();
                spikes.Add(new Rectangle(810, 280, 20, 60));
                spikes.Add(new Rectangle(260, 550, 20, 60));
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
                caveBlocks.Add(new Rectangle(790, 720, 80, 80));
                caveBlocks.Add(new Rectangle(870, 720, 80, 80));
                caveBlocks.Add(new Rectangle(950, 720, 80, 80));
                caveBlocks.Add(new Rectangle(160, 600, 80, 80));
                caveBlocks.Add(new Rectangle(240, 600, 80, 80));
                caveBlocks.Add(new Rectangle(310, 600, 80, 80));
                caveBlocks.Add(new Rectangle(390, 620, 80, 80));
                caveBlocks.Add(new Rectangle(470, 620, 80, 80));
                caveBlocks.Add(new Rectangle(550, 620, 80, 80));
                caveBlocks.Add(new Rectangle(630, 620, 80, 80));

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

                foreach (Rectangle spike in spikes)
                    if (player.Intersects(spike))
                    {
                        playerPosition.Y = 10;
                        playerPosition.X = 10;
                    }

                if (player.X < 630 && player.Y > 330)
                {
                    caveBlocks.Remove(new Rectangle(390, 620, 80, 80));
                    caveBlocks.Remove(new Rectangle(470, 620, 80, 80));
                }
                if (player.X < 240 && player.Y > 810)
                {
                    screen = Screen.thirdLevel;
                    playerPosition = new Vector2(10, 10);
                    keycollected = false;
                    jumpSpeed = 6f;
                }

            }

            //third level
            else if (screen == Screen.thirdLevel)
            {
                spikes.Clear();
                spikes.Add(new Rectangle(780, 325, 20, 60));
                spikes.Add(new Rectangle(360, 325, 20, 60));

                if (player.Left <= 0 || player.Right >= 1000)
                {
                    playerPosition.X += -rockmanSpeed.X;
                }
                caveBlocks.Clear();
                keyRect = new Rectangle(650, 30, 30, 60);
                portalRect = new Rectangle(890, 530, 70, 90);
                caveBlocks.Add(new Rectangle(0, 720, 80, 80));
                caveBlocks.Add(new Rectangle(80, 720, 80, 80));
                caveBlocks.Add(new Rectangle(160, 720, 80, 80));
                caveBlocks.Add(new Rectangle(260, 370, 80, 80));
                caveBlocks.Add(new Rectangle(340, 370, 80, 80));
                caveBlocks.Add(new Rectangle(420, 370, 80, 80));
                caveBlocks.Add(new Rectangle(500, 370, 80, 80));
                caveBlocks.Add(new Rectangle(600, 100, 80, 80));
                caveBlocks.Add(new Rectangle(680, 100, 80, 80));
                caveBlocks.Add(new Rectangle(760, 100, 80, 80));
                caveBlocks.Add(new Rectangle(760, 375, 80, 80));
                caveBlocks.Add(new Rectangle(840, 375, 80, 80));
                caveBlocks.Add(new Rectangle(920, 375, 80, 80));
                caveBlocks.Add(new Rectangle(500, 720, 80, 80));
                caveBlocks.Add(new Rectangle(580, 720, 80, 80));
                caveBlocks.Add(new Rectangle(660, 720, 80, 80));

                foreach (Rectangle mushroom in mushrooms)
                    if (player.Intersects(mushroom))
                {
                    rockmanSpeed.Y = -11.5f;
                    onGround = false;
                }
                if (player.Intersects(keyRect))
                {
                    keycollected = true;
                }

                if (player.Intersects(roofspikeRect) || player.Top > 1000)
                    {
                        playerPosition.Y = 10;
                        playerPosition.X = 10;
                    }

                foreach (Rectangle spike in spikes)
                    if (player.Intersects(spike))
                    {
                        playerPosition.Y = 10;
                        playerPosition.X = 10;
                    }

                if (portalRect.Intersects(player))
                {
                    screen = Screen.fourthLevel;
                    playerPosition.Y = 650;
                    playerPosition.X = 800;
                    jumpSpeed = 6f;
                }

            }
            //fourth level
            else if (screen == Screen.fourthLevel)
            {
                if (player.Left <= 0 || player.Right >= 1000)
                {
                    playerPosition.X += -rockmanSpeed.X;
                }

                spikes.Clear();
                spikes.Add(new Rectangle(440, 667, 20, 60));
                spikes.Add(new Rectangle(650, 377, 20, 60));

                portalRect = new Rectangle(10, 10, 70, 90);

                caveBlocks.Clear();
                caveBlocks.Add(new Rectangle(0, 720, 80, 80));
                caveBlocks.Add(new Rectangle(80, 720, 80, 80));
                caveBlocks.Add(new Rectangle(160, 720, 80, 80));
                caveBlocks.Add(new Rectangle(370, 720, 80, 80));
                caveBlocks.Add(new Rectangle(450, 720, 80, 80));
                caveBlocks.Add(new Rectangle(760, 720, 80, 80));
                caveBlocks.Add(new Rectangle(840, 720, 80, 80));
                caveBlocks.Add(new Rectangle(920, 720, 80, 80));
                caveBlocks.Add(new Rectangle(-30, 430, 80, 80));
                caveBlocks.Add(new Rectangle(150, 430, 80, 80));
                caveBlocks.Add(new Rectangle(230, 430, 80, 80));
                caveBlocks.Add(new Rectangle(560, 430, 80, 80));
                caveBlocks.Add(new Rectangle(640, 430, 80, 80));
                caveBlocks.Add(new Rectangle(720, 430, 80, 80));
                caveBlocks.Add(new Rectangle(920, 430, 80, 80));
                caveBlocks.Add(new Rectangle(0, 170, 80, 80));
                caveBlocks.Add(new Rectangle(80, 170, 80, 80));
                caveBlocks.Add(new Rectangle(160, 170, 80, 80));
                caveBlocks.Add(new Rectangle(240, 170, 80, 80));
                caveBlocks.Add(new Rectangle(310, 170, 80, 80));
                caveBlocks.Add(new Rectangle(390, 170, 80, 80));
                caveBlocks.Add(new Rectangle(470, 170, 80, 80));
                caveBlocks.Add(new Rectangle(550, 170, 80, 80));
                caveBlocks.Add(new Rectangle(630, 170, 80, 80));
                caveBlocks.Add(new Rectangle(710, 170, 80, 80));

                mushrooms.Clear();
                mushrooms.Add(new Rectangle(5, 667, 80, 60));
                mushrooms.Add(new Rectangle(915, 377, 80, 60));

                foreach (Rectangle mushroom in mushrooms)
                    if (player.Intersects(mushroom))
                    {
                        rockmanSpeed.Y = -11.5f;
                        onGround = false;
                    }
                foreach (Rectangle spike in spikes)
                    if (player.Intersects(spike))
                    {
                        playerPosition.Y = 700;
                        playerPosition.X = 800;
                    }
                if (player.Top > 1000)
                {
                    playerPosition.Y = 700;
                    playerPosition.X = 800;
                }
                if (player.Intersects(skeletonRect))
                    {
                        playerPosition.Y = 700;
                        playerPosition.X = 800;
                    }
                skeletonRect.X += (int)skeletonSpeed.X;

                if (skeletonRect.X > 600)
                {
                    skeletonSpeed.X *= -1;
                }

                if (skeletonRect.X < 15)
                {
                    skeletonSpeed.X *= -1;
                }

                if (portalRect.Intersects(player))
                {
                    screen = Screen.finalLevel;
                    playerPosition.Y = 700;
                    playerPosition.X = 800;
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
                _spriteBatch.DrawString(title, "The Adventures of Squid Man!", new Vector2(50, 150), Color.White);
                _spriteBatch.Draw(startbuttonTexture,startbuttonRect, Color.White);
                _spriteBatch.Draw(exitbuttonTexture, exitbuttonRect, Color.White);
            }
             else if (screen == Screen.firstLevel)
            {
                _spriteBatch.Draw(background1Texture, window, Color.White);
                _spriteBatch.Draw(background2Texture, window, Color.White);
                _spriteBatch.Draw(background3Texture, window, Color.White);
                _spriteBatch.Draw(firsLvlBgTexture, window, Color.White);
                _spriteBatch.Draw(signTexture, new Vector2(200,695), Color.White);
                _spriteBatch.Draw(spikeTexture, new Vector2(380, 277), Color.White);
                foreach (Rectangle platform in caveBlocks)
                    _spriteBatch.Draw(caveBlockTexture, platform, Color.White);
                _spriteBatch.Draw(rockmanTexture, player, Color.White);
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
                _spriteBatch.Draw(floorPortalTexture, new Vector2(0,770), Color.White);
                foreach (Rectangle platform in caveBlocks)
                    _spriteBatch.Draw(caveBlockTexture, platform, Color.White);
                _spriteBatch.Draw(rockmanTexture, player, Color.White);
                
                if (islocked)
                {
                    if (keycollected == false)
                    {
                        _spriteBatch.Draw(keyTexture, keyRect, Color.White);
                    }
                    _spriteBatch.Draw(wallportalTexture, new Vector2(-3, 205), Color.White);
                }
                _spriteBatch.Draw(spikeTexture, new Vector2(800, 277), Color.White);
                _spriteBatch.Draw(spikeTexture, new Vector2(250, 547), Color.White);

            }
            else if (screen == Screen.thirdLevel)
            {
                _spriteBatch.Draw(background1Texture, window, Color.White);
                _spriteBatch.Draw(background2Texture, window, Color.White);
                _spriteBatch.Draw(background3Texture, window, Color.White);
                _spriteBatch.Draw(firsLvlBgTexture, window, Color.White);
                _spriteBatch.Draw(rockmanTexture, player, Color.White);
                if (playerPosition.X < 100 && playerPosition.Y > 500)
                {
                    _spriteBatch.DrawString(info, "oh no! you broke your legs!", new Vector2(100, 600), Color.White);
                }
                foreach (Rectangle platform in caveBlocks)
                    _spriteBatch.Draw(caveBlockTexture, platform, Color.White);
                _spriteBatch.Draw(mushroomTexture, mushroomRect, Color.White);
                foreach (Rectangle mushroom in mushrooms)
                    _spriteBatch.Draw(mushroomTexture, mushroom, Color.White);
                _spriteBatch.Draw(roofspikeTexture, roofspikeRect, Color.White);
                _spriteBatch.Draw(spikeTexture, new Vector2(770, 322), Color.White);
                _spriteBatch.Draw(spikeTexture, new Vector2(350, 322), Color.White);
                spikes.Add(new Rectangle(360, 325, 20, 60));
                if (keycollected == false)
                {
                    _spriteBatch.Draw(keyTexture, keyRect, Color.White);
                    
                }
                else
                {
                    _spriteBatch.Draw(portalTexture, portalRect, Color.White);
                }
            }

            

            

            if (screen == Screen.fourthLevel)
            {
                _spriteBatch.Draw(background1Texture, window, Color.White);
                _spriteBatch.Draw(background2Texture, window, Color.White);
                _spriteBatch.Draw(background3Texture, window, Color.White);
                _spriteBatch.Draw(secondLvlBgTexture, window, Color.White);
                _spriteBatch.Draw(rockmanTexture, player, Color.White);
                foreach (Rectangle mushroom in mushrooms)
                    _spriteBatch.Draw(mushroomTexture, mushroom, Color.White);
                _spriteBatch.Draw(spikeTexture, new Vector2(430, 665), Color.White);
                _spriteBatch.Draw(spikeTexture, new Vector2(640, 375), Color.White);
                
                foreach (Rectangle platform in caveBlocks)
                    _spriteBatch.Draw(caveBlockTexture, platform, Color.White);
                _spriteBatch.Draw(mushroomTexture, mushroomRect, Color.White);
                _spriteBatch.Draw(skeletonTexture, skeletonRect, Color.White);
                _spriteBatch.Draw(darknessTexture, new Vector2(playerPosition.X - 1475, playerPosition.Y - 1475), Color.White * 0.95f);







            }

                _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
