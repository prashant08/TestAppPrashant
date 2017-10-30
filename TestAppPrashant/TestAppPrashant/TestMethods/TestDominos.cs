using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;
using TestCommon.CommonUtility;
using TestCommon.PageObjects;

namespace TestAppPrashant.TestMethods
{
    [TestClass]
    public class TestDominos : BaseClass
    {
        private TestContext testContext = null;
        public TestContext TestContext
        {
            get
            {
                return this.testContext;
            }
            set
            {
                this.testContext = value;
                base.Context = this.testContext;
            }
        }

        /// <summary>
        /// Scerario 1 : Find the “HAWAIIAN” pizza
        /// Verify that the kilojoules value is ‘5152kj^’
        /// </summary>
        [TestMethod]
        [TestCategory("Domions")]
        public void TestFindThePiZZAWithTitle()
        {
            var targetBrowser = (string)Context.Properties["targetUrlDominos"];
            _driver.Url = targetBrowser;
            _driver.Navigate();

            // Define Veariables of expected value
            var PizzaTitle = "HAWAIIAN";
            var ExpectedkjsValue = "5152kj^";
            var searchFlag = "title";

            //Create object of DominosPage
            var dominospage = new DominosPage(_driver);

            //Fetch the actual kilojoules value 
            var ActualpizzaDetails = dominospage.GetPizzaDetails(PizzaTitle, searchFlag);
            Console.WriteLine($"Actual pizza Kjs value: {ActualpizzaDetails.kilojoules}");

            //Verify that the kilojoules value is ‘5152kj^’
            Assert.AreEqual(ExpectedkjsValue.ToLower(), ActualpizzaDetails.kilojoules.ToLower(), $"kjsValue value of pizza did not match , expected value :{ExpectedkjsValue}, Actual value: {ActualpizzaDetails}");
        }


        /// <summary>
        /// Scerario 2 : 
        /// Find the pizza with kilojoules of ‘4456kj^’
        /// Verify the pizza name is ‘HAM & CHEESE’
        /// </summary>
        [TestMethod]
        [TestCategory("Domions")]
        public void TestFindThePiZZAWithkilojoules()
        {
            var targetBrowser = (string)Context.Properties["targetUrlDominos"];
            _driver.Url = targetBrowser;
            _driver.Navigate();

            // Define Veariables of expected value
            var ExpectedPizzaTitle = "Ham & Cheese";
            var kjsValue = "4456kJ^";
            var searchFlag = "kjsValue";

            //Create object of DominosPage
            var dominospage = new DominosPage(_driver);

            //Fetch the actual Title value 
            var ActualpizzaDetails = dominospage.GetPizzaDetails(kjsValue, searchFlag);
            Console.WriteLine($"Actual pizza title value: {ActualpizzaDetails.title}");

            //Verify that the kilojoules value is ‘5152kj^’
            Assert.AreEqual(ExpectedPizzaTitle.ToLower(), ActualpizzaDetails.title.ToLower(), $"Title value of pizza did not match , expected value :{ExpectedPizzaTitle}, Actual value: {ActualpizzaDetails.title}");
        }
    }
}
