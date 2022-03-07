using System;
using SplashKitSDK;
using System.Collections.Generic;

public class Bullet
{
    private Player _player;
    private double X { get; set; }
    private double Y { get; set; }
    private Vector2D Velocity { get; set; }

    private Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(X, Y, 10);
        }
    }


    public Bullet(Player player, Point2D position)
    {
        const int SPEED = 20;
        _player = player;
        X = _player.X + (_player.Width / 2);
        Y = _player.Y + (_player.Height / 2);
        

        Point2D fromPt = new Point2D()
        {
            X = X, Y = Y
        };
        Point2D toPt = SplashKit.MousePosition();
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt,toPt));
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }

    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }
    
    public void Draw()
    {
        SplashKit.FillCircle(Color.Black, X, Y, 10);
    }

    public bool CollidedWith(Robot bot)
    {
        if (SplashKit.CirclesIntersect(CollisionCircle, bot.CollisionCircle))
        {
            return true;
        }
        else return false;
    }
}
