using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_selenium.Tests1.POM
{
    class Searchcity
    {
        public void SearchCityNamed(IWebDriver driver, string City)
        {

            var citySearchID = "homepage-search-field";
            var SearchButtonClass = "search-field-button";
            driver.FindElement(By.Id(citySearchID)).Clear();
            driver.FindElement(By.Id(citySearchID)).SendKeys(City);
            driver.FindElement(By.ClassName(SearchButtonClass)).Click();
            
            //Console.WriteLine("City name is : " + actualCityName);
            //Console.WriteLine("Contains Irvine: " + actualCityName.Contains("Irvine"));
        }

        public string ActualCityName(IWebDriver driver)
        {
            var CityNameXpath = "//*[@id='location-criteria-list']/ul/li/span";
            String CityName = driver.FindElement(By.XPath(CityNameXpath)).Text;
            return CityName;
        }
    }
}
