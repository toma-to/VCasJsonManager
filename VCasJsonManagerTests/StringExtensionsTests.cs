//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCasJsonManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCasJsonManager.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ToUriTest_正常()
        {
            var result = "http://drive.google.com/uc?export=view&id=xxx".ToUri();

            Assert.AreEqual(new Uri("http://drive.google.com/uc?export=view&id=xxx"), result);
        }

        [TestMethod()]
        public void ToUriTest_NG()
        {
            var result = "drive.google.com/uc?export=view&id=1fQC0vygVvYbaNa8ADBykcFNuDXWgWjGb".ToUri();

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void IsAbsolutePathTest_絶対パス()
        {
            var result = @"C:\users\foo\config.json".IsAbsolutePath();

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsAbsolutePathTest_相対パス()
        {
            var result = @"users\foo\config.json".IsAbsolutePath();

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsAbsolutePathTest_null()
        {
            var result = ((string)null).IsAbsolutePath();

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsAbsolutePathTest_BADパス()
        {
            var result = @"C:\users\foo\config><.json".IsAbsolutePath();

            Assert.IsFalse(result);
        }
    }
}
