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

using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Views.Converters.Tests
{
    [TestClass()]
    public class PresetNameConverterTests
    {
        [TestMethod()]
        public void ConvertTest_NotNull()
        {
            var input = new PresetInfo()
            {
                Name = "Name",
            };
            var target = new PresetNameConverter()
            {
                DefaltName = "Default",
            };

            var result = target.Convert(input, null, null, null);

            Assert.AreEqual("Name", result);

        }

        [TestMethod()]
        public void ConvertTest_Null()
        {
            var target = new PresetNameConverter()
            {
                DefaltName = "Default",
            };

            var result = target.Convert(null, null, null, null);

            Assert.AreEqual("Default", result);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertTest_BadClass()
        {
            var target = new PresetNameConverter()
            {
                DefaltName = "Default",
            };

            target.Convert("BadString", null, null, null);

        }

        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertBackTest()
        {
            new PresetNameConverter().ConvertBack(null, null, null, null);
        }
    }
}
