using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwinGameSDK;

namespace MyGame
{
    class SpaceBlocks
    {
        private ControlBlock _player1;
        private ControlBlock _player2;
        private List<Block> _listOfOtherBlocks;
        private bool _gameStarted;
        private bool _gameOver;
        private int _highscore;
        private string _filename;
        public bool GameStarted
        {
            get
            {
                return _gameStarted;
            }
            set
            {
                _gameStarted = value;
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


        public SpaceBlocks()
        {

        }
        //Instantiates the Background blocks, and control blocks. Adds all the background blocks to a list  of blocks
        public void Start()
        {
            // _filename = "/Users/User/Desktop/Highscore.txt";
            _filename =  "./Resources/Highscore.txt";
            UpdateHighScore();
            _gameStarted = false;   
            _gameOver = false;
            _player1 = new ControlBlock(KeyCode.UpKey, KeyCode.DownKey, KeyCode.RightKey, KeyCode.LeftKey,100,50);
            _player2 = new ControlBlock(KeyCode.WKey, KeyCode.SKey, KeyCode.DKey, KeyCode.AKey, 700, 50);
            BackgroundBlock block1 = new BackgroundBlock(100, 300, Direction.Right, Color.MidnightBlue);
            BackgroundBlock block2 = new BackgroundBlock(200, 400, Direction.Left, Color.MediumOrchid);
            BackgroundBlock block3 = new BackgroundBlock(100, 500, Direction.Right, Color.MediumOrchid);
            BackgroundBlock block4 = new BackgroundBlock(100, 450, Direction.Right, Color.MidnightBlue);
            BackgroundBlock block5 = new BackgroundBlock(400, 500, Direction.Left, Color.MidnightBlue);
            BackgroundBlock block6 = new BackgroundBlock(100, 700, Direction.Left, Color.MediumOrchid);
            BackgroundBlock block7 = new BackgroundBlock(0, 0, Direction.Right, Color.MediumOrchid);
            BackgroundBlock block8 = new BackgroundBlock(120, 350, Direction.Right, Color.MidnightBlue);
            BackgroundBlock block9 = new BackgroundBlock(110, 710, Direction.Left, Color.MidnightBlue);
            BackgroundBlock block10 = new BackgroundBlock(30, 400, Direction.Left, Color.MediumOrchid);
            BackgroundBlock block11 = new BackgroundBlock(500, 500, Direction.Up, Color.MidnightBlue);
            BackgroundBlock block12 = new BackgroundBlock(300, 500, Direction.Up, Color.MediumOrchid);
            BackgroundBlock block13 = new BackgroundBlock(370, 200, Direction.Up, Color.MidnightBlue);
            BackgroundBlock block14 = new BackgroundBlock(40, 500, Direction.Up, Color.MediumOrchid);
            BackgroundBlock block15 = new BackgroundBlock(140, 300, Direction.Down, Color.MediumOrchid);
            BackgroundBlock block16 = new BackgroundBlock(200, 400, Direction.Down, Color.MidnightBlue);
            BackgroundBlock block17 = new BackgroundBlock(270, 400, Direction.Down, Color.MediumOrchid);
            BackgroundBlock block18 = new BackgroundBlock(300, 300, Direction.Down, Color.MidnightBlue);
            _listOfOtherBlocks = new List<Block>
            {
                block1,
                block2,
                block3,
                block4,
                block5,
                block6,
                block7,
                block8,
                block9,
                block10,
                block11,
                block12,
                block13,
                block14,
                block15,
                block16,
                block17,
                block18
            };
        }
        

        //Checks if game has started, Updates control blocks, background blocks and checks for collisions. Keeps track of scores and reseting the game.
        public void Update()
        {
            StartScreen();
            if (_gameStarted)
            {
                UpdateHighScore();
                _player1.Update();
                _player2.Update();
                foreach (Block i in _listOfOtherBlocks)
                {
                    i.Update();
                    _player1.Collide(i);
                    _player2.Collide(i);
                }
                if (!_player1.GameOver)
                {
                    _player1.Score++;
                }
                if(!_player2.GameOver)
                {
                    _player2.Score++;
                }
                if (_gameOver && SwinGame.KeyTyped(KeyCode.RKey))
                {
                    ClearHighScore(_filename);
                    UpdateHighScore();
                }
                if (_gameOver)
                {
                    if (_player1.Score > _highscore && _player1.Score > _player2.Score )
                    {
                        SaveHighScore(_filename, _player1.Score);
                    }
                    else if (_player2.Score > _highscore && _player2.Score > _player1.Score)
                    {
                        SaveHighScore(_filename, _player2.Score);
                    }
                        DisplayGameOver();
                }
                if ( SwinGame.KeyTyped(KeyCode.SpaceKey))
                {
                    _player1.GameOver = false;
                    _player2.GameOver = false;
                    _gameOver = false;
                }
                //Indicates the bottom
                SwinGame.FillRectangle(Color.OrangeRed, 0, SwinGame.ScreenHeight() - 25, SwinGame.ScreenWidth(), 100);
                DisplayScore();
                End();
            }
        }

        //Displays scores of players and highscore
        public void DisplayScore()
        { 
            SwinGame.DrawText("PLAYER 1 SCORE:" + _player1.Score, Color.GreenYellow, 0, 0);
            SwinGame.DrawText("PLAYER 2 SCORE:" + _player2.Score, Color.GreenYellow, 0, 30);
            SwinGame.DrawText("HIGHSCORE:" + _highscore, Color.GreenYellow, 0, 50);
        }

        //Displays scores of players, highscore and messages to user when game has ended
        public void DisplayGameOver()
        {
            if (_player1.Score == _highscore)
            {
                SwinGame.DrawText("PLAYER 1 BROKE THE RECORD: " + _player1.Score, Color.White, 350, 210);
            }
            else if (_player2.Score == _highscore)
            {
                SwinGame.DrawText("PLAYER 2 BROKE THE RECORD: " + _player2.Score, Color.White, 350, 210);
            }
            SwinGame.DrawText("PLAYER 1 SCORE:" + _player1.Score, Color.White, 350, 280);
            SwinGame.DrawText("PLAYER 2 SCORE:" + _player2.Score, Color.White, 350, 290);
            SwinGame.DrawText("HIGH SCORE:" + LoadHighScore(_filename), Color.White, 350, 260);
            SwinGame.DrawText("Press R to reset Highscore", Color.White, 350, 310);
            SwinGame.DrawText("Press SpaceBar to restart", Color.White, 350, 320);
        }
        
        //Checks if the player blocks have reached the "bottom" 
        public void End()
        {
            if ((_player1.PositionY + _player1.Height >= SwinGame.ScreenHeight() - 25))
            {
                _player1.End();
            }
            if(_player2.PositionY + _player2.Height >= SwinGame.ScreenHeight() - 25)
            {
                _player2.End();
            }
            if (_player1.GameOver && _player2.GameOver)
            {
                _gameOver = true;
            }
        }

        //Clears the previous highscore
        public void ClearHighScore(string filename)
        {
            SaveHighScore(filename, 0);
        }

        //Saves the highscore of the player 
        public void SaveHighScore(string filename, int highscore)
        {
            StreamWriter writer;
            writer = new StreamWriter(filename);
            writer.WriteLine(highscore);
            writer.Close();
        }

        //Loads saved highscores from a text file
        public string LoadHighScore(string filename)
        {
            StreamReader reader;
            reader = new StreamReader(filename);
            string result = reader.ReadLine();
            reader.Close();
            return result;
        }

        //Updates the highscore variable with the saved highscore
        public void UpdateHighScore()
        {
            string result = LoadHighScore(_filename);
            _highscore = Convert.ToInt32(result);
        }

        //Displays the start screen
        public void StartScreen()
        {
            if(!_gameStarted)
            {
                SwinGame.DrawText("You are a block in space surrounded by other blocks, falling towards the end.", Color.White, 100, 250);
                SwinGame.DrawText("You alone, have the ability to shift colours.", Color.White, 100, 275);
                SwinGame.DrawText("Blocks of your color will take you higher, making you smaller, ", Color.White, 100, 300);
                SwinGame.DrawText("while others make you larger.", Color.White, 100, 325);
                SwinGame.DrawText("Keep yourself afloat in space, for at the end, even the greatest of blocks ", Color.White, 50, 360);
                SwinGame.DrawText("would disappear into nothingness.",Color.White, 50, 370);
                SwinGame.DrawText("PLAYER 1:", Color.GreenYellow, 100, 420);
                SwinGame.DrawText("Up/Down Key => Change Colors", Color.GreenYellow, 100, 430);
                SwinGame.DrawText("Right key => Move Right",Color.GreenYellow, 100, 440);
                SwinGame.DrawText("Left key => Move Left", Color.GreenYellow, 100, 450);
                SwinGame.DrawText("<PRESS ANY KEY TO START>", Color.Yellow, 300, 470);
                SwinGame.DrawText("PLAYER 2:", Color.Green, 450, 420);
                SwinGame.DrawText("W/S Key => Change Colors", Color.Green, 450, 430);
                SwinGame.DrawText("D key => Move Right", Color.Green, 450, 440);
                SwinGame.DrawText("A key => Move Left", Color.Green, 450, 450);
            }
        }
    }
}
