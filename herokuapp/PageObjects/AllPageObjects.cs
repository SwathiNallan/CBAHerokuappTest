using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace herokuapp.PageObjects
{
   public class AllPageObjects
    {
        public static HomePage homePageObject;
        public static CovidPage covidPageObject;
        public static NewsPage newsPageObject;
        public static LeaderBoardPage leaderboardPageObject;

        public AllPageObjects(IWebDriver driver)
        {
            homePageObject = new HomePage(driver);
            covidPageObject = new CovidPage(driver);
            newsPageObject = new NewsPage(driver);
            leaderboardPageObject = new LeaderBoardPage(driver);
        }
    }
}
