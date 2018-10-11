using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit_selenium.Tests1.POM;

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
            var TestURL = "https://www.xome.com";
            driver.Manage().Window.Size = new Size(1280, 920);
            driver.Navigate().GoToUrl(TestURL);
            Console.WriteLine("Testing {0}", TestURL);
            Assert.Pass("Opened browser to {0}", TestURL);
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
            var PageTitle = new NUnit_selenium.Tests1.ActualPageTitle();
            var Actual = PageTitle.GetPageTitle(driver);
            var Expected = PageTitle.ExepectedPageTitle();
            Assert.AreEqual( Actual, Expected );
        }

        [Test, Order(2)]
        public void UserLogin()
        {
            this.driver.SwitchTo().DefaultContent();
            //var LoginUser = new NUnit_selenium.Tests1.POM.Userlogin();
            Userlogin LoginUser = new Userlogin();
            LoginUser.ClickLoginButton(driver);
            LoginUser.Login(driver);
            driver.SwitchTo().DefaultContent();
            var ActualLoginName = LoginUser.UserNameIs(driver);
            var ExpectedLoginName = LoginUser.UserNameExpected();
            Console.WriteLine("Expected login name to be: {0} and got {1}", ExpectedLoginName, ActualLoginName);
            Assert.AreEqual(ActualLoginName, ExpectedLoginName);
        }

        [Test, Order(3)]
        public void VerifyCityName()
        {
            var SearchForCity = "Irvine";
            var ActualCityName = string.Empty;
            Searchcity SearchCity = new Searchcity();
            SearchCity.SearchCityNamed(driver, SearchForCity);
            ActualCityName = SearchCity.ActualCityName(driver);
            Console.WriteLine("Searching for {0} and got {1}", SearchForCity, ActualCityName);
            Assert.True(ActualCityName.Contains(SearchForCity));
        }

        [Test, Order(4)]
        public void CheckEachProperty()
        {
            PropertyPic propertyPictures = new PropertyPic();
            var NumberOfProperties = propertyPictures.ClickPicturesOfProperties(driver, 3, 3);
            Assert.True(NumberOfProperties > 5);
        }

        [Test, Order(5)]
        public void UserLogout()
        {
            userlogout userLogout = new userlogout();
            userLogout.ClickLogOut(driver);
            var actualUserName = userLogout.UserName(driver);
            Console.WriteLine("the user name is now {0}", actualUserName);
            Assert.AreEqual(actualUserName, "SIGN IN");
        }
      
    }
}
