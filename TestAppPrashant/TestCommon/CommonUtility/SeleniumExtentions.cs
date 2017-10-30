using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using HtmlAgilityPack;
using System.Xml;

namespace TestCommon.CommonUtility
{
    public static class SeleniumExtentions
    {

        /// <summary>
        /// Sends a "Click" to the target element via JavaScript when running via IE browser
        /// For all other browsers it uses the standard Element.Click()
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="driver">IWebDriver</param>
        public static void SafeClick(this IWebElement element, IWebDriver driver)
        {
            if (driver is OpenQA.Selenium.IE.InternetExplorerDriver ||
                driver is OpenQA.Selenium.Firefox.FirefoxDriver)
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", element);
            }
            else
            {
                element.Click();
            }
        }


        /// <summary>
        /// Take a screenshot of the current page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="FileLocationName">Full directory plus the file name and format, e.g C:\Temp\Image.jpeg</param>
        /// <param name="imageFormat"Screenshot Image Format</param>
        public static void TakeScreenshot(this IWebDriver driver, string FileLocationName, ScreenshotImageFormat imageFormat)
        {
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(FileLocationName, imageFormat);
        }

        /// <summary>
        /// Scroll down page horizontally
        /// </summary>
        public static void ScrollHorizontally(this IWebDriver webDriver, int msToWaitForAjax)
        {
            ((IJavaScriptExecutor)webDriver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
        }

        /// <summary>
        /// Method to send values for non empty fields.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="element"></param>
        public static void SendKeysWhenNotNullOrEmpty(string value, IWebElement element)
        {
            if (!string.IsNullOrEmpty(value))
            {
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// If ajax available on Page, it Waits for AJAX calls to be Complete Load. 
        /// Also handles conditions : If Ajax call already loaded or not available on page.
        /// </summary>
        /// <param name="webDriver">The IWebDriver</param>
        /// <param name="timeoutMs">Max number of seconds to wait (default=5000)</param>
        /// <returns></returns>
        public static bool WaitForAjaxLoad(this IWebDriver webDriver, int timeoutMs = 5000)
        {
            var startTime = DateTime.Now;
            bool statuswaitForAjax = false;
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(timeoutMs));
                statuswaitForAjax = wait.Until(d => (bool)((IJavaScriptExecutor)d).ExecuteScript("return jQuery.active == 0"));
                var endTime = DateTime.Now;
                var elapsedTime = endTime - startTime;
                return statuswaitForAjax;
            }
            catch (Exception)
            {
                // Some time jQuery is not available on page or already loaded so exception occured in this case and TestScript get fail.
                // If no jQuery present or already loaded -return true
                return true;
            }
        }
    }
}
