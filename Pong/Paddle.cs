using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong.iOS
{
    public enum PlayerTypes
    {
        HUMAN,COMPUTER 
    }
    public class Paddle : Sprite
    {
        private const float PADDLE_SPEED = 400f;
        private readonly PlayerTypes playerTypes;



        /// <summary>
        /// Initializes a new instance of the <see cref="T:Pong.iOS.Paddle"/> class.
        /// </summary>
        /// <param name="texture">Texture.</param>
        /// <param name="location">Location.</param>
        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds,PlayerTypes playerTypes) : base(texture, location,screenBounds)
        {
            this.playerTypes = playerTypes;
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
            if(playerTypes == PlayerTypes.COMPUTER)
            {
                var random = new Random();
                var reactionThreshold = random.Next(30, 130);

                //computer logic
                if(gameObjects.Ball.Location.Y + gameObjects.Ball.Heigth < Location.Y + reactionThreshold)
                {
                    Velocity = new Vector2(0, -PADDLE_SPEED);
                }
                if(gameObjects.Ball.Location.Y > Location.Y + Heigth + reactionThreshold)
                {
                    Velocity = new Vector2(0, PADDLE_SPEED);
                }
            }
            if (playerTypes == PlayerTypes.HUMAN)
            {
                foreach (var touch in touchState)
                {
                    if (touch.State != TouchLocationState.Released)
                    {
                        if (touch.Position.Y < (TouchPanel.DisplayWidth / 2))
                        {
                            Velocity = new Vector2(0, -PADDLE_SPEED);
                        }
                        if (touch.Position.Y > (TouchPanel.DisplayWidth / 2))
                        {
                            Velocity = new Vector2(0, PADDLE_SPEED);
                        }

                    }
                }
            }
            base.Update(gameTime,gameObjects);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, gameBoundaries.Height - texture.Height);
        }
    }
}