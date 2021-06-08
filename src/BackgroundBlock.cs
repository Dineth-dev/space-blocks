using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    class BackgroundBlock : Block
    {
        private Color _color;
        private int _width;
        private int _height;
        private int _positionX;
        private int _positionY;
        private int _rightPosition;
        private Direction _direction;
        public override int PositionX
        {
            get
            {
                return _positionX;
            }
        }

        public override int PositionY
        {
            get
            {
                return _positionY;
            }
        }

        public override int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public override int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public override Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        /// <summary>
        /// Creates background block
        /// </summary>
        /// <param name="positionX">X Coordinate of initial position</param>
        /// <param name="positionY">Y Coordinate of initial position</param>
        ///  <param name="direction">Direction of Movement</param>
        /// <param name="color">Color of block</param>
        public BackgroundBlock(int positionX, int positionY, Direction direction, Color color)
        {
            _direction = direction;
            _color = color;
            _width = 20;
            _height = 20;
            _positionX = positionX;
            _positionY = positionY;
            _rightPosition = positionX + 250;
        }

        //Draws the block with it's updated positions
        public override void Update()
        {
            Move();
            Draw();
        }

        //Draws the block
        public override void Draw()
        {
            SwinGame.FillRectangle(_color, _positionX, _positionY, _width, _height);
        }

        //Changes the position of the block in regards to the direction parameter passed
        public override void Move()
        {
            if (_direction == Direction.Up)
            {
                MoveUp();
            }
            else if (_direction == Direction.Down)
            {
                MoveDown();
            }
            else if (_direction == Direction.Left)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }

        //Moves the block upwards from the bottom of the screen
        public void MoveUp()
        {
            if (_positionX < _rightPosition)
            {
                _positionX = _positionX + SwinGame.Rnd(10);
            }
            else
            {
                _positionX = _positionX - SwinGame.Rnd(10);
            }
            _positionX = _positionX + SwinGame.Rnd(10);
            _positionY = _positionY - SwinGame.Rnd(10);
            OffScreen();
        }

        //Moves the block downwards from the top of the screen
        public void MoveDown()
        {
            if (_positionX < _rightPosition)
            {
                _positionX = _positionX + SwinGame.Rnd(10);
            }
            else
            {
                _positionX = _positionX - SwinGame.Rnd(10);
            }
            //helps make movement more random
            _positionX = _positionX + SwinGame.Rnd(10);
            _positionY = _positionY + SwinGame.Rnd(10);
            OffScreen();
        }

        //Moves the block from the right to the left of the screen
        public void MoveLeft()
        {
            _positionX = _positionX - SwinGame.Rnd(10);
            _positionY = _positionY + SwinGame.Rnd(10);
            OffScreen();
        }

        //Moves the block from left to the right of the screen
        public void MoveRight()
        {
            _positionX = _positionX + SwinGame.Rnd(10);
            _positionY = _positionY - SwinGame.Rnd(10);
            OffScreen();
        }

        //Prevents block from going off the screen
        public void OffScreen()
        {
            if (_positionX > SwinGame.ScreenWidth())
            {
                _positionX = 0;
            }
            else if (_positionX < 0)
            {
                _positionX = SwinGame.ScreenWidth();
            }
            if (_positionY > SwinGame.ScreenHeight())
            {
                _positionY = 0;
            }
            else if (_positionY < 0)
            {
                _positionY = SwinGame.ScreenHeight();
            }
        }

    }
}
