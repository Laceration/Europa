using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Europa
{
    public static class TextureHelper
    {

        #region GraphicsDevice extensions

        /// <summary>
        /// The dictionary for caching block colours.
        /// </summary>
        private static Dictionary<Color, Texture2D> _blockColours = new Dictionary<Color, Texture2D>();

        /// <summary>
        /// Creates a block colour texture 1x1 pixel.
        /// </summary>
        /// <param name="gfx">The graphics device.</param>
        /// <param name="colour">The block colour to apply.</param>
        /// <returns>A Texture2D, 1x1 pixel in the colour specified.</returns>
        public static Texture2D BlockColour(this GraphicsDevice gfx, Color colour)
        {
            // check the cache
            if (_blockColours.ContainsKey(colour))
            {
                // if it's in the case, just return it
                return _blockColours[colour];
            }
            else
            {
                // otherwise create it
                var texture = new Texture2D(gfx, 1, 1);

                texture.SetData<Color>(new[] { colour });

                // be sure to cache it
                _blockColours[colour] = texture;

                return texture;
            }
        }

        #endregion

    }
}
