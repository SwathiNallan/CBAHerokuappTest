using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace herokuapp.PageObjects
{
   public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);

        #region page factory
        [FindsBy(How = How.Id, Using = "worrior_username")]
        public IWebElement worriorUsername { get; set; }

        [FindsBy(How = How.Id, Using = "warrior")]
        public IWebElement btnWarrior { get; set; }

        [FindsBy(How = How.Id, Using = "popup")]
        public List<IWebElement> popupText { get; set; }

        [FindsBy(How = How.Id, Using = "start")]
        public List<IWebElement> startYourJourney { get; set; }


        #endregion

        #region methods

        public void EnterWarriorName(string worriorName)
        {
            worriorUsername.SendKeys(worriorName);

        }

        public string GetPopUpText()
        {
            return popupText.Count() > 0 ? popupText[0].Text : null;
        }

        public void ClickCreateWarrior()
        {
            btnWarrior.Click();
        }


        public string GetStartYourJourney()
        {
            return startYourJourney.Count() > 0 ? startYourJourney[0].Text : null;
        }

        public void ClickStartYourJourney()
        {
            if (startYourJourney.Count() > 0)
            {
                startYourJourney[0].Click();
            }
        }
        #endregion
    }
}
