using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    class ControlBlock:Block
    {
        private int _positionX;
        private int _positionY;
        private int _startX;
        private int _startY;
        private int _width;
        private int _height;
        private int _score;
        private Color _color;
        private bool _hasCollided;
        private int _positionToReach;
        private KeyCode _up, _down, _right, _left;
        private bool _gameOver;
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

        public bool HasCollided
        {
            get
            {
                return _hasCollided;
            }
            set
            {
                _hasCollided = value;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }
        public bool GameOver
        {
            get
            {
                return _gameOver;
            }
            set
            {
                _gameOver = value;
            }
        }
        /// <summary>
        /// Creates Control Block
        /// </summary>
        /// <param name="up">Move Up key</param>
        /// <param name="down">Move Down key</param>
        /// <param name="right">Move Right key</param>
        /// <param name="left">Move Left key</param>
        /// <param name="positionX">X Coordinate of initial position</param>
        /// <param name="positionY">Y Coordinate of initial positioon</param>
        public ControlBlock(KeyCode up, KeyCode down, KeyCode right, KeyCode left,int positionX, int positionY)
        {
            _color = Color.MidnightBlue;
            _up = up;
            _down = down;
            _right = right;
            _left = left;
            _gameOver = false;
            _hasCollided = false;
            _startX = positionX;
            _startY = positionY;
            _positionX = _startX;
            _positionY = _startY;
            _width = 50;
            _height = 50;
            _positionToReach = 0;
            _score = 0;
        }

        //Updates the position of the block depending on the methods
        public override void Update()
        {
            Reset();
            Move();
            DownwardForce();
            CycleColor();
            Rebound();
            //Sets the
            if (_hasCollided)
            {
                if (_positionY == _positionToReach)
                {
                    HasCollided = false;
                }
            }
            Draw();
        }

        //Draws the control block
        public override void Draw()
        {
            SwinGame.FillRectangle(_color, _positionX, _positionY, _width, _height);
        }

        //Resets the control block to its default values when the block was instantiated
        public void Reset()
        {
            if (SwinGame.KeyTyped(KeyCode.SpaceKey))
            {
                _hasCollided = false;
                _gameOver = false;
                _score = 0;
                _color = Color.MidnightBlue;
                _positionX = _startX;
                _positionY = _startY;
                _width = 50;
                _height = 50;
                _positionToReach = 0;
            }
        }

        //Changes the block's appearance once it reaches the bottom of the screen
        public void End()
        {
            _gameOver = true;
            _color = Color.Black;
            SwinGame.FillRectangle(Color.Blue, _positionX, _positionY, _width / 2, _height / 2);
            SwinGame.FillRectangle(Color.Red, _positionX + _width / 2, _positionY, _width / 2, _height / 2);
            SwinGame.FillRectangle(Color.Yellow, _positionX, _positionY+ _height/2, _width / 2, _height / 2);
            SwinGame.FillRectangle(Color.White, _positionX + _width/2, _positionY+ _height/2, _width / 2, _height / 2);
        }

        //Checks if a key is pressed and moves the block accordingly
        public override void Move()
        {
            if((SwinGame.KeyDown(_right)) && (_positionX + _width < SwinGame.ScreenWidth()))
            {
                _positionX = _positionX + 10;
            }
            else if ((SwinGame.KeyDown(_left)) && (_positionX > 0))
            {
                _positionX = _positionX - 10;
            }
        }
        
        //Moves the block downwards, if the block has not collided with a background block of similar color
        public void DownwardForce()
        {
            if(!_hasCollided)
            {
                 _positionY++;
            }
        }

        //Checks if a key is pressed and changes the color accordingly
        public void CycleColor()
        {
            if(SwinGame.KeyTyped(_up) && (_color == Color.MidnightBlue))
            {
                _color = Color.MediumOrchid;
            }
            else if(SwinGame.KeyTyped(_up) && (_color == Color.MediumOrchid))
            {
                _color = Color. MidnightBlue;
            }
            else if(SwinGame.KeyTyped(_down) && (_color == Color.MediumOrchid))
            {
                _color = Color.MidnightBlue;
            }
            else if(SwinGame.KeyTyped(_down) && (_color == Color.MidnightBlue))
            {
                _color = Color.MediumOrchid;
            }
                  
        }

        //Checks if the control block has collided with a block passed as a parameter and
        //changes the size of the block depending on the color of the background block it has
        //collided with
        public void Collide(Block block)
        {

            if (_positionX < block.PositionX + block.Width && _positionX + _width > block.PositionX && _positionY < block.PositionY + block.Height && _height + _positionY > block.PositionY)
            {
                _positionToReach = _positionY - 50;
                if (_color == block.Color)
                {
                    _hasCollided = true;
                    _width--;
                    _height--;
                }
                else
                {
                    _hasCollided = false;
                    _width++;
                    _height++;
                }
            }
        }

        // If the control block has collided with another block, this moves the control block upwards from
        // the position at collision to a position a few units upwards
        public void Rebound()
        {
            if((_hasCollided) &&(_positionY > 0))
            {
                SwinGame.FillCircle(Color.Yellow, _positionX+ _width/2, _positionY+ _height/2,_width);
                    _positionY = _positionY - 1;  
            }
            else
            {
                _hasCollided = false;
            }
        }
    }
}
