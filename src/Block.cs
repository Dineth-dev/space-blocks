using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    public abstract class Block
    {
        public abstract int PositionX
        {
            get;
        }

        public abstract int PositionY
        {
            get;
        }

        public abstract int Width
        {
            get;
            set;
        }

        public abstract int Height
        {
            get;
            set;
        }
         
        public abstract Color Color
        {
            get;
            set;
        }

        public abstract void Update();

        public abstract void Move();

        public abstract void Draw();
    }
}
