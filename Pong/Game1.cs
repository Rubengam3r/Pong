using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong.iOS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Paddle paddle;
        private Ball ball;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.SupportedOrientations =
                        DisplayOrientation.Portrait |
                        DisplayOrientation.LandscapeLeft |
                        DisplayOrientation.LandscapeRight;


            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;

            TouchPanel.EnabledGestures = GestureType.VerticalDrag; //Moves the paddel

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            paddle = new Paddle(Content.Load<Texture2D>("Paddle"), Vector2.Zero, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height));
            ball = new Ball(Content.Load<Texture2D>("Ball"), Vector2.Zero);
            ball.AttachTo(paddle);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed.
            // Exit() is obsolete on iOS
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)

            paddle.Update(gameTime);
            ball.Update(gameTime);

            // TODO: Add your update logic here            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            paddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

