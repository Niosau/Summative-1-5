using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
enum Screen
    {
    Title,Animation,End



}
namespace Summative_1_5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Screen screen;
        MouseState mouseState, prevMouseState;
        Texture2D titleScreen, bnnuy, playButtonImg, charaset;
        Rectangle bnnuyRect, window, playButton;
        Vector2 bnnuySpeed;
        SoundEffect bonk;

        // A timer that stores milliseconds.
        float timer;

        // An int that is the threshold for the timer.
        int threshold;

        // A Rectangle array that stores sourceRectangles for animations.
        Rectangle[] sourceRectangles;

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
            bnnuyRect = new Rectangle(300, 200, 100, 100);
            _graphics.PreferredBackBufferWidth = window.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = window.Height;   // set this value to the desired height of your window

            base.Initialize();
        }

        protected override void LoadContent()
        {
            timer = 0;

            // Set an initial threshold of 250ms, you can change this to alter the speed of the animation (lower number = faster animation).
            threshold = 250;

            // Three sourceRectangles contain the coordinates of Alex's three down-facing sprites on the charaset.
            sourceRectangles = new Rectangle[4];
            sourceRectangles[0] = new Rectangle(0, 0, 300, 350);
            sourceRectangles[1] = new Rectangle(350, 0, 250, 350);
            sourceRectangles[2] = new Rectangle(600, 0, 300, 350);
            sourceRectangles[3] = new Rectangle(1000, 0, 300, 350);

            // This tells the animation to start on the left-side sprite.
            previousAnimationIndex = 2;
            currentAnimationIndex = 1;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playButtonImg = Content.Load<Texture2D>("playButton");
            bnnuy = Content.Load<Texture2D>("LayDumbRabbit");
            charaset = Content.Load<Texture2D>("bunnyRoll");
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
                int totalAnimations = 4; // change this when you add more

                if (timer > threshold)
                {
                    currentAnimationIndex++;

                    if (currentAnimationIndex >= totalAnimations)
                    {
                        currentAnimationIndex = 0;
                    }
                    
                    timer = 0;
                }
                else
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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
                Rectangle sourceRectangle = new Rectangle(0, 0, 600, 600);

                // Only draw the area contained within the sourceRectangle.
                _spriteBatch.Draw(charaset, new Vector2(100, 100), sourceRectangles[currentAnimationIndex], Color.White);

            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
