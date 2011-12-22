using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Europa.Actors
{
    public class Blob : Actor
    {

        #region Private members

        /// <summary>
        /// The blob's texture.
        /// </summary>
        private Texture2D _blobTexture;

        #endregion

        #region Constructor

        /// <summary>
        /// Instantiates a new Blob.
        /// </summary>
        /// <param name="colour">The colour of the blob.</param>
        public Blob(EuropaGame game, Color colour, Vector2 location)
            : base(game)
        {
            this._blobTexture = game.GraphicsDevice.BlockColour(colour);
            base._location = location;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the Blob.
        /// </summary>
        public override void Draw()
        {
            base._game.Drawer.Draw(this._blobTexture, base._location, null, Color.White, 0f, Vector2.Zero, 12f, SpriteEffects.None, 0);
        }

        #endregion

    }
}
