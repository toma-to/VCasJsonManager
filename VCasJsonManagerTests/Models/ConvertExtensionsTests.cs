//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCasJsonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCasJsonManager.Models.Tests
{
    [TestClass()]
    public class ConvertExtensionsTests
    {
        [TestMethod()]
        public void ToUriListTest()
        {
            var input = new[]
            {
                "http://drive.google.com/uc?export=view&id=xxx",
                "http://yahoo.co.jp",
                "hoge",
                "http://localhost",
                null,
                "https://google.com",
            };

            var result = input.ToUriList();

            var expected = new[]
            {
                new Uri("http://drive.google.com/uc?export=view&id=xxx"),
                new Uri("http://yahoo.co.jp"),
                new Uri("http://localhost"),
                new Uri("https://google.com"),
            };

            CollectionAssert.AreEqual(expected, result.ToArray());
        }

        [TestMethod()]
        public void ToDoubleImageUrlTest()
        {
            var input = new[]
            {
                new []
                {
                    "http://front.foo",
                    "http://back.foo",
                },
                null,
                new string[0],
                new []
                {
                    "http://front.bar",
                },
                new []
                {
                    "http://front.baz",
                    "http://back.baz",
                },
                new []
                {
                    ":front.hoge",
                    "http://back.boge",
                },
                new []
                {
                    "http://front.hoge",
                    "http://back.hoge",
                },

            };

            var result = input.ToDoubleImageUrl();

            var expectedFront = new[]
            {
               new Uri("http://front.foo"),
               new Uri("http://front.baz"),
               new Uri("http://front.hoge"),
            };
            var expectedBack = new[]
            {
               new Uri("http://back.foo"),
               new Uri("http://back.baz"),
               new Uri("http://back.hoge"),
            };

            CollectionAssert.AreEqual(expectedFront, result.Select(e => e.FrontSide).ToArray());
            CollectionAssert.AreEqual(expectedBack, result.Select(e => e.BackSide).ToArray());
        }
    }
}
