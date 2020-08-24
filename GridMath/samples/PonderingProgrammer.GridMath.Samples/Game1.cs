using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using PonderingProgrammer.GridMath.Shapes;

namespace PonderingProgrammer.GridMath.Samples
{
    public class Game1 : Game
    {
        private static readonly int _scale = 10;
        
        private int _time;
        private double _shapeDuration = 1000;
        private double _cycleDuration;
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _font;
        private Shapes.Demo _currentDemo = Shapes.Demo.Point;
        private Button _button;
        private IList<IGridShape> _shapes;
        private IList<ShapeSprite> _shapeSprites;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            _button = new Button(12, 12, "Click to switch demo", _font, SwitchDemo, _spriteBatch);
            SwitchDemo();
        }

        private void SwitchDemo()
        {
            var demos = (Shapes.Demo[]) Enum.GetValues(typeof(Shapes.Demo));
            var index = Array.IndexOf<Shapes.Demo>(demos, _currentDemo) + 1;
            _currentDemo = demos.Length==index ? demos[0] : demos[index];
            _shapes = Shapes.GetShapes(_currentDemo).ToList();
            _shapeSprites = _shapes.Select(s => new ShapeSprite(s, _scale, _graphics.GraphicsDevice)).ToArray();
            _cycleDuration = _shapeDuration * _shapes.Count;
            _time = 0;
        }

        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("Font");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _time += gameTime.ElapsedGameTime.Milliseconds;
            if (_time >= _cycleDuration) _time -= (int)_cycleDuration;
            
            _button.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            _button.Draw();

            var shapeIndex = (int)((_time / _cycleDuration) * _shapes.Count);
            _shapeSprites[shapeIndex].Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}