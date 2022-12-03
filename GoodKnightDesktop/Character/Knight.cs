using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GoodKnightDesktop;

namespace GoodKnightDesktop.Character
{
	public class Knight
	{
        private Rectangle _knightRectangle = new Rectangle(0, 0, 64, 128);
        private Texture2D _knightTexture;
        private Vector2 _location;
        private float _timer;
        private Variation _variation;
        private KnightState _knightState;
        private SpriteEffects _spriteEffects;

        public enum Variation
        {
            ONE,
            TWO,
            THREE
        };

        private enum KnightState
        {
            IDLE,
            WALK,
            RUN,
            ATTACK_1,  //Slash 1
            ATTACK_2,  //Slash 2
            ATTACK_3,  //Thrust
            DEFEND,
            PROTECT,   //Defense Stance
            HURT,
            JUMP,
            RUN_ATTACK,
            DEAD
        }

        public Knight(Variation variation)
		{
            _timer = 0;
            _variation = variation;
            _knightState = KnightState.IDLE;
            _location = new Vector2(100, 0);
        }

        public void LoadContent(GraphicsDeviceManager graphics)
        {
            /* Texture2D.FromStream    SoundEffect.FromStream */
            using (var fileStream = new FileStream(Game1.CURRENT_DIRECTORY + "Content/Knight/Knight_" + ((int)_variation+1) + "/" + _knightState.ToString() + ".png", FileMode.Open))
            {
                _knightTexture = Texture2D.FromStream(graphics.GraphicsDevice, fileStream);
                fileStream.Close();
            }
        }

        private void resetAnimation()
        {
            _timer = 0;
            _knightRectangle.X = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (_knightState != KnightState.DEAD)
            {
                if (_knightState == KnightState.IDLE)
                {
                    _spriteEffects = SpriteEffects.None;
                    Animate(gameTime, 400, 128, 512, true);

                    if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.A)) {
                        resetAnimation();
                        if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                        {
                            _knightState = KnightState.RUN;
                        }
                        else
                        {
                            _knightState = KnightState.WALK;
                        }
                    }
                }
                if (_knightState == KnightState.WALK)
                {
                    Animate(gameTime, 100, 128, 1024, true);
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        _spriteEffects = SpriteEffects.FlipHorizontally;
                        _location.X -= 3;
                    } else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        _spriteEffects = SpriteEffects.None;
                        _location.X += 3;
                    }

                    if (Keyboard.GetState().GetPressedKeyCount() == 0)
                    {
                        resetAnimation();
                        _knightState = KnightState.IDLE;
                    }
                }
                if (_knightState == KnightState.RUN)
                {
                    Animate(gameTime, 100, 128, 896, true);
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        _spriteEffects = SpriteEffects.FlipHorizontally;
                        _location.X -= 5;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        _spriteEffects = SpriteEffects.None;
                        _location.X += 5;
                    }

                    if (Keyboard.GetState().GetPressedKeyCount() == 0)
                    {
                        resetAnimation();
                        _knightState = KnightState.IDLE;
                    }
                }
            }
        }

        private bool Animate(GameTime gameTime, float threshold, int spriteFrameIncrement, int spriteFrameMaxOffset, bool looped = false)
        {
            if (_timer < threshold)
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                _knightRectangle.X = (_knightRectangle.X + spriteFrameIncrement) % spriteFrameMaxOffset;

                if (_knightRectangle.X == 0 && !looped)
                {
                    return false;
                }
                _timer = 0;
            }
            return true;
        }

        public void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            LoadContent(graphics);
            spriteBatch.Begin();
            spriteBatch.Draw(_knightTexture, _location, _knightRectangle, Color.White, 0f, Vector2.Zero, Vector2.One, _spriteEffects, 0f);
            spriteBatch.End();
        }
    }
}

