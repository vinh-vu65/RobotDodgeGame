using System;
using SplashKitSDK;

public abstract class Robot
{
    public double X {get; set;}
    public double Y {get; set;}
    public Color MainColor {get; set;}
    private Vector2D Velocity {get; set;}
    public int Width
    {
        get {return 50;}
    }
    public int Height
    {
        get {return 50;}
    }
    public Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(X + 25, Y + 25, 22.5);
        }
        
    }

    public abstract void Draw();
    public bool IsOffScreen(Window screen)
    {
            if ( X < -Width || X > screen.Width || Y < -Height || Y > screen.Height)
            {return true;}
            else 
            {return false;}
    }
    public Robot(Window gameWindow, Player player)
    {
        if (SplashKit.Rnd() < 0.5)
        {
            X = SplashKit.Rnd(gameWindow.Width);
            if (SplashKit.Rnd() < 0.5)
                Y = -Height;
            else 
                Y = gameWindow.Height;
        }
        else
        {
            Y = SplashKit.Rnd(gameWindow.Height);
            if (SplashKit.Rnd() < 0.5)
                X = -Width;
            else   
                X = gameWindow.Width;
        }
        const int SPEED = 4;
        Point2D fromPt = new Point2D()
        {
            X = X, Y = Y
        };
        Point2D toPt = new Point2D()
        {
            X = player.X, Y = player.Y
        };
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt,toPt));
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
        MainColor = Color.RandomRGB(200);
    }
    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }
    
}

public class Boxy : Robot
{
    public override void Draw()
    {
        double leftX, rightX, eyeY, mouthY;
        leftX = X + 12;
        rightX = X + 27;
        eyeY = Y + 10;
        mouthY = Y + 30;
        SplashKit.FillRectangle(Color.Gray, X, Y, 50, 50);
        SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
        SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
    }

    public Boxy(Window gameWindow, Player player) : base(gameWindow, player)
    {
        
    }
}

public class Roundy : Robot
{
    public Roundy(Window gameWindow, Player player) : base(gameWindow, player)
    {
        
    }
    public override void Draw()
    {
        double leftX, midX, rightX;
        double midY, eyeY, mouthY;

        leftX = X + 17;
        midX = X + 25;
        rightX = X + 33;

        midY = Y + 25;
        eyeY = Y + 20;
        mouthY = Y + 35;
        
        SplashKit.FillCircle(Color.White, midX, midY, 25);
        SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
        SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
        SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
        SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
        SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
    }
}

public class Triangly : Robot
{
    public Triangly(Window gameWindow, Player player) : base(gameWindow, player)
    {
        
    }

    public override void Draw()
    {
        double X1, X2, X3, leftX, rightX;
        double Y1, Y2, Y3, eyeY;

        X1 = X;
        Y1 = Y + 60;
        X2 = X + 30;
        Y2 = Y;
        X3 = X + 60;
        Y3 = Y1;
        eyeY = Y + 30;
        leftX = X + 23;
        rightX = X + 37;
        
        SplashKit.FillTriangle(Color.Gray, X1, Y1, X2, Y2, X3, Y3);
        SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
        SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
        SplashKit.FillRectangle(MainColor, leftX, eyeY + 12, 15, 5);
    }
}