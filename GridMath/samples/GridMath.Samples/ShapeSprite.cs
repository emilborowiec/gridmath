#region

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using GridMath.Shapes;

#endregion

namespace GridMath.Samples
{
    public class ShapeSprite
    {
        public ShapeSprite(IGridShape shape, int scale, GraphicsDevice gd)
        {
            _shape = shape;
            _scale = scale;
            UpdateTexture(gd);
        }

        private readonly IGridShape _shape;
        private readonly int _scale;
        private Texture2D _tex;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null) throw new ArgumentNullException(nameof(spriteBatch));
            
            var bb = _shape.BoundingBox;
            var bbRect = new Rectangle(bb.MinX * _scale, bb.MinY * _scale, bb.Width * _scale, bb.Height * _scale);
            spriteBatch.Draw(_tex, bbRect, Color.White);
        }

        /// <summary>
        ///     Expensive. Don't call in game loop.
        /// </summary>
        /// <param name="gd"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UpdateTexture(GraphicsDevice gd)
        {
            if (gd == null) throw new ArgumentNullException(nameof(gd));
            var bb = _shape.BoundingBox;
            var renderTarget = new RenderTarget2D(gd, bb.Width * _scale, bb.Height * _scale);
            var spriteBatch = new SpriteBatch(gd);
            gd.SetRenderTarget(renderTarget);
            gd.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach (var c in _shape.Interior)
            {
                spriteBatch.FillRectangle(
                    (c.X - bb.MinX) * _scale, (c.Y - bb.MinY) * _scale, _scale, _scale, Color.Aqua);
            }

            foreach (var c in _shape.Edge)
            {
                spriteBatch.FillRectangle(
                    (c.X - bb.MinX) * _scale, (c.Y - bb.MinY) * _scale, _scale, _scale, Color.Blue);
            }

            var bbRect = new Rectangle(0, 0, (bb.Width * _scale) - 1, (bb.Height * _scale) - 1);
            spriteBatch.DrawRectangle(bbRect, Color.Green);
            spriteBatch.End();
            gd.SetRenderTarget(null);
            spriteBatch.Dispose();
            _tex = renderTarget;
        }
    }
}