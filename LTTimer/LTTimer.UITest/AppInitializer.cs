﻿using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace LTTimer.UITest
{
    public class AppInitializer
    {
        public static string APKpath { get; set; } =
            @"C:\Users\gurag\Source\Repos\LTTimer\LTTimer\LTTimer\LTTimer.Droid\bin\Release\LTTimer.Droid-Signed.apk";


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

