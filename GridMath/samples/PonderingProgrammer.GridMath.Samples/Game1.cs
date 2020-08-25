#region

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PonderingProgrammer.GridMath.Shapes;

#endregion

namespace PonderingProgrammer.GridMath.Samples
{
    public class Game1 : Game
    {
        private const int Scale = 10;
        private static readonly GridCoordinatePair Center = new GridCoordinatePair(40, 30);

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

        private DemoShape _currentShape = DemoShape.Point;
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
        private Dictionary<DemoShape, IGridShape> _shapes;

        private int _radius = 10;
        private Grid8Direction _direction8 = Grid8Direction.Right;
        private bool _rotateSegment;

        public void RadiusDown()
        {
            if (_radius > 1) _radius--;
            UpdateShapeSprite();
        }

        public void DirectionChange()
        {
            _direction8 = Directions.Rotate(_direction8, new GridRotation(1));
            _rotateSegment = true;
            UpdateShapeSprite();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _pointButton = new Button(12, 12, "Point", _font, () => SwitchShape(DemoShape.Point), _spriteBatch);
            _rectButton = new Button(12, 42, "Rectangle", _font, () => SwitchShape(DemoShape.Rectangle), _spriteBatch);
            _aaSegmentButton = new Button(12, 72, "AA Segment", _font, () => SwitchShape(DemoShape.AASegment), _spriteBatch);
            _segmentButton = new Button(12, 102, "Segment", _font, () => SwitchShape(DemoShape.Segment), _spriteBatch);
            _circleButton = new Button(12, 132, "Circle", _font, () => SwitchShape(DemoShape.Circle), _spriteBatch);
            _fanButton = new Button(12, 162, "Fan", _font, () => SwitchShape(DemoShape.Fan), _spriteBatch);

            _radiusUpButton = new Button(250, 12, "Radius Up", _font, RadiusUp, _spriteBatch);
            _radiusDownButton = new Button(250, 42, "Radius Down", _font, RadiusDown, _spriteBatch);

            _nextDirectionButton = new Button(500, 12, "Direction change", _font, DirectionChange, _spriteBatch);

            _shapeButtons = new[]
            {
                _pointButton, _rectButton, _aaSegmentButton, _segmentButton, _circleButton, _fanButton,
            };
            _shapes = new Dictionary<DemoShape, IGridShape>
            {
                [DemoShape.Point] = new GridPoint(Center),
                [DemoShape.Rectangle] =
                    new GridRectangle(
                        GridBoundingBox.FromMinMax(
                            Center.X - _radius, Center.Y - _radius, Center.X + _radius, Center.Y + _radius)),
                [DemoShape.AASegment] = new GridAASegment(Center, Center.Translation(0, _radius)),
                [DemoShape.Segment] = new GridSegment(Center, Center.Translation(10, 10)),
                [DemoShape.Circle] = new GridCircle(Center, _radius),
                [DemoShape.Fan] = new GridFan(Center, _radius, _direction8),
            };

            SwitchShape(DemoShape.Point);
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
                shapeButton.Draw(shapeButton.Text == Enum.GetName(typeof(DemoShape), _currentShape));
            }

            _radiusUpButton.Draw();
            _radiusDownButton.Draw();
            _nextDirectionButton.Draw();

            _shapeSprite.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RadiusUp()
        {
            if (_radius < 30) _radius++;
            UpdateShapeSprite();
        }

        private void SwitchShape(DemoShape demoShape)
        {
            _currentShape = demoShape;
            UpdateShapeSprite();
        }

        private void UpdateShapeSprite()
        {
            _shape = _shapes[_currentShape];
            switch (_shape)
            {
                case GridRectangle r:
                    r.Width = _radius;
                    break;
                case GridAASegment aaSegment when _rotateSegment:
                    aaSegment.Rotate(new GridRotation(1));
                    break;
                case GridSegment s when _rotateSegment:
                    s.Rotate(new GridRotation(1));
                    break;
                case GridCircle c:
                    c.Radius = _radius;
                    break;
                case GridFan f:
                    f.Radius = _radius;
                    f.Direction = _direction8;
                    break;
            }

            _rotateSegment = false;
            _shapeSprite = new ShapeSprite(_shape, Scale, _graphics.GraphicsDevice);
        }
    }
}