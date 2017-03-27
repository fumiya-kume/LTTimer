using System;
using System.IO;
using Xamarin.UITest;

namespace LTTimer.UITest
{
    public static class Extension
    {
        public static void ExScreenShot(this IApp app, string Title)
        {
            var fileInfo = app.Screenshot(Title);

            Directory.CreateDirectory(fileInfo.DirectoryName + ".\\ScreenShot");

            if (File.Exists($".\\ScreenShot\\{Title}.png"))
            {
                File.Delete($".\\ScreenShot\\{Title}.png");
            }
            File.Move(fileInfo.FullName, $".\\ScreenShot\\{Title}.png");
        }
    }
}