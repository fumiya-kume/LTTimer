using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace LTTimer.UITest
{
    public class AppInitializer
    {
        public static string APKpath { get; set; } =
            @"C:\Users\gurag\AppData\Local\Xamarin\Mono for Android\Archives\2017-03-27\LTTimer.Droid 3-27-17 8.00 PM.apkarchive\LTTimer.Droid.apk";


        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile(APKpath)
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

