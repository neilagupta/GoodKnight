using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoodKnightDesktop;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _knightTexture;
    private String CURRENT_DIRECTORY = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/";
    private Rectangle _knightRectangle = new Rectangle(0, 0, 128, 128);
    private float _timer;
    private float _threshold;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _timer = 0;
        _threshold = 250;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        /* Texture2D.FromStream    SoundEffect.FromStream */
        using (var fileStream = new FileStream(CURRENT_DIRECTORY + "Content/Knight/Knight_1/Idle.png", FileMode.Open))
        {
            _knightTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Close();
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if (_timer < _threshold)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        else
        {
            _knightRectangle.X = (_knightRectangle.X + 128) % 512;
            _timer = 0;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_knightTexture, new Vector2(0, 0), _knightRectangle, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

