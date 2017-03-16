﻿using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;

namespace LTTimer.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp _app;
        readonly Platform _platform;

        public Tests(Platform platform)
        {
            this._platform = platform;
        }
        
        [Test]
        public void BeforeRun()
        {
            _app = AppInitializer.StartApp(_platform);
        }
        
        [TestCase("あいうえお")]
        [TestCase("abcd")]
        public async void タイマーテスト(string datakey)
        {
            _app.EnterText(query => query.Marked("DataKeyEditor"), datakey);
            _app.DismissKeyboard();

            _app.Tap(query => query.Marked("StartTimerButton"));
            await Task.Delay(10000);
            _app.ExScreenShot($"{datakey} でタイマーが開始するかのテスト");
        }

        private static object[] _fontSizeList =
        {
            new object[] {-1},
            new object[] {100},
            new object[] {250},
            new object[] {500},
            new object[] {1000}
        };

        [TestCaseSource(nameof(_fontSizeList))]
        public void フォントサイズの変更テスト(int fontSize)
        {
            _app.SetSliderValue(x => x.Marked("FontSizeSlider"), fontSize);
            _app.ExScreenShot($"フォントサイズ{fontSize}への変更テスト");
        }

        [TestFixtureTearDown]
        public void TestFinishe()
        {
            var filePass = Directory.GetCurrentDirectory() + "\\ScreenShot";
            System.Diagnostics.Process.Start(filePass);
        }
    }
}

