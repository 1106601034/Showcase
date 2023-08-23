using System;
using SplashKitSDK;
namespace RobotDodge
{
    public class Program
    {
        public static void Main()
        {
            Window _gameWindow = new Window("RobotDodge", 1280, 720);
            RobotDodge game = new RobotDodge(_gameWindow);
        /* user will stay on game until they press esc or close the window */
            while (!game._quit && !_gameWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();
                game.HandleInput();
                game.Update();
                game.Draw();
            }
            _gameWindow.Close();
            _gameWindow = null;
        }
    }

}