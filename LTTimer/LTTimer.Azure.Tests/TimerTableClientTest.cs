// <copyright file="TimerTableClientTest.cs">Copyright ©  2014</copyright>
using System;
using System.Threading.Tasks;
using LTTimer.Azure.DataModel;
using LTTimer.Azure.Model;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LTTimer.Azure.Model.Tests
{
    /// <summary>このクラスには、TimerTableClient に対するパラメーター化された単体テストが含まれています</summary>
    [PexClass(typeof(TimerTableClient))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class TimerTableClientTest
    {
        /// <summary>GetTimer(String) のテスト スタブ </summary>
        [PexMethod]
        public Task<TimerTable> GetTimerTest([PexAssumeUnderTest]TimerTableClient target, string key)
        {
            Task<TimerTable> result = target.GetTimer(key);
            return result;
            // TODO: アサーションを メソッド TimerTableClientTest.GetTimerTest(TimerTableClient, String) に追加します
        }

        /// <summary>StartTimer(String) のテスト スタブ </summary>
        [PexMethod]
        public void StartTimerTest([PexAssumeUnderTest]TimerTableClient target, string key)
        {
            target.StartTimer(key);
            // TODO: アサーションを メソッド TimerTableClientTest.StartTimerTest(TimerTableClient, String) に追加します
        }
    }
}
