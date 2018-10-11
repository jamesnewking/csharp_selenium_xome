using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_selenium.Tests1
{
    class ActualPageTitle
    {
        public string GetPageTitle(IWebDriver driver)
        {
            //Console.WriteLine("Webtitle!!!!");

            string actualTitle = driver.Title;
            
            //Console.WriteLine("title is: " + actualTitle);
            //Console.WriteLine("we expected {0}", expectedTitle);
            return actualTitle;
            //var tryThis = new NUnit_selenium.Tests1.TryingClass();
            //tryThis.Printout();
            //Assert.AreEqual(actualTitle, expectedTitle);
        }

        public string ExepectedPageTitle()
        {
            string expectedTitle = "Xome Retail | Real Estate & Homes For Sale";
            return expectedTitle;
        }
    }
}
