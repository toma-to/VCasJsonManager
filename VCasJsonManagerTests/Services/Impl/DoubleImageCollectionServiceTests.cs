//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCasJsonManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCasJsonManagerTests.Stubs;
using VCasJsonManager.Models;

namespace VCasJsonManager.Services.Impl.Tests
{
    [TestClass()]
    public class DoubleImageCollectionServiceTests
    {
        DoubleImageCollectionService target;
        ExecutionServiceStub execution;

        [TestInitialize()]
        public void Setup()
        {
            execution = new ExecutionServiceStub();
            target = new DoubleImageCollectionService(
                execution,
                new UriConversionService(new UserSettingServiceStub()),
                new ConfigJsonService(new UserSettingServiceStub(), new ConfigJsonFileServiceStub()),
                (conf) => conf.DoubleSidedImageUrls);
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_正常()
        {
            target.InputValue = "http://foo.bar/";
            target.AnotherInputValue = "http://baz.hoge/";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { new Uri("http://foo.bar/") }, target.Collection.Select(e => e.FrontSide).ToArray());
            CollectionAssert.AreEqual(new[] { new Uri("http://baz.hoge/") }, target.Collection.Select(e => e.BackSide).ToArray());
            Assert.IsFalse(target.HasErrors);
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_front_null()
        {
            target.InputValue = "";
            target.AnotherInputValue = "http://baz.hoge/";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.InputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_back_null()
        {
            target.InputValue = "http://baz.hoge/";
            target.AnotherInputValue = "";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.AnotherInputValue)).OfType<string>().ToArray());
        }


        [TestMethod()]
        public void ValidateAndCreateNewItemTest_frontback_null()
        {
            target.InputValue = "";
            target.AnotherInputValue = "";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.AnotherInputValue)).OfType<string>().ToArray());
            CollectionAssert.AreEqual(new[] { "入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.InputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_front_badurl()
        {
            target.InputValue = "hfoo.bar/";
            target.AnotherInputValue = "http://baz.hoge/";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいURLを入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.InputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_back_badurl()
        {
            target.InputValue = "http://baz.hoge";
            target.AnotherInputValue = "s";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいURLを入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.AnotherInputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_frontback_badurl()
        {
            target.InputValue = "e";
            target.AnotherInputValue = "s";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいURLを入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.AnotherInputValue)).OfType<string>().ToArray());
            CollectionAssert.AreEqual(new[] { "正しいURLを入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.InputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_エラー解除()
        {
            target.InputValue = "e";
            target.AnotherInputValue = "s";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいURLを入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.AnotherInputValue)).OfType<string>().ToArray());
            CollectionAssert.AreEqual(new[] { "正しいURLを入力してください" },
                target.GetErrors(nameof(DoubleImageCollectionService.InputValue)).OfType<string>().ToArray());

            target.InputValue = "http://foo.bar/";
            target.AnotherInputValue = "http://baz.hoge/";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { new Uri("http://foo.bar/") }, target.Collection.Select(e => e.FrontSide).ToArray());
            CollectionAssert.AreEqual(new[] { new Uri("http://baz.hoge/") }, target.Collection.Select(e => e.BackSide).ToArray());
            Assert.IsFalse(target.HasErrors);
        }

        [TestMethod()]
        public void BrowsItemTest_url()
        {
            target.BrowsItem(new Uri("http://foo.bar"));

            Assert.AreEqual("http://foo.bar/", execution.RunBrowserUrl);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void BrowsItemTest_exception()
        {
            target.BrowsItem(new ConfigJson.DoubleImageUrls(new Uri("http://foo.bar"), new Uri("http://bar.baz/")));
        }
    }
}
