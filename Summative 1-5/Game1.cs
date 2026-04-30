using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices;
enum Screen
{
    Title,
    Animation,
    End
}
namespace Summative_1_5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Screen screen;
        MouseState mouseState, prevMouseState;
        Texture2D titleScreen, bnnuy, playButtonImg, charaset, dragonFly, dragoneFire;
        Rectangle bunnyRect, window, playButton, dragonRect;
        Vector2 bunnySpeed;
        SoundEffect bonk;
        Texture2D dragonFly, dragonFire;
        Rectangle dragonRect;
        Vector2 dragonSpeed;
        bool fly = true;

        
        // A timer that stores milliseconds.
        float bunnyFrameTimer;
        int totalAnimations = 8; // change this when you add more

        float bunnyStartTimer;

        // An int that is the threshold for the timer.
        int threshold;

        // A Rectangle array that stores sourceRectangles for animations.
        Rectangle[] bunnyRectangles;

        // These bytes tell the spriteBatch.Draw() what sourceRectangle to display.
        byte previousAnimationIndex;
        byte currentAnimationIndex;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Title;
            mouseState = Mouse.GetState();
            base.Initialize();
            window = new Rectangle(0, 0, 1250, 1250);
            //384, 182
            playButton = new Rectangle(300, 200, 100, 100);
            bunnyRect = new Rectangle(100, 100, 100, 100);
            bunnySpeed = new Vector2(1, 0);
            dragonRect = new Rectangle(800, 100, 100, 100);
            dragonSpeed = new Vector2(0, 0);


            _graphics.PreferredBackBufferWidth = window.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = window.Height;   // set this value to the desired height of your window
            bunnyStartTimer = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            bunnyFrameTimer = 0;

            // You can change this to alter the speed of the animation (lower number = faster animation).
            threshold = 100;

            // Animation rectangles or smth 
            bunnyRectangles = new Rectangle[8];
            //BACKWARDS   bunnyRectangles[7] = new Rectangle(0, 0, 369, 313);
            //bunnyRectangles[6] = new Rectangle(372, 0, 311, 347);
            //bunnyRectangles[5] = new Rectangle(720, 0, 277, 369);
            //bunnyRectangles[4] = new Rectangle(0, 360, 363, 320);
            //bunnyRectangles[3] = new Rectangle(365, 360, 362, 317);
            //bunnyRectangles[2] = new Rectangle(726, 360, 340, 335);
            //bunnyRectangles[1] = new Rectangle(0, 720, 308, 365);
            //bunnyRectangles[0] = new Rectangle(315, 720, 335, 360);

            bunnyRectangles[0] = new Rectangle(0, 0, 369, 313);
            bunnyRectangles[1] = new Rectangle(372, 0, 311, 347);
            bunnyRectangles[2] = new Rectangle(720, 0, 277, 369);
            bunnyRectangles[3] = new Rectangle(0, 315, 363, 320);
            bunnyRectangles[4] = new Rectangle(365, 350, 362, 317);
            bunnyRectangles[5] = new Rectangle(726, 372, 340, 335);
            bunnyRectangles[6] = new Rectangle(0, 325, 309, 370);
            bunnyRectangles[7] = new Rectangle(315, 325, 318, 357);

            // This tells the animation to start on the left-side sprite.
            previousAnimationIndex = 2;
            currentAnimationIndex = 0;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playButtonImg = Content.Load<Texture2D>("playButton");
            bnnuy = Content.Load<Texture2D>("LayDumbRabbit");
            charaset = Content.Load<Texture2D>("BunnyRoll45");
            if (fly == true) {
                dragonFly = Content.Load<Texture2D>("dragonFly");
            }
            if(fly == false) {
                dragonFly = Content.Load<Texture2D>("dragonFireBreathe");
            }
                
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            this.Window.Title = mouseState.Position.ToString();
            mouseState = Mouse.GetState();    
            prevMouseState = mouseState;
            if (screen == Screen.Title)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && playButton.Contains(mouseState.Position))
                        screen = Screen.Animation;

            }
            else if (screen == Screen.Animation)
            {
                // TODO: Add your update logic here

                dragonRect.X += (int)dragonSpeed.X;
                dragonRect.Y += (int)dragonSpeed.Y;
                bunnyRect.X += (int)bunnySpeed.X;
                UpdateBunnyFrame(gameTime);
                if (dragonRect.X == 680)
                {
                    dragonSpeed = new Vector2(0, 0); 
                        fly = false;
                }


            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
           
            if (screen == Screen.Title)
            {

                _spriteBatch.Draw(playButtonImg, playButton, Color.White);
            }
            else if (screen == Screen.Animation)
            {
                // Only draw the area contained within the sourceRectangle.
                _spriteBatch.Draw(charaset, bunnyRect, bunnyRectangles[currentAnimationIndex], Color.White);
                if (bunnyRect.X == 570)
                {
                    _spriteBatch.Draw(dragonFly, dragonRect, Color.White);
                    
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpdateBunnyFrame(GameTime gameTime)
        {
            bunnyFrameTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (bunnyFrameTimer > threshold)
            {
                currentAnimationIndex++;
                if (currentAnimationIndex >= totalAnimations)
                {
                    currentAnimationIndex = 0;
                }
                bunnyFrameTimer = 0;
            }
            if (bunnyRect.X == 570)
            {
                bunnySpeed = new Vector2(0, 0);
                threshold = 0;
                currentAnimationIndex = 0;
                dragonSpeed = new Vector2(-1, 0);


            }
           
        }

    }
}
