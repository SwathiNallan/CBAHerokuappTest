using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using herokuapp.ExtraHandlers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace herokuapp.PageObjects
{
    public class NewsPage : BasePage
    {
        public NewsPage(IWebDriver driver) : base(driver) => PageFactory.InitElements(driver, this);

        #region page factory

        [FindsBy(How = How.Id, Using = "introModal")]
        public List<IWebElement> introModal { get; set; }

        [FindsBy(How = How.Id, Using = "incorrectModal")]
        public List<IWebElement> incorrectModal { get; set; }

        [FindsBy(How = How.Id, Using = "correctModal")]
        public List<IWebElement> correctModal { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#progress #bar")]
        public IWebElement progressBar { get; set; }

        [FindsBy(How = How.CssSelector, Using = "article")]
        public IWebElement article { get; set; }
        #endregion

        #region methods

        public ModalDialog GetIntroModal()
        {
            return introModal.Count() > 0 ? new ModalDialog(introModal[0]) :
                throw new Exception("Failed to load intro modal"); ;
        }

        public ModalDialog GetInCorrectModal()
        {
            return incorrectModal.Count() > 0 ? new ModalDialog(incorrectModal[0]) :
                throw new Exception("Failed to load incorrect modal"); ;

        }

        public ModalDialog GetCorrectModal()
        {
            return correctModal.Count() > 0 ? new ModalDialog(correctModal[0]) :
                throw new Exception("Failed to load correct modal");
        }

        public bool GetProgressBarStatus()
        {
            return progressBar.GetAttribute("style").Contains("width: 100%");
        }

        public string GetArticle()
        {
            return article.FindElement(By.TagName("h3")).Text;
        }

        public void SelectAnswers(string question, string answer)
        {
            List<IWebElement> articleThumbmails = article.FindElement(By.Id("question")).Text.Equals(question) ?
        article.FindElements(By.CssSelector("h3~.thumbnail .caption a")).ToList() :
        throw new Exception("Failed to get articlethumbnails");
            articleThumbmails.Where(a => a.Text.Equals(answer)).FirstOrDefault().Click();
        }
        #endregion
    }
}
