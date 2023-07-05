using System;
using SplashKitSDK;
namespace RobotDodge
{
    public class Player
    {
        private Bitmap _playerbitmap;
        public double _x { get; private set; }
        public double _y { get; private set; }
        public bool _quit { get; private set; }
        public int _hp { get; private set; }
        public double _speed { get; private set; }
        public int _width { get { return _playerbitmap.Width; } }
        public int _height { get { return _playerbitmap.Height; } }

        /* constructor of current class */
        public Player(Window gameWindow)
        {
            _playerbitmap = new Bitmap("Player", "./images/Player.PNG");
            _hp = 3;
            _speed = 5;
            _x = (gameWindow.Width - _width) / 2;
            _y = (gameWindow.Height - _height) / 2;
            _quit = false;
        }

        /* Draw player */
        public void Draw()
        {
            _playerbitmap.Draw(_x, _y);
        }

        /* 
        let user to input, w to up, s to down, 
        a to left, d to right, esc to quit 
        arrow keys are also avalible
        */
        public void HandleInput()
        {
            SplashKit.ProcessEvents();
            if (SplashKit.KeyDown(KeyCode.UpKey) || SplashKit.KeyDown(KeyCode.WKey))
                _y -= _speed;
            if (SplashKit.KeyDown(KeyCode.DownKey) || SplashKit.KeyDown(KeyCode.SKey))
                _y += _speed;
            if (SplashKit.KeyDown(KeyCode.LeftKey) || SplashKit.KeyDown(KeyCode.AKey))
                _x -= _speed;
            if (SplashKit.KeyDown(KeyCode.RightKey) || SplashKit.KeyDown(KeyCode.DKey))
                _x += _speed;
            if (SplashKit.KeyDown(KeyCode.EscapeKey))
                _quit = true;
        }
        /* let player stay on instead of get out from screen*/
        public void StayOnWindow(Window gameWindow)
        {
            const int GAP = 10;
            if (_x < GAP) _x = GAP;
            if (_y < GAP) _y = GAP;
            if (_x + _width > gameWindow.Width - GAP) _x = gameWindow.Width - _width - GAP;
            if (_y + _height > gameWindow.Height - GAP) _y = gameWindow.Height - _height - GAP;
        }
        /* check if player have reached a robot's hit box */
        public bool CollidedWith(Robot bot)
        {
            return _playerbitmap.CircleCollision(_x, _y, bot._collisionCircle);
        }
    }
}