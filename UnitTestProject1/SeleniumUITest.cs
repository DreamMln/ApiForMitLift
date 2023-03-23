using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MitLiftSeleniumTest
{
    [TestClass]
    public class SeleniumUITest
    {
        private static readonly string DriverDirectory = "C://webDrivers";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestIndex()
        {
            //string url = "http://127.0.0.1:5500/index.html";
            _driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");
            string title = _driver.Title;
            Assert.AreEqual("Cars", title);
            Thread.Sleep(1000);
            //OBS: Vi benytter os af Thread.Sleep så vi kan nå at se hvad testen gør og så hjemmesiden kan nå at loade før vi tjekker for hvad der er på siden.
            //Vi finder GivLift knappen og trykker for at navigere over til vores opret lift side
            _driver.FindElement(By.Id("GivLift")).Click();
            Thread.Sleep(1000);

            //Vi tjekker titlen på den nye side for at tjekke vi er kommet det rigtige sted hen
            string newTitle = _driver.Title;
            Assert.AreEqual("Opret Lift", newTitle);

            //Vi navigerer tilbage til vores Index side ved at klikke på logoet oppe i venstre hjørne i NavBaren
            _driver.FindElement(By.Id("Logo")).Click();
            Thread.Sleep(1000);

            //Vi navigerer til BestilLift siden hvor vi har en liste over alle vores biler
            _driver.FindElement(By.Id("BestilLift")).Click();
            Thread.Sleep(1000);

            //Vi navigerer tilbage til Index siden
            _driver.FindElement(By.Id("MitLift")).Click();
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void AddCarRideTest()
        {
            _driver.Navigate().GoToUrl("http://127.0.0.1:5500/Pages/addLift.html");
            string title = _driver.Title;
            Assert.AreEqual("Opret Lift", title);
            Thread.Sleep(1000);

            //Vi skriver vores data ind til at oprette et nyt lift
            //Her finder vi det første input felt og skriver vores ønskede pris ind
            WebElement prisElement = (WebElement)_driver.FindElement(By.Id("Pris"));
            prisElement.SendKeys(Keys.Backspace);
            prisElement.SendKeys("100");

            //Her finder vi vores input felt til dato og tidspunkt. Her skal vi dele vores tekst op i flere dele
            //da vi skal trykke tab for at komme over til tidspunkt.
            WebElement datoElement = (WebElement)_driver.FindElement(By.Id("Dato"));
            datoElement.SendKeys("24032023");
            datoElement.SendKeys(Keys.Tab);
            datoElement.SendKeys("1730");

            _driver.FindElement(By.Id("startDestination")).SendKeys("Roskilde");

            _driver.FindElement(By.Id("endDestination")).SendKeys("Holbæk");
            
            WebElement sædeElement = (WebElement)_driver.FindElement(By.Id("quantity"));
            sædeElement.SendKeys(Keys.Backspace);
            sædeElement.SendKeys("3");

            WebElement bilElement = (WebElement) _driver.FindElement(By.Id("vælgBil"));
            bilElement.SendKeys("O");
            
            Thread.Sleep(3000);

            _driver.FindElement(By.Id("opretBil")).Click();
            Thread.Sleep(5000);

            //Vi navigerer til BestilLift siden hvor vi har en liste over alle vores biler
            _driver.FindElement(By.Id("BestilLift")).Click();
            Thread.Sleep(5000);

            //Vi finder vores liste i vores Card Row og tæller listen for at kunne finde første til sidste element i listen
            ReadOnlyCollection<IWebElement> ListElement = _driver.FindElements(By.Id("Card"));
            Console.WriteLine(ListElement.Count());
            IWebElement lastElement = ListElement.ElementAt(ListElement.Count - 1);

            //Vi tjekker at det liste element vi hiver fat i indeholder hhv "Roskilde" og "Helsingør" forskellige steder i listen
            string lastElementText = lastElement.Text;
            Assert.IsTrue(lastElementText.Contains("Roskilde"));

                ReadOnlyCollection<IWebElement> CardListElement = _driver.FindElements(By.Id("CardInfo"));
                CardListElement.ElementAt(0).Click();
            
            Thread.Sleep(1000);

            _driver.Navigate().Back();

            Thread.Sleep(1000);

            Assert.IsTrue(_driver.Title == "Mine Lift");

            Thread.Sleep(5000);

            ReadOnlyCollection<IWebElement> CardElement = _driver.FindElements(By.Id("Card"));
            Console.WriteLine(CardElement.Count());
            IWebElement lastCardElement = CardElement.ElementAt(CardElement.Count - 1);


            lastCardElement.FindElements(By.Id("deleteButton")).ElementAt(CardElement.Count() - 1).Click();

            Thread.Sleep(2000);
            
            _driver.FindElement(By.Id("deleteRideModal")).Click();

            Thread.Sleep(5000);

            _driver.Navigate().Back();
        }
    }
}
