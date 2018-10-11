using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_selenium.Tests1.POM
{
    class Screenshot
    {
        public void TakeScreenShot(IWebDriver driver, string filename)
        {
            try
            {
                OpenQA.Selenium.Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
                image.SaveAsFile(filename, ScreenshotImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail("Could not screencapture. Exception: " + e);
            }
        }
    }
}
