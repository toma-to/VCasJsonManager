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

namespace VCasJsonManager.Services.Impl.Tests
{
    [TestClass()]
    public class MylistIdCollectionServiceTests
    {
        MylistIdCollectionService target;
        ExecutionServiceStub execution;

        [TestInitialize()]
        public void Setup()
        {
            execution = new ExecutionServiceStub();
            target = new MylistIdCollectionService(
                execution,
                new UriConversionService(new UserSettingServiceStub()),
                new ConfigJsonService(new UserSettingServiceStub(), new ConfigJsonFileServiceStub()));
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_正常()
        {
            target.InputValue = "123";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { 123 }, target.Collection);
            Assert.IsFalse(target.HasErrors);
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_入力無し()
        {
            target.InputValue = "";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "入力してください" },
                target.GetErrors(nameof(Nico3dIdCollectionService.InputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_入力不正()
        {
            target.InputValue = "aa123";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいマイリストのIDまたはURLを入力してください" },
                target.GetErrors(nameof(Nico3dIdCollectionService.InputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_エラー解除()
        {
            target.InputValue = "aa123";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいマイリストのIDまたはURLを入力してください" },
                target.GetErrors(nameof(Nico3dIdCollectionService.InputValue)).OfType<string>().ToArray());

            target.InputValue = "123";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { 123 }, target.Collection);
            Assert.IsFalse(target.HasErrors);
        }

        [TestMethod()]
        public void BrowsItemTest()
        {
            target.BrowsItem(123);
            Assert.AreEqual("https://www.nicovideo.jp/mylist/123", execution.RunBrowserUrl);
        }
    }
}
