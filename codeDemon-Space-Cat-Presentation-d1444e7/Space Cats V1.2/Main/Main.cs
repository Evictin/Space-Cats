#region using
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
// In the .NET pane, scroll down and select System.Xml. 
// Additional Using Statments Needed for the Storage Functions. 
using System.IO;
using System.Xml.Serialization; 

#endregion


namespace Space_Cats_V1._2
{
    //Main Class
    public class Main : Microsoft.Xna.Framework.Game
    {
        #region Variables
        /*
        //Declare Instance Variables ----------------------------------------------------------------------------
        private GraphicsDeviceManager z_graphics;
        private SpriteBatch z_spriteBatch;
        private ScrollingBackground z_backgroundImage1;
        private ScrollingBackground z_backgroundImage2;
        private Rectangle z_viewportRec;
        
        
        private ContentManager z_contentManager;
        
        
        //The Loading Manager
        private LoadingManager z_loadingManager;
        //The GameState Manager
        private GameStateManager z_gameStateManager;
        //The Asteroid Manager
        private AsteroidManager z_asteroidManager;
        //The Missle Manager
        private MissleManager z_missleManager;
        //The Enemy Manager
        private EnemyManager z_enemyManager;
        
        
        //Variables For Text Fonts
        private SpriteFont z_timerFont;
        private SpriteFont z_livesFont;
         * 
         * 
         * */

        //Declare Instance Variables
        private GraphicsDeviceManager z_graphics;
        private SpriteBatch z_spriteBatch;
        private Rectangle z_viewportRec;
        private ContentManager z_contentManager;
        private AudioManager z_audioManager;
        private UltimateManager z_ultimateManager;
        private KeyboardState z_previousKeyboardState = Keyboard.GetState();
        private GamePadState z_previousGamePadState = GamePad.GetState(PlayerIndex.One);



        #endregion


        //Constructor -------------------------------------------------------------------------------------------
        public Main()
        {
            this.z_graphics = new GraphicsDeviceManager(this);
            this.z_audioManager = new AudioManager(this);
            //this.z_graphics.PreferredBackBufferWidth = 1280;
            //this.z_graphics.PreferredBackBufferHeight = 720;

            this.z_graphics.IsFullScreen = false;
            this.z_graphics.SynchronizeWithVerticalRetrace = true;

            Content.RootDirectory = "Content";
            //Adds the xbox live Profile Service to the game
            this.Components.Add(new GamerServicesComponent(this));
            this.Components.Add(this.z_audioManager);
            Components.Add(new FrameRateCounter(this));
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            this.z_viewportRec = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth,
                                                GraphicsDevice.PresentationParameters.BackBufferHeight);
        }


        //Initialize Method -------------------------------------------------------------------------------------
        protected override void Initialize()
        {
            base.Initialize();
        }


        //Load Content Method -----------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            //Set the contentManger
            this.z_contentManager = new ContentManager(Services);

            // Create a new SpriteBatch, which can be used to draw textures.
            this.z_spriteBatch = new SpriteBatch(GraphicsDevice);

            //Set the viewPortRec
            this.z_viewportRec = new Rectangle(0, 0, z_graphics.GraphicsDevice.Viewport.Width,
                                                z_graphics.GraphicsDevice.Viewport.Height);

            //Add Objects to Game Services
            this.Services.AddService(typeof(SpriteBatch), z_spriteBatch);
            this.Services.AddService(typeof(ContentManager), z_contentManager);
            this.Services.AddService(typeof(GraphicsDeviceManager), z_graphics);
            this.Services.AddService(typeof(Rectangle), z_viewportRec);

            //Initialize the Ultimate Manager
            this.z_ultimateManager = new UltimateManager(this, this.z_contentManager);

        }


        //Unload Content Method ----------------------------------------------------------------------------------
        protected override void UnloadContent()
        {
        }


        //Main Update Method -------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);
            if(this.z_contentManager != null)
                this.z_ultimateManager.Update(currentKeyboardState, this.z_previousKeyboardState, gameTime, this.z_contentManager, currentGamePadState, this.z_previousGamePadState);

            this.z_previousKeyboardState = currentKeyboardState;
            this.z_previousGamePadState = currentGamePadState;
            base.Update(gameTime);
        }


        //Draw Method --------------------------------------------------------------------------------------------
        protected override void Draw(GameTime gameTime)
        {
            //Clear all images
            GraphicsDevice.Clear(Color.Black);
            this.z_spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            this.z_ultimateManager.Draw(gameTime);



            //Close Sprite Batch
            this.z_spriteBatch.End();

            base.Draw(gameTime);
        }



        //Other Methods add here ---------------------------------------------------------------------------------





    }
}
