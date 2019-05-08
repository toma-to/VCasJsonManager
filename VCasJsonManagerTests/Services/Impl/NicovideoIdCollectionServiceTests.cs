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
    public class NicovideoIdCollectionServiceTests
    {
        NicovideoIdCollectionService target;
        ExecutionServiceStub execution;

        [TestInitialize()]
        public void Setup()
        {
            execution = new ExecutionServiceStub();
            target = new NicovideoIdCollectionService(
                execution,
                new UriConversionService(new UserSettingServiceStub()),
                new ConfigJsonService(new UserSettingServiceStub(), new ConfigJsonFileServiceStub()));
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_正常()
        {
            target.InputValue = "sm6";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { "sm6" }, target.Collection);
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
            CollectionAssert.AreEqual(new[] { "正しいニコニコ動画のIDまたはURLを入力してください" },
                target.GetErrors(nameof(Nico3dIdCollectionService.InputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_エラー解除()
        {
            target.InputValue = "aa123";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいニコニコ動画のIDまたはURLを入力してください" },
                target.GetErrors(nameof(Nico3dIdCollectionService.InputValue)).OfType<string>().ToArray());

            target.InputValue = "sm6";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { "sm6" }, target.Collection);
            Assert.IsFalse(target.HasErrors);
        }

        [TestMethod()]
        public void BrowsItemTest()
        {
            target.BrowsItem("sm6");
            Assert.AreEqual("https://www.nicovideo.jp/watch/sm6", execution.RunBrowserUrl);
        }
    }
}
