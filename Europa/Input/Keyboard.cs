using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Europa.Input
{
    public class Keyboard
    {

        #region Private members

        /// <summary>
        /// The last keyboard state.
        /// </summary>
        private Microsoft.Xna.Framework.Input.KeyboardState _lastState;

        #endregion

        #region Methods

        /// <summary>
        /// Gets an array of keys that were pressed and released.
        /// </summary>
        /// <returns>The list of keys pressed.</returns>
        public Keys[] KeysPressed()
        {
            var lastState = this._lastState.GetPressedKeys();
            this._lastState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            var currentState = this._lastState.GetPressedKeys();

            return lastState.Except(currentState).ToArray();
        }

        /// <summary>
        /// Gets an array of keys that are currently down.
        /// </summary>
        /// <returns>A list of keys that are currently down.</returns>
        public Keys[] KeysDown()
        {
            return Microsoft.Xna.Framework.Input.Keyboard.GetState().GetPressedKeys();
        }

        #endregion

    }
}
