//******************************************************************************
//
// Copyright (c) 2017 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using System.Drawing;
using Applitools.Selenium;

namespace CalculatorTest
{
    [TestClass]
    public class ScenarioStandard : CalculatorSession
    {
        private static WindowsElement header;
        private static WindowsElement calculatorResult;
        
        [TestMethod]
        public void Addition()
        {
            session.Manage().Window.Size = new Size(700, 700); //for responsive design testing...

            Eyes eyes = new Eyes();
            eyes.ApiKey = "your_applitools_key";

            try
            {
                eyes.Open(session, "Windows Calculator", "C# Test");
            }
            catch(Exception ex)
            {
                Console.WriteLine("ahhhhhhh");
                throw new ApplicationException("Exception: ", ex);
            }

            // Find the buttons by their names and click them in sequence to peform 1 + 7 = 8
            session.FindElementByName("One").Click();
            session.FindElementByName("Plus").Click();
            session.FindElementByName("Seven").Click();
            session.FindElementByName("Equals").Click();
            
            eyes.CheckWindow("Equals 8");
            eyes.Close();

            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch a Calculator window
            Setup(context);

            // Identify calculator mode by locating the header
            try
            {
                header = session.FindElementByAccessibilityId("Header");
            }
            catch
            {
                header = session.FindElementByAccessibilityId("ContentPresenter");
            }

            // Ensure that calculator is in standard mode
            if (!header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase))
            {
                session.FindElementByAccessibilityId("NavButton").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var splitViewPane = session.FindElementByClassName("SplitViewPane");
                splitViewPane.FindElementByName("Standard Calculator").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Assert.IsTrue(header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase));
            }

            // Locate the calculatorResult element
            calculatorResult = session.FindElementByAccessibilityId("CalculatorResults");
            Assert.IsNotNull(calculatorResult);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestInitialize]
        public void Clear()
        {
            session.FindElementByName("Clear").Click();
            Assert.AreEqual("0", GetCalculatorResultText());
        }

        private string GetCalculatorResultText()
        {
            return calculatorResult.Text.Replace("Display is", string.Empty).Trim();
        }
    }
}