using System.Windows.Controls;
using System.Windows.Data;
using InfiniteRain.Shared.Presentation.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows;

namespace InfiniteRain.Shared.Presentation.Tests
{
    [TestClass]
    public class BooleanVisibilityConverterTests
    {
        private IValueConverter target;

        [TestInitialize]
        public void Init()
        {
            target = new BooleanVisibilityConverter();
        }

        [TestMethod]
        public void WillConvertSuccessfully()
        {
            Assert.AreEqual(Visibility.Visible, Convert(true, "True"));
            Assert.AreEqual(Visibility.Collapsed, Convert(true, "False"));
            Assert.AreEqual(Visibility.Collapsed, Convert(false, "True"));
            Assert.AreEqual(Visibility.Visible, Convert(false, "False"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WillNotConvert1()
        {
            Convert(false, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WillNotConvert2()
        {
            Convert(true, null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void WillNotConvert3()
        {
            Convert(false, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void WillNotConvert4()
        {
            Convert(true, string.Empty);
        }

        public Visibility Convert(bool value, string parameter)
        {
            return (Visibility)target.Convert(value, typeof(Grid), parameter, CultureInfo.CurrentCulture);
        }


        [TestMethod]
        public void WillConvertBackSuccessfully()
        {
            Assert.AreEqual(true, ConvertBack(Visibility.Visible, "True"));
            Assert.AreEqual(true, ConvertBack(Visibility.Collapsed, "False"));
            Assert.AreEqual(false, ConvertBack(Visibility.Collapsed, "True"));
            Assert.AreEqual(false, ConvertBack(Visibility.Visible, "False"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WillNotConvertBack1()
        {
            ConvertBack(Visibility.Collapsed, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WillNotConvertBack2()
        {
            ConvertBack(Visibility.Visible, null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void WillNotConvertBack3()
        {
            ConvertBack(Visibility.Collapsed, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void WillNotConvertBack4()
        {
            ConvertBack(Visibility.Visible, string.Empty);
        }

        public bool ConvertBack(Visibility value, string parameter)
        {
            return (bool)target.ConvertBack(value, typeof(Grid), parameter, CultureInfo.CurrentCulture);
        }
    }
}
