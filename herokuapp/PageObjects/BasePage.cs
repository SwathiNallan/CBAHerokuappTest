using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace herokuapp.PageObjects
{
    public class BasePage
    {
        public IWebDriver driver;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool isPageLoaded(string expectedURL)
        {
            return driver.Url.Equals(expectedURL);
        }

        public bool isPageLoaded()
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            return new WebDriverWait(driver, TimeSpan.FromSeconds(60)).
                Until(wait => jsExecutor.ExecuteScript("return document.readyState").ToString() == "complete");
        }

    }
}

