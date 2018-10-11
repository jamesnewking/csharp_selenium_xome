using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_selenium.Tests1.POM
{
    class userlogout
    {
        public void ClickLogOut(IWebDriver driver)
        {
            var TopMenuID = "uniqid-NavSubmenu-button-14";
            var LogoutDropDownXpath = "//*[@id='uniqid-NavSubmenu-dropdown-14']/ul/li[17]/a";
            driver.SwitchTo().DefaultContent();
            driver.FindElement(By.Id(TopMenuID)).Click();
            driver.FindElement(By.XPath(LogoutDropDownXpath)).Click();
            Console.WriteLine("User logged out");
            var signInText = driver.FindElement(By.XPath("//*[@id='js-SiteHead']/div/nav/div[3]/div[2]/div[2]/a[1]")).Text;
        }

        public string UserName(IWebDriver driver)
        {
            var UserNameXpath = "//*[@id='js-SiteHead']/div/nav/div[3]/div[2]/div[2]/a[1]";
            var signInText = driver.FindElement(By.XPath(UserNameXpath)).Text;
            return signInText;
        }
    }
}
