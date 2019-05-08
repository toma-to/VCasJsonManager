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
    public class UriListCollectionServiceTests
    {
        UriCollectionService target;
        ExecutionServiceStub execution;

        [TestInitialize()]
        public void Setup()
        {
            execution = new ExecutionServiceStub();
            target = new UriCollectionService(
                execution,
                new UriConversionService(new UserSettingServiceStub()),
                new ConfigJsonService(new UserSettingServiceStub(), new ConfigJsonFileServiceStub()),
                (conf) => conf.BackgroundUrls);
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_正常()
        {
            target.InputValue = "https://marshmallow-qa.com/system/images/a23a2bf5-b22f-4d1d-90c0-ba821989ec23.png";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { new Uri("https://marshmallow-qa.com/system/images/a23a2bf5-b22f-4d1d-90c0-ba821989ec23.png") }, target.Collection);
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
            target.InputValue = "1111111111";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいURLを入力してください" },
                target.GetErrors(nameof(Nico3dIdCollectionService.InputValue)).OfType<string>().ToArray());
        }

        [TestMethod()]
        public void ValidateAndCreateNewItemTest_エラー解除()
        {
            target.InputValue = "aa123";
            target.AddNewItem();

            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "正しいURLを入力してください" },
                target.GetErrors(nameof(Nico3dIdCollectionService.InputValue)).OfType<string>().ToArray());

            target.InputValue = "https://marshmallow-qa.com/system/images/a23a2bf5-b22f-4d1d-90c0-ba821989ec23.png";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { new Uri("https://marshmallow-qa.com/system/images/a23a2bf5-b22f-4d1d-90c0-ba821989ec23.png") }, target.Collection);
            Assert.IsFalse(target.HasErrors);
        }

        [TestMethod()]
        public void BrowsItemTest()
        {
            target.BrowsItem(new Uri("https://3d.nicovideo.jp/works/td123"));
            Assert.AreEqual("https://3d.nicovideo.jp/works/td123", execution.RunBrowserUrl);
        }
    }
}
