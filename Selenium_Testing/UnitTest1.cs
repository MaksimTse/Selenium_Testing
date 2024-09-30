using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium_Testing
{
    [TestFixture]
    public class GoogleSearchTest1
    {
        private IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\Users\opilane\source\repos\Selenium_Testing\Selenium_Testing\driver\");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void SearchTest()
        {
            driver!.Navigate().GoToUrl("https://maksimtsepelevits22.thkit.ee/");

            System.Threading.Thread.Sleep(1000);

            var MenuToggElement = driver.FindElement(By.ClassName("menu-toggle"));
            MenuToggElement.Click();
            System.Threading.Thread.Sleep(1000);

            var headerElement = driver.FindElement(By.XPath("//h3[text()='JS tööd']"));
            headerElement.Click();
            System.Threading.Thread.Sleep(1000);

            var MuusikaAnkeetlink = driver.FindElement(By.LinkText("Muusika ankeet"));
            MuusikaAnkeetlink.Click();
            System.Threading.Thread.Sleep(1000);

            var inputField = driver.FindElement(By.Id("nimi"));
            inputField.SendKeys("Maksim");
            Assert.That(inputField.GetAttribute("value"), Is.EqualTo("Maksim"), "Error, name in the field is not equal to input.");
            System.Threading.Thread.Sleep(1000);

            var selectElement = new SelectElement(driver.FindElement(By.Id("elu")));
            selectElement.SelectByValue("Tallinn");

            var selectedOption = selectElement.SelectedOption;
            Assert.That(selectedOption.GetAttribute("value"), Is.EqualTo("Tallinn"), "Selected element is not equal to expected.");
            System.Threading.Thread.Sleep(1000);

            var positiveRadioButton = driver.FindElement(By.Id("positive"));
            positiveRadioButton.Click();
            System.Threading.Thread.Sleep(1000);

            Assert.IsTrue(positiveRadioButton.Selected, "Radio button 'Positive' is not selected.");

            var negativeRadioButton = driver.FindElement(By.Id("negative"));
            negativeRadioButton.Click();

            Assert.IsTrue(negativeRadioButton.Selected, "Radio button 'Negative' is not selected.");
            Assert.IsFalse(positiveRadioButton.Selected, "Radio button 'Positive' should not be selected anymore.");
            System.Threading.Thread.Sleep(1000);


            var rangeSlider = driver.FindElement(By.Id("exp"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].value = 3; arguments[0].dispatchEvent(new Event('change'));", rangeSlider);

            Assert.That(rangeSlider.GetAttribute("value"), Is.EqualTo("3"), "Slider is not set in right position.");
            System.Threading.Thread.Sleep(1000);

        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}
