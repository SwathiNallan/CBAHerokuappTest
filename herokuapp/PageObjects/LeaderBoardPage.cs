using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace herokuapp.PageObjects
{
    public class LeaderBoardPage : BasePage
    {
        public LeaderBoardPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);

        #region page factory

        [FindsBy(How = How.Id, Using = "showData")]
        public IWebElement showData { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#showData th")]
        public List<IWebElement> tableth { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#showData tr")]
        public List<IWebElement> tablerows { get; set; }

        #endregion

        #region methods

        public User GetUser(string username)
        {
            User user = new User();
            var row = tablerows.Where(a => a.Text.Contains(username)).FirstOrDefault();
            var userIdIndex = tableth.IndexOf(tableth.Where(a => a.Text.Equals("user_id")).FirstOrDefault());
            user.user_id = int.Parse(row.FindElements(By.TagName("td")).ToList()[userIdIndex].Text);
            var usernameIndex = tableth.IndexOf(tableth.Where(a => a.Text.Equals("username")).FirstOrDefault());
            user.username = row.FindElements(By.TagName("td")).ToList()[usernameIndex].Text;
            var scoreIndex = tableth.IndexOf(tableth.Where(a => a.Text.Equals("score")).FirstOrDefault());
            user.score = int.Parse(row.FindElements(By.TagName("td")).ToList()[scoreIndex].Text);
            return user;
        }

        
        #endregion

    }
}
