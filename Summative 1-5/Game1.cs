using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
enum Screen
{
    Title,
    Animation,
    Cooked,
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
        SpriteFont fontyMonty;
        Texture2D bnnuy, playButtonImg, charaset, dragonFly, dragonFire, dragonTexture, speed, waterGun, waterPellet;
        Texture2D backGround, grasslands, menuBg, menuText, death, coolText, rocks;
        Rectangle bunnyRect, window, playButton, dragonRect, bunnyRect2, bunnyRect3, bunnyRect4, bunnyRect5, speedRect;
        Rectangle offset, textRect, offset2, offset3, offset4, offset5, bulletRect, bulletRect2, bulletRect3, bulletRect4, bulletRect5;
        Vector2 bunnySpeed, bulletSpeed;
        SoundEffect commander, gunshot, gunshot2, gunshot3, gunshot4, gunshot5, roar, holup, surprised, deaded;
        private Song Menu, patient0;
        Vector2 dragonSpeed;
        Color bg;






        float wait = 0, cookedWait = 0, opacity = 0f, fadeSpeed = 0.5f, armyShow = 0, armyGo = 0;
        bool fly = true, army = false, soundBoom = false, offCooked = false, triggerDigger = false, booomy = false, stopSound = false;
        bool end = false, surprise = false, holdIt = false, yay = false, surprise2 = false, holdIt2 = false, yay2 = false, dragon = false, dragon2 = false;
        
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
            window = new Rectangle(0, 0, 800, 800);
            //384, 182
            playButton = new Rectangle(300, 400, 200, 100);
            bunnyRect = new Rectangle(100, 345, 100, 100);
            bunnyRect2 = new Rectangle(0, 345, 100, 100);
            bunnyRect3 = new Rectangle(-100, 345, 100, 100);
            bunnyRect4 = new Rectangle(-200, 345, 100, 100);
            bunnyRect5 = new Rectangle(-300, 345, 100, 100);
            bunnySpeed = new Vector2(1, 0);

            bulletRect = new Rectangle(offset.X, offset.Y, 20, 20);
            bulletRect2 = new Rectangle(offset2.X, offset2.Y, 20, 20);
            bulletRect3 = new Rectangle(offset3.X, offset3.Y, 20, 20);
            bulletRect4 = new Rectangle(offset4.X, offset4.Y, 20, 20);
            bulletRect5 = new Rectangle(offset5.X, offset5.Y, 20, 20);

            textRect = new Rectangle(155, 100, 500, 300);

            speedRect = new Rectangle(415, 350, 300, 300);
   
            dragonRect = new Rectangle(800, 330, 100, 100);
            dragonSpeed = new Vector2(0, 0);

            bulletSpeed = new Vector2(7, 0);

            _graphics.PreferredBackBufferWidth = window.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = window.Height;   // set this value to the desired height of your window
            bunnyStartTimer = 0;
            _graphics.ApplyChanges();
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
            bunnyRectangles[3] = new Rectangle(0, 360, 363, 320);
            bunnyRectangles[4] = new Rectangle(365, 360, 362, 317);
            bunnyRectangles[5] = new Rectangle(726, 360, 340, 335);
            bunnyRectangles[6] = new Rectangle(0, 720, 308, 365);
            bunnyRectangles[7] = new Rectangle(315, 720, 335, 360);

            // This tells the animation to start on the left-side sprite.
            previousAnimationIndex = 2;
            currentAnimationIndex = 0;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playButtonImg = Content.Load<Texture2D>("playButton");
            bnnuy = Content.Load<Texture2D>("LayDumbRabbit");
            charaset = Content.Load<Texture2D>("BunnyRoll45");
            grasslands = Content.Load<Texture2D>("grasslands");
            menuBg = Content.Load<Texture2D>("grassBg");
            menuText = Content.Load<Texture2D>("bunnyTxT");
            coolText = Content.Load<Texture2D>("coolText");

            commander = Content.Load<SoundEffect>("Commander2");

            gunshot = Content.Load<SoundEffect>("Gunshot Sound Effect Single Shot");
            gunshot2 = Content.Load<SoundEffect>("Gunshot Sound Effect Single Shot");
            gunshot3 = Content.Load<SoundEffect>("Gunshot Sound Effect Single Shot");
            gunshot4 = Content.Load<SoundEffect>("Gunshot Sound Effect Single Shot");
            gunshot5 = Content.Load<SoundEffect>("Gunshot Sound Effect Single Shot");

            roar = Content.Load<SoundEffect>("Dragon Roar - Free Sound Effect");

            deaded = Content.Load<SoundEffect>("asdfmovie grave sound");

            surprised = Content.Load<SoundEffect>("Metal Gear Solid_ Alert (!)");

            holup = Content.Load<SoundEffect>("Record Scratch Sound Effect");
            rocks = Content.Load<Texture2D>("rocks");

            Menu = Content.Load<Song>("Able Sisters Animal Crossing City Folk Music 1 Hour Extended HD");
            patient0 = Content.Load<Song>("Official Tower Defense Simulator OST - Sound The Alarm_");

            waterGun = Content.Load<Texture2D>("waterGun");
            waterPellet = Content.Load<Texture2D>("waterPellet");
            dragonFly = Content.Load<Texture2D>("dragonFly"); 
            dragonFire = Content.Load<Texture2D>("dragonFireBreathe");
            dragonTexture = dragonFly;

            speed = Content.Load<Texture2D>("speed");

            death = Content.Load<Texture2D>("gravestone");

            fontyMonty = Content.Load<SpriteFont>("File");

            backGround = menuBg;
            MediaPlayer.IsRepeating = true;

            // Adjust the volume (0.0f to 1.0f)
            MediaPlayer.Volume = 0.5f;

            

            // Start playing the background music
            




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
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(Menu);
                }
                if (mouseState.LeftButton == ButtonState.Pressed && playButton.Contains(mouseState.Position))
                {
                    screen = Screen.Animation;

                    MediaPlayer.Stop();

                }
                // Check if the media player is already playing, if so, stop it
                
                
            }
            else if (screen == Screen.Animation)
            {
                


                
                backGround = grasslands;
                // TODO: Add your update logic here
                offset = new Rectangle(bunnyRect.X + bunnyRect.Width - 30, bunnyRect.Y + 20, 50,50);
                offset2 = new Rectangle(bunnyRect2.X + bunnyRect2.Width - 30, bunnyRect2.Y + 22, 50, 50);
                offset3 = new Rectangle(bunnyRect3.X + bunnyRect3.Width - 30, bunnyRect3.Y + 22, 50, 50);
                offset4 = new Rectangle(bunnyRect4.X + bunnyRect4.Width - 30, bunnyRect4.Y + 22, 50, 50);
                offset5 = new Rectangle(bunnyRect5.X + bunnyRect5.Width - 30, bunnyRect5.Y + 22, 50, 50);
                if (!offCooked)
                {
                    bulletRect = new Rectangle(offset.X, offset.Y, 20, 20);
                    bulletRect2 = new Rectangle(offset2.X, offset2.Y, 20, 20);
                    bulletRect3 = new Rectangle(offset3.X, offset3.Y, 20, 20);
                    bulletRect4 = new Rectangle(offset4.X, offset4.Y, 20, 20);
                    bulletRect5 = new Rectangle(offset5.X, offset5.Y, 20, 20);
                }

                
                dragonRect.X += (int)dragonSpeed.X;
                dragonRect.Y += (int)dragonSpeed.Y;
                bunnyRect.X += (int)bunnySpeed.X;
                bunnyRect2.X += (int)bunnySpeed.X;
                bunnyRect3.X += (int)bunnySpeed.X;
                bunnyRect4.X += (int)bunnySpeed.X;
                bunnyRect5.X += (int)bunnySpeed.X;
                UpdateBunnyFrame(gameTime);

                if (bunnyRect.X == 570 && !soundBoom)
                {
                    bunnySpeed = new Vector2(0, 0);
                    threshold = 0;
                    currentAnimationIndex = 0;
                    dragonSpeed = new Vector2(-1, 0);
                    fly = false;
                    surprise = true;
                }

                if (surprise && !surprise2)
                {
                    surprised.Play();
                    surprise2 = true;
                }

                if (dragonRect.X == 680 && !soundBoom)
                {
                    dragonSpeed = new Vector2(0, 0); 
                    dragonTexture = dragonFire;
                    bunnySpeed.X = -3;
                    threshold = 50;
                }

                if (dragonRect.X == 680 && !dragon2)
                {
                    dragon2 = true;
                    
                }

                if (bunnyRect.X == window.X && !soundBoom)
                {
                    bunnySpeed.X = 0;
                    soundBoom = true;
                    commander.Play();
                }
                
                if (wait >= 1.5 && !offCooked)
                {
                    army = true;
  
                }
                if (soundBoom)
                {
                    wait += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (army && !triggerDigger)
                {

                    armyShow += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    

                    if (armyShow >= 1)
                    {
                       triggerDigger = true;
                    }
                }
               
                if (triggerDigger && !offCooked)
                {
                    bunnySpeed.X = 1; 
                    if (bunnyRect.X == 570)
                    {
                        bunnySpeed = new Vector2(0, 0);
                        threshold = 0;
                        currentAnimationIndex = 0;
                        armyGo += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        holdIt = true;
                        if (armyGo >= 1)
                        {
                            screen = Screen.Cooked;

                        }
                    }
                    if (holdIt && !holdIt2)
                    {
                        holdIt2 = true;
                        holup.Play();
                    }
                }
               if (offCooked)
                {
                    dragonTexture = speed;
                    bulletRect.X += (int)bulletSpeed.X;
                    bulletRect2.X += (int)bulletSpeed.X;
                    bulletRect3.X += (int)bulletSpeed.X;
                    bulletRect4.X += (int)bulletSpeed.X;
                    bulletRect5.X += (int)bulletSpeed.X;
                    booomy = true;
                }
               if (booomy && !stopSound)
                {
                    booomy = false;
                    stopSound = true;
                    gunshot.Play();
                    gunshot.Play();
                    gunshot.Play();
                    gunshot.Play();
                    gunshot.Play();


                }
                if (bulletRect5.X >= 900)
                {
                    screen = Screen.End;
                }

            }
            else if (screen == Screen.Cooked)
            {
                backGround = rocks;
                cookedWait += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (cookedWait >= 3)
                {
                offCooked = true;
                screen = Screen.Animation;
                }
                
                    

                if (opacity < 1.0f)
                {
                    opacity += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (opacity > 1.0f) opacity = 1.0f;
                }
            }
            else if (screen == Screen.End)
            {
                backGround = death;
                deaded.Play();
                textRect = new Rectangle(155, 0, 500, 200);
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Exit();
                }
            }

                base.Update(gameTime);

        }
                // TODO: Add your update logic here

                
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (screen == Screen.Title)
            {

                _spriteBatch.Draw(backGround, window, Color.White);
                _spriteBatch.Draw(playButtonImg, playButton, Color.White);
                _spriteBatch.Draw(menuText, textRect, Color.White);
            }
            else if (screen == Screen.Animation)
            {

                _spriteBatch.Draw(backGround, window, Color.White);
                // Only draw the area contained within the sourceRectangle.
                _spriteBatch.Draw(charaset, bunnyRect, bunnyRectangles[currentAnimationIndex], Color.White);
                if (army == true)
                {
                    _spriteBatch.Draw(charaset, bunnyRect2, bunnyRectangles[currentAnimationIndex], Color.White);
                    _spriteBatch.Draw(charaset, bunnyRect3, bunnyRectangles[currentAnimationIndex], Color.White);
                    _spriteBatch.Draw(charaset, bunnyRect4, bunnyRectangles[currentAnimationIndex], Color.White);
                    _spriteBatch.Draw(charaset, bunnyRect5, bunnyRectangles[currentAnimationIndex], Color.White);
                }

                if (bunnyRect.X == 570 || fly == false)
                {
                    _spriteBatch.Draw(dragonTexture, dragonRect, Color.White);


                }
                if (triggerDigger == true && bunnyRect.X == 570)
                {

                    _spriteBatch.Draw(waterGun, offset, Color.White);
                    _spriteBatch.Draw(waterGun, offset, Color.White);
                    _spriteBatch.Draw(waterGun, offset2, Color.White);
                    _spriteBatch.Draw(waterGun, offset3, Color.White);
                    _spriteBatch.Draw(waterGun, offset4, Color.White);
                    _spriteBatch.Draw(waterGun, offset5, Color.White);

                }
                if (offCooked)
                {
                    _spriteBatch.Draw(waterPellet, bulletRect, Color.White);
                    _spriteBatch.Draw(waterPellet, bulletRect2, Color.White);
                    _spriteBatch.Draw(waterPellet, bulletRect3, Color.White);
                    _spriteBatch.Draw(waterPellet, bulletRect4, Color.White);
                    _spriteBatch.Draw(waterPellet, bulletRect5, Color.White);
                    if (bulletRect.X >= 700)
                    {
                        bulletRect.X = 900;
                    }
                    if (bulletRect2.X >= 700)
                    {
                        bulletRect2.X = 900;
                    }
                    if (bulletRect3.X >= 700)
                    {
                        bulletRect3.X = 900;
                    }
                    if (bulletRect4.X >= 700)
                    {
                        bulletRect4.X = 900;
                    }
                    if (bulletRect5.X >= 700)
                    {
                        bulletRect5.X = 900;
                    }


                }

            }
            else if (screen == Screen.Cooked)
            {
                _spriteBatch.Draw(backGround, window, Color.White);
                _spriteBatch.Draw(dragonTexture, new Vector2(300, 300), Color.White);
                _spriteBatch.Draw(speed, speedRect, Color.White * opacity);
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(backGround, window, Color.White);
                _spriteBatch.Draw(coolText, textRect, Color.WhiteSmoke);
            }
                _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpdateBunnyFrame(GameTime gameTime)
        {
            bunnyFrameTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;



            if(bunnySpeed.X > 0)
            {
                if (bunnyFrameTimer > threshold)
                {
                    currentAnimationIndex++;
                    if (currentAnimationIndex >= totalAnimations)
                    {
                        currentAnimationIndex = 0;
                    }
                    bunnyFrameTimer = 0;
                }
                
            }
            else if (bunnySpeed.X < 0)
            {
                threshold = 100;
                if (bunnyFrameTimer > threshold)
                {
                    
                    currentAnimationIndex--;
                    if (currentAnimationIndex >= totalAnimations)
                    {
                        currentAnimationIndex = 7;
                        fly = false;
                    }
                    bunnyFrameTimer = 0;
                    
                }
            }
            
           
        }

    }
}
