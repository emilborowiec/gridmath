using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;

namespace PonderingProgrammer.GridMath.Samples
{
    public class Button
    {
        public Button(int x, int y, string text, SpriteFont font, Action onClick, SpriteBatch spriteBatch)
        {
            Text = text;
            _font = font;
            _onClick = onClick;
            _spriteBatch = spriteBatch;
            var (w, h) = _font.MeasureString(text);
            _rect = new Rectangle(x, y, (int) w, (int) h);
        }

        private readonly SpriteFont _font;
        private readonly Action _onClick;
        private readonly SpriteBatch _spriteBatch;
        private Rectangle _rect;
        private MouseState _oldState;

        public string Text { get; }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && _oldState.LeftButton == ButtonState.Released)
            {
                if (_rect.Contains(mouseState.Position))
                {
                    _onClick?.Invoke();
                }
            }

            _oldState = mouseState;
        }

        public void Draw(bool toggled = false)
        {
            _spriteBatch.FillRectangle(_rect, toggled ? Color.Yellow : Color.White);
            _spriteBatch.DrawString(_font, Text, _rect.Location.ToVector2(), Color.Black);
        }
    }
}