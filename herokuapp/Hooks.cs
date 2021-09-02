using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using herokuapp.PageObjects;

namespace herokuapp
{
    public class User
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public int score { get; set; }
    }


    [TestClass]
    public class Hooks
    {
        protected IWebDriver driver { get; set; }
        protected User expecteduser { get; set; }
        public void setUser()
        {
            expecteduser = new User() { user_id = 0, username = "swathi1233", score = 0 };
        }

        [TestInitialize]
        public void TestInit()
        {
            string homepage = ConfigurationManager.AppSettings["homepage"].ToString();
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl(homepage);
            AllPageObjects allPageObjects = new AllPageObjects(driver);
            setUser();
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
            driver.Close();
        }
    }
}
