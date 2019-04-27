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
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;

using VCasJsonManagerTests;
using VCasJsonManagerTests.Stubs;
using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Services.Tests
{
    [TestClass()]
    public class UserSettingsServiceTests
    {
        AppSettingsStub appSettings = new AppSettingsStub();
        string settingPath;

        [TestInitialize()]
        public void Setup()
        {
            appSettings.AppDataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            settingPath = Path.Combine(appSettings.AppDataPath, "setting.json");

            File.Delete(settingPath);
        }

        [TestMethod()]
        public async Task SaveAsyncTest_正常()
        {
            var target = new UserSettingsService(appSettings);

            target.UserSettings.MergeUnknownJsonProperty = true;
            target.UserSettings.ConvertGoogleDriveUri = false;
            target.UserSettings.PresetInfos = new ObservableCollection<PresetInfo>(new[]
            {
                new PresetInfo()
                {
                    Id = "1",
                    Name = "Preset1",
                    FileName = "Preset1_1.json",
                },
                new PresetInfo()
                {
                    Id = "2",
                    Name = "Preset2",
                    FileName = "Preset2_2.json",
                },
                new PresetInfo()
                {
                    Id = "3",
                    Name = "Preset3",
                    FileName = "Preset3_3.json",
                },
            });
            target.UserSettings.VirtualCastFolderPath = @"C:\work";

            await target.SaveAsync();

            var expected = @"
{
    ""VirtualCastFolderPath"": ""C:\\work"",
    ""PresetInfos"": [
        {
            ""Id"": ""1"",
            ""Name"": ""Preset1"",
            ""FileName"": ""Preset1_1.json""
        },
        {
            ""Id"": ""2"",
            ""Name"": ""Preset2"",
            ""FileName"": ""Preset2_2.json""
        },
        {
            ""Id"": ""3"",
            ""Name"": ""Preset3"",
            ""FileName"": ""Preset3_3.json""
        }
    ],
    ""ConvertGoogleDriveUri"": false,
    ""MergeUnknownJsonProperty"": true
}".RemoveSpace();
            var result = await TestUtility.ReadResultDataFileAsync(settingPath);
            Assert.AreEqual(expected, result.RemoveSpace());
        }

        [TestMethod()]
        [ExpectedException(typeof(IOException), AllowDerivedTypes = true)]
        public async Task SaveAsyncTest_NG()
        {
            appSettings.AppDataPath = Path.Combine(appSettings.AppDataPath, "baddirectory");
            var target = new UserSettingsService(appSettings);

            target.UserSettings.MergeUnknownJsonProperty = true;
            target.UserSettings.ConvertGoogleDriveUri = false;
            target.UserSettings.PresetInfos = new ObservableCollection<PresetInfo>(new[]
            {
                new PresetInfo()
                {
                    Id = "1",
                    Name = "Preset1",
                    FileName = "Preset1_1.json",
                },
                new PresetInfo()
                {
                    Id = "2",
                    Name = "Preset2",
                    FileName = "Preset2_2.json",
                },
                new PresetInfo()
                {
                    Id = "3",
                    Name = "Preset3",
                    FileName = "Preset3_3.json",
                },
            });

            await target.SaveAsync();

        }

        [TestMethod()]
        public async Task ConstructorTest_正常()
        {
            var input = @"
{
    ""VirtualCastFolderPath"": ""C:\\work"",
    ""PresetInfos"": [
        {
            ""Id"": ""1"",
            ""Name"": ""Preset1"",
            ""FileName"": ""Preset1_1.json""
        },
        {
            ""Id"": ""2"",
            ""Name"": ""Preset2"",
            ""FileName"": ""Preset2_2.json""
        },
        {
            ""Id"": ""3"",
            ""Name"": ""Preset3"",
            ""FileName"": ""Preset3_3.json""
        }
    ],
    ""ConvertGoogleDriveUri"": false,
    ""MergeUnknownJsonProperty"": false
}".RemoveSpace();
            await TestUtility.CreateTestDataFileAsync(settingPath, input);

            var target = new UserSettingsService(appSettings);

            Assert.IsFalse(target.UserSettings.ConvertGoogleDriveUri);
            Assert.IsFalse(target.UserSettings.MergeUnknownJsonProperty);
            Assert.AreEqual(@"C:\work", target.UserSettings.VirtualCastFolderPath);
            CollectionAssert.AreEqual(new[] { "1", "2", "3" }, target.UserSettings.PresetInfos.Select(e => e.Id).ToArray());
            CollectionAssert.AreEqual(new[] { "Preset1", "Preset2", "Preset3" }, target.UserSettings.PresetInfos.Select(e => e.Name).ToArray());
            CollectionAssert.AreEqual(new[] { "Preset1_1.json", "Preset2_2.json", "Preset3_3.json" },
                                        target.UserSettings.PresetInfos.Select(e => e.FileName).ToArray());

        }

        [TestMethod()]
        public void ConstructorTest_ファイル無し()
        {
            var target = new UserSettingsService(appSettings);

            Assert.IsTrue(target.UserSettings.ConvertGoogleDriveUri);
            Assert.IsTrue(target.UserSettings.MergeUnknownJsonProperty);
            Assert.IsNotNull(target.UserSettings.PresetInfos);
            Assert.IsFalse(target.UserSettings.PresetInfos.Any());
        }

        [TestMethod()]
        public async Task ConstructorTest_例外()
        {
            var input = @"
{
    ""PresetInfos"": [
        {
            ""Id"": ""1"",
            ""Name"": ""Preset1"",
            ""FileName"": ""Preset1_1.json""
        },
        {
            ""Id"": ""2"",
            ""Name"": ""Preset2"",
            ""FileName"": ""Preset2_2.json""
        },
        }
    ],
    ""ConvertGoogleDriveUri"": false,
    ""MergeUnknownJsonProperty"": false
}".RemoveSpace();
            await TestUtility.CreateTestDataFileAsync(settingPath, input);

            var target = new UserSettingsService(appSettings);

            Assert.IsTrue(target.UserSettings.ConvertGoogleDriveUri);
            Assert.IsTrue(target.UserSettings.MergeUnknownJsonProperty);
            Assert.IsNotNull(target.UserSettings.PresetInfos);
            Assert.IsFalse(target.UserSettings.PresetInfos.Any());

        }

    }
}
