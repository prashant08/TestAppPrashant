using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Model;

namespace TestCommon.CommonUtility
{
    public static class CommonUtils
    {
        /// <summary>
        /// Wait For Clickable Element
        /// </summary>
        /// <param name="webDriver">Driver</param>
        /// <param name="element">IWebElement type </param>
        /// <param name="maxMilliseconds">Explicit wait time in milliseconds</param>
        public static void WaitForClickableElement(IWebDriver webDriver, IWebElement element, int maxMilliseconds = 10000)
        {
            Console.WriteLine($"\tWAITING START - for element clickable: Max - {maxMilliseconds} ms");
            DateTime start = DateTime.Now;
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 0, 0, maxMilliseconds));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"\tWAIT: {element} did not clickable in {maxMilliseconds}ms! { ex.Message }");
                throw new InvalidOperationException($"{element} did not clickable in {maxMilliseconds}ms! { ex.Message }");
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"\tWAIT: {element} did not exists in {maxMilliseconds}ms! { ex.Message }");
                throw new InvalidOperationException($"{element} did not exists in {maxMilliseconds}ms! { ex.Message }");
            }
            DateTime end = DateTime.Now;
            TimeSpan elapsed = end.Subtract(start);
            Console.WriteLine($"\tWAITING END - Elapsed: {elapsed.TotalMilliseconds} ms");
        }

        /// <summary>
        /// Wait For Clickable Element
        /// </summary>
        /// <param name="webDriver">Driver</param>
        /// <param name="locator">By  type locator </param>
        /// <param name="maxMilliseconds">Explicit wait time in milliseconds</param>
        public static void WaitForClickableElement(IWebDriver webDriver, By locator, int maxMilliseconds = 10000)
        {
            Console.WriteLine($"\tWAITING START - for element clickable: Max - {maxMilliseconds} ms");
            DateTime start = DateTime.Now;
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 0, 0, maxMilliseconds));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"\tWAIT: {locator} did not clickable in {maxMilliseconds}ms! { ex.Message }");
                throw new InvalidOperationException($"{locator} did not clickable in {maxMilliseconds}ms! { ex.Message }");
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"\tWAIT: {locator} did not exists in {maxMilliseconds}ms! { ex.Message }");
                throw new InvalidOperationException($"{locator} did not exists in {maxMilliseconds}ms! { ex.Message }");
            }
            DateTime end = DateTime.Now;
            TimeSpan elapsed = end.Subtract(start);
            Console.WriteLine($"\tWAITING END - Elapsed: {elapsed.TotalMilliseconds} ms");
        }

        /// <summary>
        /// Wait For Element To Be Visible
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="selector">By type</param>
        /// <param name="milliseconds">Explicit wait time in milliseconds</param>
        public static void WaitForElementIsVisible(IWebDriver webDriver, By selector, int milliseconds = 10000)
        {
            Console.WriteLine($"\tWAITING START - for element clickable: Max - {milliseconds} ms");
            DateTime start = DateTime.Now;
            var activeLink = new WebDriverWait(webDriver, new TimeSpan(0, 0, 0, 0, milliseconds));
            try
            {
                activeLink.Until(ExpectedConditions.ElementIsVisible(selector));
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"\tWAIT: {selector} did not Visible in {milliseconds}ms! {ex.Message}");
                throw new InvalidOperationException($"{selector} did not Visible in {milliseconds}ms! { ex.Message }");
            }
            catch (ElementNotVisibleException ex)
            {
                Console.WriteLine($"\tWAIT: {selector} did not Visible in {milliseconds}ms!");
                throw new InvalidOperationException($"{selector} did not Visible in {milliseconds}ms!  { ex.Message }");
            }
            DateTime end = DateTime.Now;
            TimeSpan elapsed = end.Subtract(start);
            Console.WriteLine($"\tWAIT: Wait for  {selector} Visible completion {elapsed.TotalMilliseconds}ms total.");
        }
    }
}
