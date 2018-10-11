using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_selenium.Tests1.POM
{
    class PropertyPic
    {
        public int ClickPicturesOfProperties(IWebDriver driver, int MaxProperties, int MaxPictures)
        {
            var PropertyFoundXPath = "//*[@id='mapsearch-results-body']/div";
            var PicTotalXpath = "//*[@id='ltslide-total']";
            var StreetAddrXpath = "//*[@id='listingdetail-title-summary']/div[2]/div/h1";
            var CityAddrXpath = "//*[@id='listingdetail-title-summary']/div[2]/div/div/span[1]";
            var PriceXpath = "//*[@id='listingdetail-title-summary']/div[1]/div[2]/span[1]/span/span";
            var NextPicXpath = "//*[@id='gallery-photos-all']/div/div/div/a[2]/i";
            var ClosePicXpath = "//*[@id='top-navigation-v3-closer']/span";

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(c => c.FindElement((By.XPath(PropertyFoundXPath))));
            var properties = driver.FindElements(By.XPath(PropertyFoundXPath));
            Console.WriteLine("Number of properties on the page: " + properties.Count);
            for (int pInPage = 1; pInPage <= properties.Count; pInPage++)
            {
                var HasPropPix = true;
                driver.FindElement(By.XPath("//*[@id='mapsearch-results-body']/div[" + pInPage + "]")).Click();
                try
                {
                    WebDriverWait nextWait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
                    nextWait.Until(c => c.FindElement((By.XPath("//*[@id='ltslide-total']"))));
                }
                catch (Exception e)
                {
                    Console.WriteLine("no picture! " + e);
                    HasPropPix = false;
                }

                var picTotal = "0";
                if (HasPropPix)
                {
                    picTotal = driver.FindElement(By.XPath(PicTotalXpath)).Text;
                } else
                {
                    picTotal = "0";
                }

                var streetAddr = driver.FindElement(By.XPath(StreetAddrXpath)).Text;
                var cityAddr = driver.FindElement(By.XPath(CityAddrXpath)).Text;
                var askingPrice = driver.FindElement(By.XPath(PriceXpath)).Text;
                Console.WriteLine("The property at " + streetAddr + " " + cityAddr + " has asking price of " + askingPrice + " with " + picTotal + " property pictures.");
                if (HasPropPix)
                {
                    var picNum = Int32.Parse(picTotal);
                    if (picNum > 1)
                    {
                        for (int i = 0; i < picNum; i++)
                        {
                            driver.FindElement(By.XPath(NextPicXpath)).Click();
                            if (i >= MaxPictures-2)
                            {
                                i = picNum;//so we don't click through all of the pictures
                            }
                        }

                    }
                }
                driver.FindElement(By.XPath(ClosePicXpath)).Click();

                if (pInPage >= MaxProperties)
                {
                    pInPage = properties.Count;//to reduce the number of properties to click through
                }
            }
            return properties.Count;
        }
    }
}
