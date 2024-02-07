﻿using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    [TestFixture]
    public class RemoveItemFromCart
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
            verificationErrors = new StringBuilder();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheRemoveItemFromCartTest()
        {
            driver.Navigate().GoToUrl("https://www.demoblaze.com/");
            driver.FindElement(By.Id("login2")).Click();
            driver.FindElement(By.Id("loginusername")).Click();
            driver.FindElement(By.Id("loginusername")).Clear();
            driver.FindElement(By.Id("loginusername")).SendKeys("korisnickoime");
            driver.FindElement(By.Id("loginpassword")).Click();
            driver.FindElement(By.Id("loginpassword")).Clear();
            driver.FindElement(By.Id("loginpassword")).SendKeys("korisnickalozinka");
            driver.FindElement(By.XPath("//div[@id='logInModal']/div/div/div[3]/button[2]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Sony vaio i5")).Click();
            driver.FindElement(By.LinkText("Add to cart")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("Product added.", CloseAlertAndGetItsText());
            driver.FindElement(By.Id("cartur")).Click();
            driver.FindElement(By.LinkText("Delete")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/ html / body / nav / div / div / ul / li[1] / a")).Click();
        }
        private bool IsElementPresent(By element)
        {
            try
            {
                driver.FindElement(element);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
