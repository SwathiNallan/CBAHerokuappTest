using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace herokuapp.ExtraHandlers
{
   public class ModalDialog
    {
        IWebElement modalPoup;

        public ModalDialog(IWebElement modalPoup)
        {
            this.modalPoup = modalPoup;
        }

        public string GetModalTitle()
        {
            return modalPoup.FindElement(By.Id("staticBackdropLabel")).Text;
        }

        public string GteModalBody()
        {
            return modalPoup.FindElement(By.CssSelector(".modal-body")).Text;
        }

        public void ClickStart()
        {
            modalPoup.FindElement(By.CssSelector(".modal-footer #start")).Click();
        }

        public void ClickGoHomeAndStartAgain()
        {
            modalPoup.FindElement(By.CssSelector(".modal-footer #close_modal_btn_2")).Click();
        }

        public void ClickContinue()
        {
            modalPoup.FindElement(By.Id("continue")).Click();
        }

    }
}
