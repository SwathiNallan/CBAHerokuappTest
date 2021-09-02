using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using herokuapp.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace herokuapp.Tests
{
    [TestClass]
    public class HomePageTests : Hooks
    {
        private void HandlecorrectModal()
        {
            var correctModal = AllPageObjects.newsPageObject.GetCorrectModal();
            Assert.AreEqual("That is correct!", correctModal.GetModalTitle(), "Failed to validate correct message");
            correctModal.ClickContinue();
        }

        [TestMethod]
        public void CreateUser()
        {
            AllPageObjects.homePageObject.EnterWarriorName(expecteduser.username);
            AllPageObjects.homePageObject.ClickCreateWarrior();
            Assert.AreEqual(String.Format("Start your journey {0}", expecteduser.username),
                AllPageObjects.homePageObject.GetStartYourJourney(), "Failed to validate create user");
        }

        [TestMethod]
        public void ValidateUseOnly10Characters()
        {
            AllPageObjects.homePageObject.EnterWarriorName("swathi");
            Assert.AreEqual("Only use 10 characters", AllPageObjects.homePageObject.GetPopUpText(), "Failed to validate use only 10 characters in username");

        }

        [TestMethod]
        public void ValidateCompleteChallengeAndLeaderboard()
        {
            CreateUser();
            string covidPageURl = ConfigurationManager.AppSettings["covidpage"].ToString();
            AllPageObjects.homePageObject.ClickStartYourJourney();
            Assert.IsTrue(AllPageObjects.covidPageObject.isPageLoaded(covidPageURl), "Failed to navigate to covid challenge page");
            AllPageObjects.covidPageObject.ChooseBattle("Are you sure you know how to keep safe?");

            // to handle start modal popup
            var introModal = AllPageObjects.newsPageObject.GetIntroModal();
            Assert.AreEqual("You are in a battlefield...", introModal.GetModalTitle(), "Failed to validate intro message");
            introModal.ClickStart();

            AllPageObjects.newsPageObject.SelectAnswers("You must travel to buy food and medical supplies?",
                "Use your superheroes Mask and sanitizer while traveling on public transport and clean your hands regularly.");

            // to handle correct modal popup
            HandlecorrectModal();

            AllPageObjects.newsPageObject.SelectAnswers("Your coworker Markus, is constantly coughing in the office, what do you do?",
               "Use your super hero sanitizer, keep a safe distance and advise they should keep 1.5 metres away from others.");

            // to handle correct modal popup
            HandlecorrectModal();

            AllPageObjects.newsPageObject.SelectAnswers("You notice a large group of people waiting in line next to each other at a restaurant, what do you do?",
               "Use your super hero sanitizer, keep a safe distance and advise they should keep 1.5 metres away from others.");

            HandlecorrectModal();

            string leaderboardURl = ConfigurationManager.AppSettings["leaderboardpage"].ToString();
            Assert.IsTrue(AllPageObjects.leaderboardPageObject.isPageLoaded(leaderboardURl), "Failed to navigate to leader board page");
            User actualuser = AllPageObjects.leaderboardPageObject.GetUser(expecteduser.username);
            Assert.IsTrue(actualuser.user_id > 0, "Failed to validate userid");
            Assert.AreEqual(expecteduser.username, actualuser.username, "Failed to validate username");
            Assert.IsTrue(actualuser.score >= 0, "Failed to validate score");
        }


        [TestMethod]
        public void ValidateGoHomeAndStartAgain()
        {
            CreateUser();
            string covidPageURl = ConfigurationManager.AppSettings["covidpage"].ToString();
            AllPageObjects.homePageObject.ClickStartYourJourney();
            Assert.IsTrue(AllPageObjects.covidPageObject.isPageLoaded(covidPageURl), "Failed to navigate to covid challenge page");
            AllPageObjects.covidPageObject.ChooseBattle("Are you sure you know how to keep safe?");

            // to handle start modal popup
            var introModal = AllPageObjects.newsPageObject.GetIntroModal();
            Assert.AreEqual("You are in a battlefield...", introModal.GetModalTitle(), "Failed to validate intro message");
            introModal.ClickStart();
            bool progressstatus = false;
            do
            {
                progressstatus = AllPageObjects.newsPageObject.GetProgressBarStatus();
                if (progressstatus)
                {
                    break;
                }
            } while (!progressstatus);

            // handle oh no incorrect modal poup

            var incorrectModal = AllPageObjects.newsPageObject.GetInCorrectModal();
            Assert.AreEqual("Oh no!", incorrectModal.GetModalTitle(), "Failed to validate incoorect modal message");
            incorrectModal.ClickGoHomeAndStartAgain();
            string homePageURL = ConfigurationManager.AppSettings["covidpage"].ToString();
            Assert.IsTrue(AllPageObjects.homePageObject.isPageLoaded(homePageURL), "Failed to navigate to home page");

        }

    }
}
