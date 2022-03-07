using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Game", 600, 600);
        RobotDodge RD = new RobotDodge(gameWindow);
        
        while ( ! gameWindow.CloseRequested && RD.Quit == false)
        {
            SplashKit.ProcessEvents();
            gameWindow.Clear(Color.White);
            RD.HandleInput();
            RD.Update();
            RD.Draw();
            gameWindow.Refresh(60);
        }
        gameWindow.Close();
        gameWindow = null;
    }
}
