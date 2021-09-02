using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace herokuapp.PageObjects
{
    public class CovidPage : BasePage
    {
        public CovidPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);

        #region page factory
       [FindsBy(How = How.CssSelector, Using = ".thumbnail")]
        public List<IWebElement> thumbnail { get; set; }


        #endregion

        #region methods

        public void ChooseBattle(string battleName)
        {
            var selectedBattlefield = thumbnail.Where(a => a.FindElement(By.CssSelector(".summary")).Text.Equals(battleName)).FirstOrDefault();
            selectedBattlefield.FindElement(By.CssSelector(".btn")).Click();
        }
        
        #endregion
    }
}
