using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong.iOS
{
    public class Ball : Sprite
    {
        private const float BALL_SPEED_X = 140f;
        
        private Paddle attachedToPaddle;
        private Vector2 originalPosition;



        /// <summary>
        /// Initializes a new instance of the <see cref="T:Pong.iOS.Ball"/> class.
        /// </summary>
        /// <param name="texture">Texture.</param>
        /// <param name="location">Location.</param>
        /// <param name="gameBoundaries">Game boundaries.</param>
        public Ball(Texture2D texture, Vector2 location,Rectangle gameBoundaries) : base(texture, location,gameBoundaries)
        {
            this.originalPosition = location;
        }

        public void Reset()
        {
            Location = new Vector2(originalPosition.X, originalPosition.Y);

        }
        /// <summary>
        /// Checks the bounds.
        /// </summary>
        protected override void CheckBounds()
        {
            if(Location.Y >= (gameBoundaries.Height - texture.Height) || Location.Y <= 0)
            {
                var ball = this;

                var newVelocity = new Vector2(Velocity.X, -Velocity.Y);
                Velocity = newVelocity;


            }
        }
        /// <summary>
        /// Update the specified gameTime and gameObjects.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="gameTime">Game time.</param>
        /// <param name="gameObjects">Game objects.</param>
        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            var touchState = TouchPanel.GetState();

            foreach (var touch in touchState)
            {
                //Fire the ball
                if(touch.State != TouchLocationState.Released && attachedToPaddle != null)
                {
                    ///<summary>
                    /// Ball Reflects off of walls
                    /// </summary>
                    if (touch.Position.Y < (TouchPanel.DisplayWidth / 2))
                    {
                        var newVelocity = new Vector2(BALL_SPEED_X, attachedToPaddle.Velocity.Y*1.2f);
                        Velocity = newVelocity;
                        attachedToPaddle = null;
                    }         
                }                           
            }

            if (attachedToPaddle != null)
            {
                Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
                Location.Y = attachedToPaddle.Location.Y;
            }
            else
            {
                ///<summary>
                /// Classification for the collision of the Ball with a game paddle
                /// </summary>
                if(BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox) || BoundingBox.Intersects(gameObjects.ComputerPaddle.BoundingBox))
                {
                    Velocity = new Vector2(-Velocity.X, Velocity.Y);
                }
            }
            base.Update(gameTime,gameObjects);
        }
        /// <summary>
        /// Attachs to.
        /// </summary>
        /// <param name="paddle">Paddle.</param>
        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
        } 
    }
}