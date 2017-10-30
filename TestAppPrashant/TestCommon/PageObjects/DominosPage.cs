using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;
using TestCommon.Model;
using HtmlAgilityPack;
using TestCommon.CommonUtility;

namespace TestCommon.PageObjects
{
    public class DominosPage
    {
        IWebDriver _driver;
        By _pageLoadedSelector = By.XPath("//img[@alt='SOCIAL_LINKS_FACEBOOK_TEXT']");

        public DominosPage(IWebDriver webDriver)
        {
            _driver = webDriver;
            this.WaitForLoad();
            PageFactory.InitElements(webDriver, this);
        }

        public void WaitForLoad()
        {
            this._driver.WaitForAjaxLoad();
            CommonUtils.WaitForElementIsVisible(this._driver, _pageLoadedSelector);
        }

        [FindsBy(How = How.CssSelector, Using = "pizzamenus.content")]
        public IWebElement PizzaMenus { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='background']//following::span[@id='close']")]
        public IList<IWebElement> OfferPopup { get; set; }

        By _pizzaMenus = By.CssSelector("pizzamenus.content");


        /// Get Pizza Details with Pizza Title and KjsValue
        /// </summary>
        /// <param name="_driver"></param>
        /// <param name="pizzaTitle">Pizza Name</param>
        /// <param name="searchFlag">Title or KjsValue</param>
        /// <returns></returns>
        public PizzaDetails GetPizzaDetails(string searchedValue, string searchFlag)
        {
            PizzaDetails foundData = new PizzaDetails();
            this._driver.WaitForAjaxLoad();
            CommonUtils.WaitForElementIsVisible(_driver, _pizzaMenus);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
            var gridHTML = (string)jse.ExecuteScript("return arguments[0].outerHTML;", this.PizzaMenus);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(gridHTML);
            var navigator = doc.CreateNavigator();

            var nodeIterator = navigator.Select("//div[@class='product-container']");
            while (nodeIterator.MoveNext())
            {
                var row = nodeIterator.Current;
                string title = string.Empty;
                string kjsvalue = string.Empty;

                //If Search basis on Pizza name/Title
                if (searchFlag == "title")
                {
                    title = row.SelectSingleNode("./div[@class='prod-info']/a[@class='product-page-link']/span").Value.Trim().ToLower();
                    if (searchedValue.ToLower() == title)
                    {
                        Console.WriteLine($"GetPizzaDetails - Actual pizza Title: {title}");
                        foundData.title = title;
                        foundData.kilojoules = row.SelectSingleNode("./div[@class='prod-info']/span").Value.Trim();
                        Console.WriteLine($"GetPizzaDetails -Actual pizza Kjs value: { foundData.kilojoules}");
                        return foundData;
                    }
                }

                //If Search basis on Pizza kjs value
                else if (searchFlag == "kjsValue")
                {
                    kjsvalue = row.SelectSingleNode("./div[@class='prod-info']/span[@class='kjs']").Value.Trim().ToLower();
                    if (searchedValue.ToLower() == kjsvalue)
                    {
                        Console.WriteLine($"GetPizzaDetails - Actual pizza kjsValue: {kjsvalue}");
                        foundData.kilojoules = kjsvalue;
                        foundData.title = row.SelectSingleNode("./div[@class='prod-info']/a[@class='product-page-link']/span").Value.Trim().Replace("amp;", ""); ;
                        Console.WriteLine($"GetPizzaDetails -Actual pizza title: { foundData.title}");
                        return foundData;
                    }
                }
            }
            return foundData;
        }
    }
}
