using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
namespace Balashowa.Lexer.Tests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void toRecognozeIntFirstTest()
        {
            DataProvider dataProvider = new DataProvider();
            Automation automation = dataProvider.GetAutomation("intAutomation.txt");
            int skip = 10;
            string inputString = "vdjvbjwbvj1234jwbvwljbjwl";
            var expactedPair = new KeyValuePair<bool, int>(true, 4);
            var expectedString = "1234";
            var resultPair = automation.toRecognize(inputString, skip);
            Assert.AreEqual(expactedPair, resultPair);
            var resultString = inputString.Substring(skip, resultPair.Value);
            Assert.AreEqual(expectedString, resultString);
        }
        [TestMethod]
        public void toRecognozeIntSecondTest()
        {
            DataProvider dataProvider = new DataProvider();
            Automation automation = dataProvider.GetAutomation("intAutomation.txt");
            int skip = 0;
            string inputString = "+f1234567678557";
            var expactedPair = new KeyValuePair<bool, int>(false, 0);
            var resultPair = automation.toRecognize(inputString, skip);
            Assert.AreEqual(expactedPair, resultPair);
        }
        [TestMethod]
        public void toRecognizeRealFirstTest()
        {
            DataProvider dataProvider = new DataProvider();
            Automation automation = dataProvider.GetAutomation("realAutomation.txt");
            string inputText = "klsdkvnsv-2.245e12ihiil";
            int skip = 9;
            var expectedPair = new KeyValuePair<bool, int>(true, 9);
            var expectedString = "-2.245e12";
            var resultPair = automation.toRecognize(inputText, skip);
            Assert.AreEqual(expectedPair, resultPair);
            Assert.AreEqual(expectedString, inputText.Substring(skip, resultPair.Value));
        }
        [TestMethod]
        public void toRecognizeRealSecondTest()
        {
            DataProvider dataProvider = new DataProvider();
            Automation automation = dataProvider.GetAutomation("realAutomation.txt");
            string inputText = "klsd0.00ihiil";
            int skip = 4;
            var expectedPair = new KeyValuePair<bool, int>(true, 4);
            var expectedString = "0.00";
            var resultPair = automation.toRecognize(inputText, skip);
            Assert.AreEqual(expectedPair, resultPair);
            Assert.AreEqual(expectedString, inputText.Substring(skip, resultPair.Value));
        }
        [TestMethod]
        public void toRecognizeBoolFirstTest()
        {
            DataProvider dataProvider = new DataProvider();
            Automation automation = dataProvider.GetAutomation("boolAutomation.txt");
            string inputText = "!!!!!!true";
            int skip = 0;
            var expectedPair = new KeyValuePair<bool, int>(true, 10);
            var expectedString = "!!!!!!true";
            var resultPair = automation.toRecognize(inputText, skip);
            Assert.AreEqual(expectedPair, resultPair);
            Assert.AreEqual(expectedString, inputText.Substring(skip, resultPair.Value));
        }
        [TestMethod]
        public void toRecognizeBoolSecondTest()
        {
            DataProvider dataProvider = new DataProvider();
            Automation automation = dataProvider.GetAutomation("boolAutomation.txt");
            string inputText = "False";
            int skip = 0;
            var expectedPair = new KeyValuePair<bool, int>(false, 0);
            var resultPair = automation.toRecognize(inputText, skip);
            Assert.AreEqual(expectedPair, resultPair);
        }
        [TestMethod]
        public void toRecognizeCodeFirstTest()
        {
            DataProvider dataProvider = new DataProvider();
            Lexer lexer = dataProvider.GetLexer();
            var expectedSet = new HashSet<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string> ("int", "keyword"),
                new KeyValuePair<string, string>(" ", "space"),
                new KeyValuePair<string, string>("a", "ID"),
                new KeyValuePair<string, string>("=", "operation"),
                new KeyValuePair<string, string>("123", "int")
            };
            bool expected = true;
            using (StreamReader streamReader = new StreamReader("sampleCode.txt"))
            {
                int skip = 0;
                var actuallySet = lexer.toRecognizeCode(streamReader.ReadToEnd(), ref skip);
                bool actually = actuallySet.SetEquals(expectedSet);
                Assert.AreEqual(expected, actually);
            }
        }
        [TestMethod]
        public void toRecognizeCodeSecondTest()
        {
            DataProvider dataProvider = new DataProvider();
            Lexer lexer = dataProvider.GetLexer();
            var expectedSet = new HashSet<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string> ("int", "keyword"),
                new KeyValuePair<string, string>(" ", "space"),
                new KeyValuePair<string, string>("a", "ID"),
                new KeyValuePair<string, string>("=", "operation"),
                new KeyValuePair<string, string>("123", "int"),
                new KeyValuePair<string, string>(";", "operation"),
                new KeyValuePair<string, string>(Environment.NewLine, "space"),
                new KeyValuePair<string, string>("double", "keyword"),
                new KeyValuePair<string, string>("b", "ID"),
                new KeyValuePair<string, string>("2.2e5", "real"),
                new KeyValuePair<string, string>("bool", "keyword"),
                new KeyValuePair<string, string>("break", "keyword"),
                new KeyValuePair<string, string>("false", "bool"),
                new KeyValuePair<string, string>(Environment.NewLine+Environment.NewLine, "space"),
                new KeyValuePair<string, string>("while", "keyword"),
                new KeyValuePair<string, string>("(", "operation"),
                new KeyValuePair<string, string>(">", "operation"),
                new KeyValuePair<string, string>("&&", "operation"),
                new KeyValuePair<string, string>(")", "operation"),
                new KeyValuePair<string, string>("{", "operation"),
                new KeyValuePair<string, string>(Environment.NewLine+ " " + " ", "space"),
                new KeyValuePair<string, string>("-", "operation"), 
                new KeyValuePair<string, string> ("if", "keyword"),
                new KeyValuePair<string, string>("<=", "operation"),
                new KeyValuePair<string, string>("0", "real"),
                new KeyValuePair<string, string>(Environment.NewLine + " " + " " + " " + " ", "space"),
                new KeyValuePair<string, string>("}", "operation"),
                new KeyValuePair<string, string>("/", "operation"),
                new KeyValuePair<string, string>("15.0", "real"),
            };
            bool expected = true;
            using (StreamReader streamReader = new StreamReader("code.txt"))
            {
                int skip = 0;
                var actuallySet = lexer.toRecognizeCode(streamReader.ReadToEnd(), ref skip);
                bool actually = actuallySet.SetEquals(expectedSet);
                Assert.AreEqual(expected, actually);
            }
        }
    }
}
