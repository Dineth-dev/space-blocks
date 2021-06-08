using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
             //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            SwinGame.ShowSwinGameSplashScreen();
            SpaceBlocks SpaceBlocks = new SpaceBlocks();
            //Instantiates game
            SpaceBlocks.Start();
            //Run the game loop 
            while (false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.Black);
                //Starts the game if any key is pressed
                if(SwinGame.AnyKeyPressed())
                {
                    SpaceBlocks.GameStarted = true;
                }
                //Updates the game 
                SpaceBlocks.Update();
                //Draw onto the screen
                SwinGame.RefreshScreen(60);

            }
        }
    }
}