using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
//using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;


namespace Demo
{
    [TestClass]
    public class Class1
    {
        static IWebDriver driver;
        //private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [AssemblyInitialize]
        public static void SetupTest(TestContext context)
        {
            driver = new FirefoxDriver();
        }

        [AssemblyCleanup]
        public static void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            //Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheUntitledTest()
        {
            baseURL = "http://newtours.demoaut.com";
            driver.Navigate().GoToUrl(baseURL + "/");


            // check before access url
            Assert.AreEqual("Welcome: Mercury Tours", driver.Title);

            WebDriverWait webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            webDriverWait.Until((d) => { return d.FindElement(By.Name("userName")); });


            PageObject obj = new PageObject(driver);

            obj.loginTo("tutorial", "tutorial");

            //driver.FindElement(By.Name("userName")).Clear();
            //driver.FindElement(By.Name("userName")).SendKeys("tutorial");
            //driver.FindElement(By.Name("password")).Clear();
            //driver.FindElement(By.Name("password")).SendKeys("tutorial");
            //driver.FindElement(By.Name("login")).Click();

            // screen after login
            Assert.AreEqual("Find a Flight: Mercury Tours:", driver.Title);
            webDriverWait.Until((d) => { return d.FindElement(By.Name("fromPort")); });

            
            // select departing form 
            IWebElement element = driver.FindElement(By.XPath("//html/body/div[1]/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/form/table/tbody/tr[4]/td[2]/select"));
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByText("Paris");

            // select date 
            element = driver.FindElement(By.XPath("//html/body/div[1]/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/form/table/tbody/tr[5]/td[2]/select[2]"));
            selectElement = new SelectElement(element);
            selectElement.SelectByText("23");

            // select month on
            element = driver.FindElement(By.XPath("//html/body/div[1]/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/form/table/tbody/tr[5]/td[2]/select[1]"));
            selectElement = new SelectElement(element);
            selectElement.SelectByText("August");

            // select arriving in 
            element = driver.FindElement(By.XPath("//html/body/div[1]/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/form/table/tbody/tr[6]/td[2]/select"));
            selectElement = new SelectElement(element);
            selectElement.SelectByText("Portland");

            // Airline 
            element = driver.FindElement(By.XPath("//html/body/div[1]/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/form/table/tbody/tr[10]/td[2]/select"));
            selectElement = new SelectElement(element);
            selectElement.SelectByText("Pangea Airlines");

            // rad button
            element = driver.FindElement(By.XPath("//input[@name='servClass'][@value='Business']"));
            element.Click();

            Thread.Sleep(3000);

            // findFlights continue to screen 3 
            driver.FindElement(By.Name("findFlights")).Click();

            //Screen 3
            // Select a Flight: Mercury Tours
            Assert.AreEqual("Select a Flight: Mercury Tours", driver.Title);
            webDriverWait.Until((d) => { return d.FindElement(By.Name("outFlight")); });

            // in flight 
            ReadOnlyCollection<IWebElement> radOutFlight = driver.FindElements(By.XPath("//input[@name='outFlight']"));
            radOutFlight[3].Click();

            ReadOnlyCollection<IWebElement> radInFlight = driver.FindElements(By.XPath("//input[@name='inFlight']"));
            radInFlight[3].Click();

            Thread.Sleep(3000);

            driver.FindElement(By.Name("reserveFlights")).Click();

            // screen 4
            // title Book a Flight: Mercury Tours
            Assert.AreEqual("Book a Flight: Mercury Tours", driver.Title);
            webDriverWait.Until((d) => { return d.FindElement(By.Name("passFirst0")); });

            driver.FindElement(By.Name("passFirst0")).Clear();
            driver.FindElement(By.Name("passFirst0")).SendKeys("Dat");

            driver.FindElement(By.Name("passLast0")).Clear();
            driver.FindElement(By.Name("passLast0")).SendKeys("Ngo");

            //creditCard
            element = driver.FindElement(By.XPath("//html/body/div[1]/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/form/table/tbody/tr[6]/td/table/tbody/tr[2]/td[1]/select"));
            selectElement = new SelectElement(element);
            selectElement.SelectByText("Visa");

            driver.FindElement(By.Name("creditnumber")).Clear();
            driver.FindElement(By.Name("creditnumber")).SendKeys("123456789");

            // address 
            Assert.AreEqual("1325 Borregas Ave.", driver.FindElement(By.Name("billAddress1")).GetAttribute("value"));

            // compare dropdown
            element = driver.FindElement(By.Name("billCountry"));
            SelectElement select = new SelectElement(element);
            Assert.AreEqual("UNITED STATES", select.SelectedOption.GetAttribute("text"));

            // check checkbox
            Assert.AreEqual(false, driver.FindElement(By.XPath("//input[@name='ticketLess'][@value='checkbox'][@type='checkbox']")).Selected);

            Thread.Sleep(3000);

            // buyFlights
            driver.FindElement(By.Name("buyFlights")).Click();

            // Flight Confirmation: Mercury Tours
            Assert.AreEqual("Flight Confirmation: Mercury Tours", driver.Title);

            Thread.Sleep(5000);
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
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
