//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCasJsonManager.Views.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCasJsonManager.Views.Converters.Tests
{
    [TestClass()]
    public class BoolInvertConverterTests
    {
        [TestMethod()]
        public void ConvertTest_true()
        {
            var result = new BoolInvertConverter().Convert(true, null, null, null);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void ConvertTest_false()
        {
            var result = new BoolInvertConverter().Convert(false, null, null, null);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void ConvertTest_NotBool()
        {
            var result = new BoolInvertConverter().Convert("", null, null, null);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void ConvertBackTest_true()
        {
            var result = new BoolInvertConverter().ConvertBack(true, null, null, null);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void ConvertBackTest_false()
        {
            var result = new BoolInvertConverter().ConvertBack(false, null, null, null);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void ConvertBackTest_NotBool()
        {
            var result = new BoolInvertConverter().ConvertBack("", null, null, null);
            Assert.AreEqual(false, result);
        }

    }
}
