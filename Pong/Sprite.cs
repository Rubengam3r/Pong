using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.iOS
{
    public abstract class Sprite
    {
        protected readonly Texture2D texture;

        public Vector2 Location;
        public int Width { get { return texture.Width; } }
        public int Heigth { get { return texture.Height; } }
        public Vector2 Velocity { get; protected set; }
        public Rectangle BoundingBox { get { return new Rectangle((int)Location.X, (int)Location.Y, Width, Heigth); }}

        protected readonly Rectangle gameBoundaries;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="texture">Texture.</param>
        /// <param name="location">Location.</param>
        public Sprite(Texture2D texture, Vector2 location,Rectangle gameBoundaries)
        {
            this.gameBoundaries = gameBoundaries;
            this.texture = texture;
            Location = location;
            Velocity = Vector2.Zero;
        }
        /// <summary>
        /// Draw the specified spriteBatch.
        /// </summary>
        /// <returns>The draw.</returns>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, Color.White);
        }/// <summary>
        /// Update the specified gameTime and gameObjects.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="gameTime">Game time.</param>
        /// <param name="gameObjects">Game objects.</param>
        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            CheckBounds();
        }

        protected abstract void CheckBounds();

    }
}