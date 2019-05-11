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
using System.Collections.ObjectModel;
using VCasJsonManagerTests.Stubs;

namespace VCasJsonManager.Services.Impl.Tests
{
    class CollectionManagerStub : CollectionManageService<string>
    {

        public string newItem;

        protected override bool ValidateAndCreateNewItem(out string item)
        {
            if (newItem == null)
            {
                SetError(nameof(InputValue), "It's Error");
                item = null;
                return false;
            }
            else
            {
                ClearError(nameof(InputValue));
                item = newItem;
                return true;
            }
        }

        public override void BrowsItem(string item)
        {
            throw new NotImplementedException();
        }

        public CollectionManagerStub(ObservableCollection<string> col)
            : base(
                  new ExecutionService(new UserSettingServiceStub()),
                  new UriConversionService(new UserSettingServiceStub()),
                  new ConfigJsonService(new UserSettingServiceStub(), new ConfigJsonFileServiceStub()),
                  (_) => col)
        { }
    }

    [TestClass()]
    public class CollectionManageServiceTests
    {
        CollectionManagerStub target;
        List<string> ErrorChangedCalled;

        ObservableCollection<string> collection;

        [TestInitialize()]
        public void Setup()
        {
            collection = new ObservableCollection<string>();
            target = new CollectionManagerStub(collection);
            ErrorChangedCalled = new List<string>();
            target.ErrorsChanged += (_, e) => ErrorChangedCalled.Add(e.PropertyName);
            target.ConfigJson.IsChanged = false;
        }

        [TestMethod()]
        public void AddNewItemTest_正常()
        {
            target.newItem = "Value";
            target.AddNewItem();

            Assert.IsFalse(ErrorChangedCalled.Any());
            CollectionAssert.AreEqual(new[] { "Value" }, target.Collection);
            CollectionAssert.AreEqual(new[] { "Value" }, collection);
            Assert.IsFalse(target.HasErrors);
            Assert.IsNull(target.GetErrors(nameof(CollectionManagerStub.InputValue)));
            Assert.IsTrue(target.ConfigJson.IsChanged);
        }

        [TestMethod()]
        public void AddNewItemTest_バリデーションエラー()
        {
            target.newItem = null;
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { nameof(CollectionManagerStub.InputValue) }, ErrorChangedCalled);
            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.HasErrors);
            CollectionAssert.AreEqual(new[] { "It's Error" }, target.GetErrors(nameof(CollectionManagerStub.InputValue)).OfType<string>().ToArray());
            Assert.IsFalse(target.ConfigJson.IsChanged);
        }


        [TestMethod()]
        public void AddNewItemTest_バリデーションエラー解除()
        {
            target.newItem = null;
            target.AddNewItem();

            target.newItem = "Value";
            target.AddNewItem();

            CollectionAssert.AreEqual(new[] { nameof(CollectionManagerStub.InputValue), nameof(CollectionManagerStub.InputValue) }, ErrorChangedCalled);
            CollectionAssert.AreEqual(new[] { "Value" }, target.Collection);
            Assert.IsFalse(target.HasErrors);
            Assert.IsNull(target.GetErrors(nameof(CollectionManagerStub.InputValue)));
        }

        [TestMethod()]
        public void RemoveAtTest()
        {
            target.newItem = "Value1";
            target.AddNewItem();
            target.newItem = "Value2";
            target.AddNewItem();
            target.ConfigJson.IsChanged = false;

            target.RemoveAt(0);
            CollectionAssert.AreEqual(new[] { "Value2" }, target.Collection);
            Assert.IsTrue(target.ConfigJson.IsChanged);
        }

        [TestMethod()]
        public void RemoveAllTest()
        {
            target.newItem = "Value1";
            target.AddNewItem();
            target.newItem = "Value2";
            target.AddNewItem();
            target.ConfigJson.IsChanged = false;

            target.RemoveAll();
            Assert.IsFalse(target.Collection.Any());
            Assert.IsTrue(target.ConfigJson.IsChanged);
        }

        [TestMethod()]
        public void Collection_CollectionChangedTest()
        {
            collection.Add("Value");
            Assert.IsTrue(target.ConfigJson.IsChanged);
        }
    }
}
