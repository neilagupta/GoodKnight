using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GoodKnightDesktop.Character;

namespace GoodKnightDesktop;

public class Game1 : Game
{
    public static readonly String CURRENT_DIRECTORY = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/";

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Knight playerOne;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        playerOne = new Knight(Knight.Variation.ONE);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        playerOne.LoadContent(_graphics);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        playerOne.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        playerOne.Draw(gameTime, _graphics, _spriteBatch);

        base.Draw(gameTime);
    }
}

