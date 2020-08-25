using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PonderingProgrammer.GridMath.Shapes;

namespace PonderingProgrammer.GridMath.Samples
{
    public class Game1 : Game
    {
        private static readonly int _scale = 10;
        private static readonly GridCoordinatePair _center = new GridCoordinatePair(40, 30);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
        }

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _font;

        private string _currentShape = "Point";
        private IGridShape _shape;
        private ShapeSprite _shapeSprite;

        private Button _pointButton;
        private Button _rectButton;
        private Button _aaSegmentButton;
        private Button _segmentButton;
        private Button _circleButton;
        private Button _fanButton;

        private Button _radiusUpButton;
        private Button _radiusDownButton;
        private Button _nextDirectionButton;

        private Button[] _shapeButtons;
        private Dictionary<string, IGridShape> _shapes;

        private int _radius = 10;
        private Grid8Direction _direction8 = Grid8Direction.Right;
        private Grid4Direction _direction4 = Grid4Direction.Right;

        protected override void Initialize()
        {
            base.Initialize();

            _pointButton = new Button(12, 12, "Point", _font, () => SwitchShape("Point"), _spriteBatch);
            _rectButton = new Button(12, 42, "Rectangle", _font, () => SwitchShape("Rectangle"), _spriteBatch);
            _aaSegmentButton = new Button(12, 72, "AA Segment", _font, () => SwitchShape("AA Segment"), _spriteBatch);
            _segmentButton = new Button(12, 102, "Segment", _font, () => SwitchShape("Segment"), _spriteBatch);
            _circleButton = new Button(12, 132, "Circle", _font, () => SwitchShape("Circle"), _spriteBatch);
            _fanButton = new Button(12, 162, "Fan", _font, () => SwitchShape("Fan"), _spriteBatch);
            
            _radiusUpButton = new Button(250, 12, "Radius Up", _font, RadiusUp, _spriteBatch);
            _radiusDownButton = new Button(250, 42, "Radius Down", _font, RadiusDown, _spriteBatch);
            
            _nextDirectionButton = new Button(500, 12, "Direction change", _font, DirectionChange, _spriteBatch);

            _shapeButtons = new[]
            {
                _pointButton, _rectButton, _aaSegmentButton, _segmentButton, _circleButton, _fanButton,
            };
            _shapes = new Dictionary<string, IGridShape>
            {
                ["Point"] = new GridPoint(_center),
                ["Rectangle"] =
                    new GridRectangle(
                        GridBoundingBox.FromMinMax(
                            _center.X - _radius, _center.Y - _radius, _center.X + _radius, _center.Y + _radius)),
                ["AA Segment"] = new GridAASegment(_center, _center.Translation(0, _radius)),
                ["Segment"] = new GridSegment(_center, _center.Translation(10, 10)),
                ["Circle"] = new GridCircle(_center, _radius),
                ["Fan"] = new GridFan(_center, _radius, _direction8),
            };

            SwitchShape("Point");
        }

        private void RadiusUp()
        {
            if (_radius < 30) _radius++;
            UpdateShapeSprite();
        }

        public void RadiusDown()
        {
            if (_radius > 1) _radius--;
            UpdateShapeSprite();
        }

        public void DirectionChange()
        {
            _direction8 = Directions.Rotate(_direction8, new GridRotation(1));
            UpdateShapeSprite();
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
            {
                Exit();
            }

            foreach (var shapeButton in _shapeButtons) shapeButton.Update();
            _radiusUpButton.Update();
            _radiusDownButton.Update();
            _nextDirectionButton.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var shapeButton in _shapeButtons)
            {
                shapeButton.Draw(shapeButton.Text.Equals(_currentShape));
            }
            _radiusUpButton.Draw();
            _radiusDownButton.Draw();
            _nextDirectionButton.Draw();

            _shapeSprite.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SwitchShape(string shape)
        {
            _currentShape = shape;
            UpdateShapeSprite();
        }

        private void UpdateShapeSprite()
        {
            _shape = _shapes[_currentShape];
            if (_shape is GridRectangle r)
            {
                r.Width = _radius;
            }

            if (_shape is GridAASegment aaSegment)
            {
            }

            if (_shape is GridSegment s)
            {
            }

            if (_shape is GridCircle c)
            {
                c.Radius = _radius;
            }

            if (_shape is GridFan f)
            {
                f.Radius = _radius;
                f.Direction = _direction8;
            }

            _shapeSprite = new ShapeSprite(_shape, _scale, _graphics.GraphicsDevice);
        }
    }
}