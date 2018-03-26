using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong.iOS
{
    public class Ball : Sprite
    {
        private Paddle attachedToPaddle;
        public Ball(Texture2D texture, Vector2 location) : base(texture, location)
        {

        }

        protected override void CheckBounds()
        {

        }
        public override void Update(GameTime gameTime)
        {
            var touchState = TouchPanel.GetState();

            foreach (var touch in touchState)
            {
                //do something with the ball  
            }

            Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
            Location.Y = attachedToPaddle.Location.Y;
            base.Update(gameTime);
        }

        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
        }
    }
}