using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using TestCommon.CommonUtility;
using OpenQA.Selenium.Remote;

namespace TestCommon
{
    [TestClass]
    public class BaseClass
    {
        protected IWebDriver _driver;

        private TestContext _context = null;
        public TestContext Context
        {
            get
            {
                return this._context;
            }
            set
            {
                this._context = value;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = GetDriver();
            Console.WriteLine($"START TEST: {Context.FullyQualifiedTestClassName}.{Context.TestName}");
            Console.WriteLine("=================================================================");
        }


        [TestCleanup]
        public void TestCleanup()
        {
            string ssFolder = Path.Combine(Context.TestLogsDir, "Screenshots");
            var screenshotFileName = $"{Path.Combine(ssFolder, Context.TestName)}.png";

            if (Context.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                // prep folder if needed
                if (Directory.Exists(ssFolder) == false)
                {
                    Directory.CreateDirectory(ssFolder);
                }

                // Now take a screenshot
                _driver.TakeScreenshot(screenshotFileName, ScreenshotImageFormat.Png);
                Console.WriteLine($"**SCREENSHOT @ '{screenshotFileName}'**");
            }
            Console.WriteLine($"END TEST: {Context.FullyQualifiedTestClassName}.{Context.TestName}");
            Console.WriteLine("=================================================================");
            Kill(_driver);
        }

        /// <summary>
        /// For Quit Open browsers
        /// </summary>
        /// <param name="_driver"></param>
        public void Kill(IWebDriver _driver)
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }

        public IWebDriver GetDriver()
        {
            var targetBrowser = (string)Context.Properties["targetbrowser"];
            if (string.IsNullOrEmpty(targetBrowser))
            {
                try
                {
                    targetBrowser = "chrome";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }

            IWebDriver driver;

            switch (targetBrowser)
            {
                case "chrome":
                    driver = this.GetChromeDriver();
                    break;

                case "ie":
                    driver = this.GetIEDriver();
                    break;

                case "firefox":
                    driver = this.GetFirefoxDriver();
                    break;

                default:
                    driver = GetChromeDriver();
                    break;
            }
            return driver;
        }

        private IWebDriver GetChromeDriver()
        {
            Console.WriteLine("** LAUNCHING BROWSER: Chrome **");
            ChromeOptions chromeOptions = new ChromeOptions();
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("chrome.switcher", new[] { "--incognito" });
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("disable-infobars");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-cache");

            return new ChromeDriver(chromeOptions);
        }

        private IWebDriver GetIEDriver()
        {
            Console.WriteLine("** LAUNCHING BROWSER: IE **");
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }

        private IWebDriver GetFirefoxDriver()
        {
            Console.WriteLine("** LAUNCHING BROWSER: Firefox **");
            return new FirefoxDriver();
        }

    }
}
