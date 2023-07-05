using System.Collections.Generic;
using System;
using SplashKitSDK;
namespace RobotDodge
{
    public class RobotDodge
    {
        private Player _player;
        private Window _gameWindow;
        private List<Robot> _robots = new List<Robot>();

        public bool _quit { get { return _player._quit; } }

        /* constructor of current class */
        public RobotDodge(Window _screen)
        {
            _gameWindow = _screen;
            _player = new Player(_gameWindow);
        }

        /* 
        ask player object to wait for user to press key, 
        also check if player have get out from screen
        */
        public void HandleInput()
        {
            _player.HandleInput();
            _player.StayOnWindow(_gameWindow);
        }

        /* draw everything on screen */
        public void Draw()
        {
            _gameWindow.Clear(Color.White);
            foreach (Robot robot in _robots)
            {
                robot.Draw();
            }
            _player.Draw();
            _gameWindow.Refresh(60);
        }

        // randomly create robots when there is no robot left.
        public void Update()
        {
            if (_robots.Count == 0)
            {
                int i = SplashKit.Rnd(1, 10);
                while (i > 0)
                {
                    _robots.Add(RandomRobot());
                    i -= 1;
                }
            }
            CheckCollisions();
            UpdateRobots();
        }

        /* create a robot */
        public Robot RandomRobot()
        {
            double i = SplashKit.Rnd();
            if (i < 0.33)
            {
                Robot bot = new Boxy(_gameWindow, _player);
                return bot;
            }
            else if (i < 0.66)
            {
                Robot bot = new Roundy(_gameWindow, _player);
                return bot;
            }
            else
            {
                Robot bot = new Micky(_gameWindow, _player);
                return bot;
            }
        }

        // elimiate robots that touch player or edge
        private void CheckCollisions()
        {
            List<Robot> _toRemove = new List<Robot>();
            foreach (Robot bot in _robots)
            {
                if (_player.CollidedWith(bot) is true)
                {
                    _toRemove.Add(bot);
                }
                else if (bot.IsOffscreen(_gameWindow) is true)
                {
                    _toRemove.Add(bot);
                }
            }
            foreach (Robot bot in _toRemove)
            {
                _robots.Remove(bot);
            }
        }

        // let robot trace player
        private void UpdateRobots()
        {
            foreach (Robot bot in _robots)
            {
                bot.Update(_player);
            }
        }
    }
}