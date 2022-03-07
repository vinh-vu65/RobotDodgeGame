using SplashKitSDK;
using System;

public class Player
{
    private Bitmap _playerBitmap;
    public double X {get; private set;}
    public double Y {get; private set;}
    private Window _gameWindow;
    public bool Quit {get; set;}
    
    public int Width
    {
        get
        {
            return _playerBitmap.Width;
        }
    }
    public int Height
    {
        get
        {
            return _playerBitmap.Height;
        }
    }
    public Player(Window gameWindow)
    {
        Quit = false;
        _gameWindow = gameWindow;
        _playerBitmap = new Bitmap("player", "Player.png");
        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Height) / 2;
    }

    public void Draw()
    {
        _gameWindow.DrawBitmap(_playerBitmap, X, Y);
    }

    public void HandleInput()
    {
        const int SPEED = 10;
        
        if (SplashKit.KeyDown(KeyCode.UpKey) || SplashKit.KeyDown(KeyCode.WKey))
        {
            Y -= SPEED;
        }
        if (SplashKit.KeyDown(KeyCode.DownKey) || SplashKit.KeyDown(KeyCode.SKey))
        {
            Y += SPEED;
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey) || SplashKit.KeyDown(KeyCode.AKey))
        {
            X -= SPEED;
        }
        if (SplashKit.KeyDown(KeyCode.RightKey) || SplashKit.KeyDown(KeyCode.DKey))
        {
            X += SPEED;
        }
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            Quit = true;
        }
    }

    public void StayOnWindow()
    {
        const int GAP = 10;
        int RightBorder = _gameWindow.Width - GAP - Width;
        int BottomBorder = _gameWindow.Height - GAP - Height;

        if (X < GAP) {X = GAP;}
        if (Y < GAP) {Y = GAP;}
        if (X > RightBorder) {X = RightBorder;}

        if (Y > BottomBorder)
        {
            Y = BottomBorder;
        }
    }
    public bool CollidedWith(Robot other)
    {   
        if (_playerBitmap.CircleCollision(X, Y, other.CollisionCircle) == true)
        {
            return true; 
        }
        else return false;
    }
}