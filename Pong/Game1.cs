using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;

namespace Pong.iOS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameObjects gameObjects;
        private Paddle computerPaddle;
        private Paddle playerPaddle;
        private Ball ball;
        private Score score;
       // private SoundEffect ballsong;
        private SoundEffect scoreSound;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Pong.iOS.Game1"/> class.
        /// </summary>
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
            TouchPanel.EnabledGestures = GestureType.DoubleTap;

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

            var gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            var paddleTexture = Content.Load<Texture2D>("Paddle");
            var computerPaddleLocation = new Vector2(gameBoundaries.Width - paddleTexture.Width, 0);

            playerPaddle = new Paddle(paddleTexture, Vector2.Zero, gameBoundaries,PlayerTypes.HUMAN);
            computerPaddle = new Paddle(paddleTexture, computerPaddleLocation,  gameBoundaries,PlayerTypes.COMPUTER);

            ball = new Ball(Content.Load<Texture2D>("Ball"), Vector2.Zero,gameBoundaries);
            ball.AttachTo(playerPaddle);

            score = new Score(Content.Load<SpriteFont>("Consolas78"),gameBoundaries);

           
            scoreSound = Content.Load<SoundEffect>("0831");
            scoreSound.Play();



            gameObjects = new GameObjects { PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle, Ball = ball,Score = score };
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

            playerPaddle.Update(gameTime,gameObjects);
            computerPaddle.Update(gameTime,gameObjects);
            ball.Update(gameTime,gameObjects);
            score.Update(gameTime, gameObjects);

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
            playerPaddle.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);
            score.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

