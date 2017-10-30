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
    public class TestColorsPicker : BaseClass
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
        /// Scerario 1 :
        /// Navigate to https://www.w3schools.com/colors/colors_picker.asp
        /// In the enter a colour input field enter “rgb(0,0,255)”
        /// Confirm the colour hex value is #0000ff
        /// 
        ///  Scerario 2 :
        /// In the Hue table find the row with a hue level of “150”
        /// Confirm the Hex value for this row is “#00ff80”
        /// </summary>
        [TestMethod]
        [TestCategory("ColorPicker")]
        public void TestEnterAColourInputAndVerifyHexValue()
        {
            var targetBrowser = (string)Context.Properties["targetUrlColorPicker"];
            _driver.Url = targetBrowser;
            _driver.Navigate();

            //================================
            //Scerario 1 :
            //================================
            var colorInput = "rgb(0,0,255)";
            var expectedHexValue = "#0000ff";

            //Create object of ColorPickerPage
            var colorPicker = new ColorPickerPage(_driver);

            //Enter input color and Click on Ok button
            colorPicker.EnterColorInput(colorInput)
                .ClickOnOkButton();

            var ActulHexVal = colorPicker.GetHexColorValue();

            //Verify Scenario 1
            Assert.AreEqual(expectedHexValue.ToLower(), ActulHexVal, $" Color hex value did not match , expected : { expectedHexValue} , Actual:  { ActulHexVal}");

            //================================
            //Scerario 2 :
            //================================
            // Define Veariables
            var hueLevelValue = "150";
            var expectedHex = "#00ff80";

            //Fetch the actual hex color details 
            var ActualColorDetails = colorPicker.GetHueColorDetails(hueLevelValue);
            Console.WriteLine($"Actual color hex value: {ActualColorDetails.hex}");

            //Verify that the hex value is “#00ff80”
            Assert.AreEqual(expectedHex.ToLower(), ActualColorDetails.hex.ToLower(), $"hex value of color did not match , expected value :{expectedHexValue}, Actual value: {ActualColorDetails.hex}");
        }


    }
}
