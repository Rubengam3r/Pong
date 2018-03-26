using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong.iOS
{
    public class Paddle : Sprite
    {
        private readonly Rectangle screenBounds;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Pong.iOS.Paddle"/> class.
        /// </summary>
        /// <param name="texture">Texture.</param>
        /// <param name="location">Location.</param>
        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds) : base(texture, location)
        {
            this.screenBounds = screenBounds;
        }



        public override void Update(GameTime gameTime)
        {
            var touchState = TouchPanel.GetState();

            foreach (var touch in touchState)
            {
                if (touch.State != TouchLocationState.Released)
                {
                    if (touch.Position.Y < (TouchPanel.DisplayWidth / 2))
                    {
                        velocity = new Vector2(0, -5.9f);
                    }
                    if (touch.Position.Y > (TouchPanel.DisplayWidth / 2))
                    {
                        velocity = new Vector2(0, 5.9f);
                    }

                }
            }
            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, screenBounds.Height - texture.Height);
        }
    }

    public abstract class Sprite
    {
        protected readonly Texture2D texture;
        public Vector2 Location;
        public int Width { get { return texture.Width; } }
        public int Heigth { get { return texture.Height; } }
        protected Vector2 velocity = Vector2.Zero;
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="texture">Texture.</param>
        /// <param name="location">Location.</param>
        public Sprite(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            Location = location;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, Color.White);
        }
        public virtual void Update(GameTime gameTime)
        {
            Location += velocity;
            CheckBounds();
        }

        protected abstract void CheckBounds();

    }
}