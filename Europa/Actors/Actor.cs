using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Europa.Actors
{
    public abstract class Actor
    {

        #region Protected members

        /// <summary>
        /// The location of the actor.
        /// </summary>
        protected Vector2 _location;

        /// <summary>
        /// The velocity of the actor.
        /// </summary>
        protected Vector2 _velocity;

        /// <summary>
        /// The acceleration of the actor.
        /// </summary>
        protected Vector2 _acceleration;

        /// <summary>
        /// A reference to the game instance.
        /// </summary>
        protected EuropaGame _game;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the location of the actor.
        /// </summary>
        public Vector2 Location
        {
            get
            {
                return this._location;
            }
        }

        /// <summary>
        /// Gets the velocity of the actor.
        /// </summary>
        public Vector2 Velocity
        {
            get
            {
                return this._velocity;
            }
        }

        /// <summary>
        /// Gets the acceleration of the actor.
        /// </summary>
        public Vector2 Acceleration
        {
            get
            {
                return this._acceleration;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Instantiates a new actor.
        /// </summary>
        protected Actor(EuropaGame game)
        {
            this._game = game;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies a force to this actor acceleration.
        /// </summary>
        /// <param name="forceVector">The force vector to apply.</param>
        public virtual void ApplyForce(Vector2 forceVector)
        {
            this._acceleration += forceVector;
        }

        /// <summary>
        /// Applies an impulse to this actor's velocity.
        /// </summary>
        /// <param name="impulseVector">The impulse vector to apply.</param>
        public virtual void ApplyImpulse(Vector2 impulseVector)
        {
            this._velocity += impulseVector;
        }

        /// <summary>
        /// Updates this actor.
        /// </summary>
        /// <param name="dt">The delta time.</param>
        public virtual void Update(float dt)
        {
            this._velocity += (this._acceleration * dt);
            this._location += (this._velocity * dt);
        }

        /// <summary>
        /// Draws this actor to the screen.
        /// </summary>
        public abstract void Draw();

        #endregion

    }
}
