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
using Europa.Console;

namespace Europa
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class EuropaGame : Microsoft.Xna.Framework.Game
    {

        #region Private members

        /// <summary>
        /// The graphics device manager for this game instance.
        /// </summary>
        private GraphicsDeviceManager _gfxDeviceManager;

        /// <summary>
        /// The console instance.
        /// </summary>
        private EuropaConsole _europaConsole;
        
        /// <summary>
        /// The game font.
        /// </summary>
        private SpriteFont _font;

        #endregion

        #region Properties

        /// <summary>
        /// The sprite batch used for rendering operations.
        /// </summary>
        public SpriteBatch Drawer
        {
            get;
            private set;
        }

        /// <summary>
        /// The keyboard for this game.
        /// </summary>
        public Input.Keyboard Keyboard
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Instantiates a new Europa game instance.
        /// </summary>
        public EuropaGame()
        {
            this._gfxDeviceManager = new GraphicsDeviceManager(this);

            this._gfxDeviceManager.PreferredBackBufferWidth = 800;
            this._gfxDeviceManager.PreferredBackBufferHeight = 600;

            this.Content.RootDirectory = "Content";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the game font.
        /// </summary>
        /// <returns></returns>
        internal SpriteFont GetFont()
        {
            return this._font;
        }

        #endregion

        #region Game life-cycle

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // instantiate the sprite batch
            this.Drawer = new SpriteBatch(this.GraphicsDevice);
            this.Keyboard = new Input.Keyboard();

            // load the game font
            this._font = this.Content.Load<SpriteFont>("europa");

            // set up the console
            this._europaConsole = new EuropaConsole(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        #endregion

        #region Main loop

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            this._europaConsole.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.Drawer.Begin();

            this._europaConsole.Draw(gameTime);

            this.Drawer.End();

            base.Draw(gameTime);
        }

        #endregion
        
    }
}
