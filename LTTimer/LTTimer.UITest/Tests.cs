using System;
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

            if (Directory.Exists(Directory.GetCurrentDirectory() + ".\\ScreenShot"))
            {
                Directory.Delete(Directory.GetCurrentDirectory() + ".\\ScreenShot", true);
            }
        }
        
        [TestCase("あいうえお")]
        [TestCase("abcd")]
        public void タイマーテスト(string datakey)
        {
            _app = AppInitializer.StartApp(_platform);

            _app.EnterText(query => query.Marked("DataKeyEditor"), datakey);
            _app.DismissKeyboard();

            _app.Tap(query => query.Marked("StartTimerButton"));
            
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
            _app = AppInitializer.StartApp(_platform);

            _app.SetSliderValue(x => x.Marked("FontSizeSlider"), fontSize);
            _app.ExScreenShot($"フォントサイズ{fontSize}への変更テスト");
        }

        [Test]
        public void 設定画面を開くテスト()
        {
            _app = AppInitializer.StartApp(_platform);

            _app.Tap(query => query.Marked("NavigateSettingButton"));
            _app.WaitForElement(query => query.Marked("SettingPage"));
            _app.ExScreenShot("設定を開いた画面");
        }

        [Test]
        public void 終了時に効果音がなるかどうかを設定を変更するテスト()
        {
            _app = AppInitializer.StartApp(_platform);

            _app.Tap(query => query.Marked("NavigateSettingButton"));

            _app.Tap(query => query.Marked("SwitchPlaySoundEffects"));
            _app.WaitForElement(query => query.Marked("SettingPage"));
            _app.ExScreenShot("効果音を無効にした設定画面");
        }

        [TestFixtureTearDown]
        public void TestFinishe()
        {
            var filePass = Directory.GetCurrentDirectory() + "\\ScreenShot";
            System.Diagnostics.Process.Start(filePass);
        }
    }
}

