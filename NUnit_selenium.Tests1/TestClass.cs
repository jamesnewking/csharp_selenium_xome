using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace NUnit_selenium_Xome.Tests1
{
    [TestFixture]
    //[Parallelizable]
    //[Ignore("skip this test")]
    public class SampleTest
    {
        IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void OpenURL()
        {
            driver.Manage().Window.Size = new Size(1280, 920);
            driver.Navigate().GoToUrl("https://www.xome.com");
            Console.WriteLine("Testing Xome");
            Assert.Pass("Opened browser to Xome");

        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            Console.WriteLine("Test finished");
            driver.Close();
            driver.Quit();
        }

        [Test, Order(1)]
        public void WebpageTitle()
        {
            var actualTitle = driver.Title;
            string expectedTitle = "Xome Retail | Real Estate & Homes For Sale";
            Console.WriteLine("title is: " + actualTitle);
            Assert.AreEqual(actualTitle, expectedTitle);
        }

        [Test, Order(2)]
        public void UserLogin()
        {
            this.driver.SwitchTo().DefaultContent();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until( c => c.FindElement((By.XPath("//*[@id='js-SiteHead']/div/nav/div[3]/div[2]/div[2]/a[1]"))));
            IWebElement loginButton = driver.FindElement(By.XPath("//*[@id='js-SiteHead']/div/nav/div[3]/div[2]/div[2]/a[1]"));
            loginButton.Click();

            var loginFrame = driver.FindElement(By.Id("login-iframe"));
            driver.SwitchTo().Frame(loginFrame);
            driver.FindElement(By.Id("security_loginname")).SendKeys("jamesnewking@gmail.com");
            driver.FindElement(By.Id("security_password")).SendKeys("kZ9A2HQiDXmNq4e");
            driver.FindElement(By.Id("submit-button")).Click();
            driver.SwitchTo().DefaultContent();
            var actualLoginName = driver.FindElement(By.XPath("//*[@id='uniqid-NavSubmenu-button-14']/span/span")).Text;
            Console.WriteLine("the login name is: " + actualLoginName);
            var expectedLoginName = "JAMES WANG";
            Assert.AreEqual(actualLoginName, expectedLoginName);
        }

        [Test, Order(3)]
        public void SearchHomes()
        {
            var citySearchID = "homepage-search-field";
            driver.FindElement(By.Id(citySearchID)).Clear();
            driver.FindElement(By.Id(citySearchID)).SendKeys("Irvine");
            driver.FindElement(By.ClassName("search-field-button")).Click();
            String actualCityName = driver.FindElement(By.XPath("//*[@id='location-criteria-list']/ul/li/span")).Text;
            Console.WriteLine("City name is : " + actualCityName);
            Console.WriteLine("Contains Irvine: " + actualCityName.Contains("Irvine"));
            Assert.True(actualCityName.Contains("Irvine"));
        }

        [Test, Order(4)]
        public void CheckEachProperty()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(c => c.FindElement((By.XPath("//*[@id='mapsearch-results-body']/div"))));
            var properties = driver.FindElements(By.XPath("//*[@id='mapsearch-results-body']/div"));
            Console.WriteLine("Number of properties on the page: " + properties.Count);
            for (int pInPage =1; pInPage <= properties.Count; pInPage++)
            {
                var propPix = true;
                driver.FindElement(By.XPath("//*[@id='mapsearch-results-body']/div[" + pInPage + "]")).Click();
                try
                {
                    WebDriverWait nextWait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
                    nextWait.Until(c => c.FindElement((By.XPath("//*[@id='ltslide-total']"))));
                }
                catch (Exception e)
                {
                    Console.WriteLine("no picture! "+ e);
                    propPix = false;
                }
                
                var picTotal = "no";
                if (propPix)
                {
                    picTotal = driver.FindElement(By.XPath("//*[@id='ltslide-total']")).Text;
                }
                
                var streetAddr = driver.FindElement(By.XPath("//*[@id='listingdetail-title-summary']/div[2]/div/h1")).Text;
                var cityAddr = driver.FindElement(By.XPath("//*[@id='listingdetail-title-summary']/div[2]/div/div/span[1]")).Text;
                var askingPrice = driver.FindElement(By.XPath("//*[@id='listingdetail-title-summary']/div[1]/div[2]/span[1]/span/span")).Text;
                Console.WriteLine("The property at " + streetAddr + " " + cityAddr + " has asking price of " + askingPrice + " with " + picTotal + " property pictures.");
                if (propPix)
                {
                    var picNum = Int32.Parse(picTotal);
                    if (picNum > 1)
                    {
                        for (int i = 0; i < picNum; i++)
                        {
                            driver.FindElement(By.XPath("//*[@id='gallery-photos-all']/div/div/div/a[2]/i")).Click();
                            if (i > 3)
                            {
                                i = picNum;//so we don't click through all of the pictures
                            }
                        }

                    }
                }
                driver.FindElement(By.XPath("//*[@id='top-navigation-v3-closer']/span")).Click();

                if (pInPage > 5)
                {
                    pInPage = properties.Count;//to reduce the number of properties to click through
                }
            }
            Assert.True(properties.Count > 5);
        }

        [Test, Order(5)]
        public void UserLogout()
        {
            driver.SwitchTo().DefaultContent();
            driver.FindElement(By.Id("uniqid-NavSubmenu-button-14")).Click();
            driver.FindElement(By.XPath("//*[@id='uniqid-NavSubmenu-dropdown-14']/ul/li[17]/a")).Click();
            var signInText = driver.FindElement(By.XPath("//*[@id='js-SiteHead']/div/nav/div[3]/div[2]/div[2]/a[1]")).Text;
            Assert.AreEqual(signInText, "SIGN IN");
        }

        public static void TakeScreenshot(string filename, IWebDriver driver)
        {
            try
            {
                Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
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
