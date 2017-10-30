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
    public class ColorPickerPage
    {
        IWebDriver _driver;
        By _pageLoadedSelector = By.XPath("//img[@alt='W3Schools.com']");

        public ColorPickerPage(IWebDriver webDriver)
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

        [FindsBy(How = How.CssSelector, Using = "#entercolor")]
        public IWebElement TxtEnterColor { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#entercolorDIV>button")]
        public IWebElement BtnColorOK { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#colorhexDIV")]
        public IWebElement ColorHexValue { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#huecontainer>.w3-responsive>.w3-table-all.colorTable>tbody")]
        public IWebElement ColorHueTable { get; set; }

        //Enter Color input
        public ColorPickerPage EnterColorInput(string input)
        {
            Console.WriteLine($"Enter Color Input : {input }");
            this.TxtEnterColor.SendKeys(input);
            return this;
        }

        By _colorTable = By.CssSelector("#huecontainer>.w3-responsive>.w3-table-all.colorTable>tbody");
        By _colorHex = By.CssSelector("#colorhexDIV");

        //Click on Ok button of Color input
        public ColorPickerPage ClickOnOkButton()
        {
            Console.WriteLine($"Click on Ok button");
            this.BtnColorOK.SafeClick(_driver);
            this._driver.WaitForAjaxLoad();
            return this;
        }


        //Retrieve Hex Color value
        public string GetHexColorValue()
        {
            CommonUtils.WaitForElementIsVisible(_driver, _colorHex);
            var value = this.ColorHexValue.Text.ToLower().Trim();
            Console.WriteLine($"Hex color value :{value}");
            return value;
        }

        /// <summary>
        /// Get Color Details
        /// </summary>
        /// <param name="searchedValue">Hue Value</param>
        /// <returns></returns>
        public ColorDetails GetHueColorDetails(string searchedValue)
        {
            ColorDetails foundData = new ColorDetails();
            this._driver.WaitForAjaxLoad();
            CommonUtils.WaitForElementIsVisible(_driver, _colorTable);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
            var gridHTML = (string)jse.ExecuteScript("return arguments[0].outerHTML;", this.ColorHueTable);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(gridHTML);
            var navigator = doc.CreateNavigator();

            var nodeIterator = navigator.Select("//tr");
            while (nodeIterator.MoveNext())
            {
                var row = nodeIterator.Current;
                var hueValue = row.SelectSingleNode("./td[2]").Value.Trim().ToLower().Replace("&nbsp;", "");
                if (searchedValue.ToLower() == hueValue)
                {
                    Console.WriteLine($"GetColorDetails - Actual hue Value: {hueValue}");
                    foundData.hue = hueValue;
                    foundData.hex = row.SelectSingleNode("./td[3]").Value.Trim().Replace("&nbsp;", "");
                    Console.WriteLine($"GetColorDetails -Actual hex value: { foundData.hex}");
                    return foundData;
                }
            }
            return foundData;
        }

    }
}
