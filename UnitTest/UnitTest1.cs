using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRSkipAsync;
using StackAsync;

namespace UnitTest
{
    [TestClass]
    public class StackAsync_UnitTest1
    {
        CS_StackAsync nstack;

        [TestMethod]
        public async void Num_TestMethod11()
        {
            nstack = new CS_StackAsync();

            #region 対象：数値処理１
            int num = 100;          // サンプル番号 100

            await nstack.PushAsync(num);      // Push(100)
            int ret = await nstack.PopAsync();

            Assert.AreEqual(100, ret, "ret = 100");
            #endregion
        }
    }

    [TestClass]
    public class StackAsync_UnitTest2
    {
        CS_StackAsync sstack;

        [TestMethod]
        public async void Str_TestMethod21()
        {
            sstack = new CS_StackAsync();

            #region 対象：数値処理１
            String word = "This is a Pen.";   // サンプルワード　"This is a Pen."

            await sstack.SPushAsync(word);      // SPush("This is a Pen.")
            String ret = await sstack.SPopAsync();

            Assert.AreEqual("This is a Pen.", ret, "ret = [This is a Pen.]");
            #endregion
        }
    }

}
