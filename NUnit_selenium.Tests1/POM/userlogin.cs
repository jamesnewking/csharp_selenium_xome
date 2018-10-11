using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_selenium.Tests1.POM
{
    class Userlogin
    {
        public void ClickLoginButton(IWebDriver driver)
        {
            var loginButtonXpath = "//*[@id='js-SiteHead']/div/nav/div[3]/div[2]/div[2]/a[1]";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(c => c.FindElement((By.XPath(loginButtonXpath))));
            IWebElement loginButton = driver.FindElement(By.XPath(loginButtonXpath));
            loginButton.Click();
        }

        public void Login(IWebDriver driver)
        {
            var LoginNameID = "security_loginname";
            var LoginPassID = "security_password";
            var SubmitButtonID = "submit-button";
            var CredentialUser = "jamesnewking@gmail.com";
            var CredentialPassword = "kZ9A2HQiDXmNq4e";
            var loginFrame = driver.FindElement(By.Id("login-iframe"));

            driver.SwitchTo().Frame(loginFrame);
            driver.FindElement(By.Id(LoginNameID)).SendKeys(CredentialUser);
            driver.FindElement(By.Id(LoginPassID)).SendKeys(CredentialPassword);
            driver.FindElement(By.Id(SubmitButtonID)).Click();

        }

        public string UserNameIs(IWebDriver driver)
        {
            var LoginNameXPath = "//*[@id='uniqid-NavSubmenu-button-14']/span/span";
            var actualLoginName = driver.FindElement(By.XPath(LoginNameXPath)).Text;
            return actualLoginName;
        }

        public string UserNameExpected()
        {
            var ExpectedName = "JAMES WANG";
            return ExpectedName;    
        }
    }
}
