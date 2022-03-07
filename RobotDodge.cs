using System;
using SplashKitSDK;
using System.Collections.Generic;
using System.Linq;

public class RobotDodge
{
    private Bitmap _lifeHeart;
    private Player _player;
    private readonly Window _gameWindow;
    private List<Robot> _robots;
    private List<Robot> _removeRobots;
    private Timer _gameTimer;
    private List<Bitmap> _lives;
    private List<Bullet> _bullets = new List<Bullet>();
    private Bullet _bullet;
    private List<Bullet> _removeBullets;
    public bool Quit
    {
        get 
        {
            return _player.Quit;
        }
    }
    
    public RobotDodge(Window gameWindow)
    {
        _gameWindow = gameWindow;
        _player = new Player(gameWindow);
        _robots = new List<Robot>() {RandomRobot()};
        _gameTimer = new Timer("Game Timer");
        _gameTimer.Start();
        LoadLives();
    }
    public void HandleInput()
    {
        _player.HandleInput();
        _player.StayOnWindow();

        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            Point2D position = SplashKit.MousePosition();
            _bullet = new Bullet(_player, position);
            _bullets.Add(_bullet);
        }
    }

    public void Draw()
    {
        _gameWindow.Clear(Color.White);
        foreach(Robot newRobot in _robots)
        {
            newRobot.Draw();
        }

        foreach (Bullet bullets in _bullets)
        {
            bullets.Draw();
        }
        _player.Draw();
        DrawLives();
        SplashKit.DrawTextOnWindow(_gameWindow, $"Score: {_gameTimer.Ticks/1000}", Color.Black, 10, 50);
        _gameWindow.Refresh(60);
    }

    public void Update()
    {
        foreach(Robot newRobot in _robots)
        {
            newRobot.Update();
        }

        foreach (Bullet bullets in _bullets)
        {
            bullets.Update();
        }
        if (SplashKit.Rnd() < 0.03)
        {
            _robots.Add(RandomRobot());
        }
        CheckCollisions();
    }
    
    public Robot RandomRobot()
    {
        if (SplashKit.Rnd() < 0.33)
        {
            return new Boxy(_gameWindow, _player);
        } else if (SplashKit.Rnd() > 0.33 && SplashKit.Rnd() < 0.66)
        {
            return new Roundy(_gameWindow, _player);
        }
        else return new Triangly(_gameWindow, _player);
    }
    private void CheckCollisions()
    {
        _removeRobots = new List<Robot>();
        _removeBullets = new List<Bullet>();
        foreach(var removebot in _robots)
        {
            if ( _player.CollidedWith(removebot) || removebot.IsOffScreen(_gameWindow) == true)
            {
                _removeRobots.Add(removebot);
            }

            if (_bullets.Count >= 1 && _bullet.CollidedWith(removebot) == true)
            {
                _removeRobots.Add(removebot);
                _removeBullets.Add(_bullet);
            }
            if(_player.CollidedWith(removebot))
            {
                MinusLife();
            }
        }
        foreach(Robot removebot in _removeRobots)
        {
            if (_player.CollidedWith(removebot) || removebot.IsOffScreen(_gameWindow) == true)
            {
                _robots.Remove(removebot);
            }
            if (_bullets.Count >= 1 && _bullet.CollidedWith(removebot) == true)
            {
                _robots.Remove(removebot);
                _bullets.Remove(_bullet);
            }
        }
    }
    
    private void DrawLives()
    {
        int heartWidth = SplashKit.BitmapNamed("heart").Width;
        int heartGap = heartWidth + 5;
        
        
        for (int i = 0; i < _lives.Count; i++)
        {
            _gameWindow.DrawBitmap(_lives[i], 10 + heartGap * i, 10);
        }   
       
    }
    private void LoadLives()
    {
        _lifeHeart = new Bitmap("heart", "heart.png");
        _lives = new List<Bitmap>() {_lifeHeart, _lifeHeart, _lifeHeart, _lifeHeart, _lifeHeart};
    }
        
    private void MinusLife()
    {
        if(_lives.Any())
        {
            _lives.RemoveAt(_lives.Count-1);
        }
        if(_lives.Count == 0) 
        {
            _player.Quit = true;
        }   
    }
}