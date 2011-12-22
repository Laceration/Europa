using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Europa.Console
{
    public class EuropaConsole
    {

        #region Private members

        /// <summary>
        /// The history of the console.
        /// </summary>
        private List<string> _history;

        /// <summary>
        /// The current line being entered.
        /// </summary>
        private string _currentLine;

        /// <summary>
        /// The background of the EuropaConsole.
        /// </summary>
        private Texture2D _background;

        /// <summary>
        /// The SpriteFont renderer to use.
        /// </summary>
        private SpriteFont _spriteFont;

        /// <summary>
        /// The reference to the game.
        /// </summary>
        private EuropaGame _game;

        /// <summary>
        /// The line the user is up to in the history (for up/down key)
        /// </summary>
        private int _historyLine = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Instantiates a new EuropaConsole.
        /// </summary>
        public EuropaConsole(EuropaGame game)
        {
            this._game = game;

            this._background = game.GraphicsDevice.BlockColour(new Color(0.25f, 0.25f, 0.25f, 0.5f)); // dark grey, translucent
            this._spriteFont = game.GetFont();

            this._history = new List<string>() { "test oldest line", "test middle line", "test new line" };
            this._currentLine = string.Empty;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Updates the EuropaConsole.
        /// </summary>
        /// <param name="gameTime">The elapsed game time.</param>
        public void Update(GameTime gameTime)
        {
            var keysPressed = this._game.Keyboard.KeysPressed();
            var keysDown = this._game.Keyboard.KeysDown();

            for (int i = 0; i < keysPressed.Length; i++)
            {
                int keyCode = (int)keysPressed[i];

                // a to z
                if (keyCode > 64 && keyCode < 90)
                {
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                    {
                        this._currentLine += (char)keyCode;
                    }
                    else
                    {
                        this._currentLine += (char)(keyCode + 32);
                    }
                }

                // backspace
                if (keyCode == 8 && this._currentLine.Length > 0)
                {
                    this._currentLine = this._currentLine.Remove(this._currentLine.Length - 1);
                }

                // spacebar
                if (keyCode == 32)
                {
                    this._currentLine += ' ';
                }

                // enter
                if (keyCode == 13)
                {
                    WriteLine(this._currentLine, true, true);

                    this._historyLine = this._history.Count;
                }

                /*

                // up key
                if (keyCode == 38)
                {
                    if (this._historyLine > 0)
                    {
                        this._currentLine = this._history[--this._historyLine];
                    }
                }

                // down key
                if (keyCode == 40)
                {
                    if (this._historyLine < this._history.Count - 1)
                    {
                        this._currentLine = this._history[++this._historyLine];
                    }
                    else
                    {
                        this._currentLine = string.Empty;
                    }
                }
                
                 
                 */
            }
        }

        /// <summary>
        /// Writes a line to the console with the options of evaluating it and clearing the current line.
        /// </summary>
        /// <param name="evaluate"></param>
        private void WriteLine(string line, bool evaluate, bool clearLine)
        {
            this._history.Add(line);

            if (evaluate)
            {
                Evaluate(line);
            }

            if (clearLine)
            {
                this._currentLine = string.Empty;
            }
        }       

        /// <summary>
        /// Renders the EuropaConsole.
        /// </summary>
        /// <param name="gameTime">The ingame time that has passed.</param>
        public void Draw(GameTime gameTime)
        {
            this._game.Drawer.Draw(this._background, new Rectangle(0, 300, 800, 300), Color.White);

            this._game.Drawer.DrawString(this._spriteFont, this._currentLine, new Vector2(5f, 570f), Color.White);

            for (int i = this._history.Count - 1; i >= 0; i--)
            {
                this._game.Drawer.DrawString(this._spriteFont, this._history[i], new Vector2(5f, 570f - ((this._history.Count - i) * 18f)), Color.White);
            }
        }

        #endregion

        #region Console control

        /// <summary>
        /// Evaluates a console command.
        /// </summary>
        /// <param name="line">The line to evaluate.</param>
        private void Evaluate(string line)
        {
            var parts = line.Split(' ');

            object item = this._game;
            for (int i = 0; i < parts.Length; i++)
            {
                if (item != null)
                    item = EvaluateItem(item, parts[i]);
                else
                    break;
            }

            if (item != null)
                WriteLine(item.ToString(), false, false);
        }

        /// <summary>
        /// Evaluates a single item in a command chain.
        /// </summary>
        /// <param name="item">The item to evaluate the command on.</param>
        /// <param name="command">The command to evaluate.</param>
        /// <returns></returns>
        private object EvaluateItem(object item, string command)
        {
            object returnItem = null;

            // try a property first
            var property = item.GetType().GetProperty(command);
            if (property != null)
            {
                try
                {
                    returnItem = property.GetGetMethod().Invoke(item, null);
                }
                catch (Exception ex)
                {
                    WriteLine("While invoking '" + command + "', got exception: " + ex.Message, false, false);
                }
            }
            else
            {
                // try a method
                var method = item.GetType().GetMethod(command);

                if (method != null)
                {
                    try
                    {
                        // more to do here with parameters
                        returnItem = method.Invoke(item, null);
                    }
                    catch (Exception ex)
                    {
                        WriteLine("While invoking '" + command + "', got exception: " + ex.Message, false, false);
                    }
                }
                else
                {
                    WriteLine("Unable to find method or property '" + command + "'", false, false);
                }
            }

            return returnItem;
        }

        #endregion

    }
}
