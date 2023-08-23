using System;
using SplashKitSDK;
namespace RobotDodge
{
    public abstract class Robot
    {
        public double _x { get; set; }
        public double _y { get; set; }
        public Vector2D _velocity { get; set; }
        public Color _mainColor { get; private set; }
        public int _width { get { return 50; } }
        public int _height { get { return 50; } }
        public Circle _collisionCircle { get; private set; }
        /* constructor of current class */
        public Robot(Window gameWindow, Player player)
        {
            if (SplashKit.Rnd() < 0.5)
            {
                _x = SplashKit.Rnd(gameWindow.Width);
                if (SplashKit.Rnd() < 0.5) _y = -_height;
                else _y = gameWindow.Height;
            }
            else
            {
                _y = SplashKit.Rnd(gameWindow.Height);
                if (SplashKit.Rnd() < 0.5) _x = -_width;
                else _x = gameWindow.Width;
            }

            _mainColor = Color.RandomRGB(200);

            const int _speed = 4;
            Point2D fromPt = new Point2D() { X = _x, Y = _y };
            Point2D toPt = new Point2D() { X = player._x, Y = player._y };
            Vector2D dir;
            dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
            _velocity = SplashKit.VectorMultiply(dir, _speed);
        }
        /* draw a robot */
        public abstract void Draw();
        public void Update(Player player)
        {
            _x += _velocity.X;
            _y += _velocity.Y;
            _collisionCircle = SplashKit.CircleAt(_x + 25, _y + 25, 25);
        }
        public Boolean IsOffscreen(Window gameWindow)
        {
            if (_x < -_width || _x > gameWindow.Width || 
            _y < -_height || _y > gameWindow.Height)
            {
                return true;
            }
            else return false;
        }
    }
	
	// Following class are child class of Robot, and Micky is the special robot.
	// Each robot have 33% to appear during gaming.
    public class Boxy : Robot
    {
        public Boxy(Window gameWindow, Player player) : base(gameWindow,player){}
        public override void Draw()
        {
            double leftX = _x + 12;
            double rightX = _x + 27;
            double eyeY = _y + 10;
            double mounthY = _y + 30;
            SplashKit.FillRectangle(Color.Gray, _x, _y, 50, 50);
            SplashKit.FillRectangle(_mainColor, leftX, eyeY, 10, 10);
            SplashKit.FillRectangle(_mainColor, rightX, eyeY, 10, 10);
            SplashKit.FillRectangle(_mainColor, leftX, mounthY, 25, 10);
            SplashKit.FillRectangle(_mainColor, leftX + 2, mounthY + 2, 21, 6);
        }
    }
    public class Roundy : Robot
    {
        public Roundy(Window gameWindow, Player player): base(gameWindow,player){}
        public override void Draw()
        {
            double leftX = _x + 17;
            double midX = _x + 25;
            double rightX = _x + 33;
            double midY = _y + 25;
            double eyeY = _y + 20;
            double mounthY = _y + 35;
            SplashKit.FillCircle(Color.White, midX, midY, 25);
            SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
            SplashKit.FillCircle(_mainColor, leftX, eyeY, 10);
            SplashKit.FillCircle(_mainColor, rightX, eyeY, 25);
            SplashKit.FillEllipse(Color.Gray, _x, eyeY, 50, 30);
            SplashKit.DrawLine(Color.Black, _x, mounthY, _x+50, _y+35);
        }
    }
    public class Micky : Robot
    {
        public Micky(Window gameWindow, Player player): base(gameWindow,player){}
        public override void Draw()
        {
            double leftEarX = _x;
            double RightEarX = _x + 33;
            double BodyX = _x + 16;
            double BodyY = _y + 18;
            double rightX = _x + 33;
            double midY = _y + 25;
            double eyeY = _y + 20;
            double mounthY = _y + 35;

            SplashKit.FillCircle(Color.Black, leftEarX, _y, 9);
            SplashKit.FillCircle(Color.Black, RightEarX, _y, 9);
            SplashKit.FillCircle(Color.Black, BodyX, BodyY, 16);
        }
    }
}