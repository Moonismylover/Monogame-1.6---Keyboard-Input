using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_1._6___Keyboard_Input
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D pacTexture; 
        Texture2D pacRight;
        Texture2D pacLeft;
        Texture2D pacUp;
        Texture2D pacDown;
        Texture2D pacSleep;

        Rectangle pacLocation;

        Vector2 pacSpeed;

        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            pacLocation = new Rectangle(10, 10, 75, 75);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            pacRight = Content.Load<Texture2D>("PacRight");
            pacLeft = Content.Load<Texture2D>("pacLeft");
            pacUp = Content.Load<Texture2D>("pacUp");
            pacDown = Content.Load<Texture2D>("pacDown");
            pacSleep = Content.Load<Texture2D>("pacSleep");

            pacTexture = pacSleep;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();
            pacSpeed.X = 0;
            pacSpeed.Y = 0;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacSpeed.Y -= 2;
                pacTexture = pacUp;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacSpeed.Y += 2;
                pacTexture = pacDown;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacSpeed.X -= 2;
                pacTexture = pacLeft;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacSpeed.X += 2;
                pacTexture = pacRight;
            }

            pacLocation.X += (int)pacSpeed.X;
            pacLocation.Y += (int)pacSpeed.Y;

            if (!keyboardState.IsKeyDown(Keys.Up) && !keyboardState.IsKeyDown(Keys.Right) && !keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.Down))
            {
                pacTexture = pacSleep;
            }

            if (pacLocation.X < 0)
                pacLocation.X = 0;

            if (pacLocation.Y < 0)
                pacLocation.Y = 0;

            if (pacLocation.Right > window.Width)
                pacLocation.X = window.Width - pacLocation.Width;

            if (pacLocation.Bottom > window.Height)
                pacLocation.Y = window.Height - pacLocation.Height;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(pacTexture, pacLocation, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
