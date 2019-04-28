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
using System.IO;
using System.Text.RegularExpressions;

using VCasJsonManagerTests.Stubs;
using VCasJsonManager.Models;
using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Services.Tests
{
    [TestClass()]
    public class ConfigJsonServiceTests
    {
        AppSettingsStub appSettings;
        UserSettingServiceStub settingService;
        ConfigJsonFileServiceStub fileService;
        ConfigJsonService target;
        List<string> notifiedProperies;
        ConfigJsonErrorEventArgs errorEvent;

        [TestInitialize()]
        public void Setup()
        {
            settingService = new UserSettingServiceStub();
            appSettings = (AppSettingsStub)settingService.AppSettings;
            fileService = new ConfigJsonFileServiceStub();
            target = new ConfigJsonService(settingService, fileService);
            target.ConfigJson.IsChanged = true;
            var obj = new PrivateObject(target);
            obj.SetProperty(nameof(ConfigJsonService.CurrentPreset), new PresetInfo() { Id = "300" });
            notifiedProperies = new List<string>();
            target.PropertyChanged += (_, e) => notifiedProperies.Add(e.PropertyName);
            errorEvent = null;
            target.ErrorOccurred += (_, e) => errorEvent = e;
        }

        [TestMethod()]
        public async Task ReadConfigJsonTest_ファイル有り()
        {
            appSettings.ConfigJsonPath = @"C:\VirtualCast\config.json";
            fileService.ConfigJson = new ConfigJson() { NgScoreThreshold = -999 };
            fileService.FileExist = true;

            await target.ReadConfigJsonAsync();

            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.ConfigJson), nameof(ConfigJsonService.CurrentPreset), nameof(ConfigJsonService.IsBusy) },
                                        notifiedProperies);
            Assert.IsNull(target.CurrentPreset);
            Assert.IsFalse(target.IsBusy);
            Assert.AreEqual(-999, target.ConfigJson.NgScoreThreshold);
            Assert.AreEqual(appSettings.ConfigJsonPath, fileService.ReadAsyncPath);
            Assert.AreEqual(appSettings.ConfigJsonPath, fileService.IsFileExistPath);
            Assert.IsNull(errorEvent);
        }

        [TestMethod()]
        public async Task ReadConfigJsonTest_ファイル無し()
        {
            appSettings.ConfigJsonPath = @"C:\VirtualCast\config.json";
            fileService.ConfigJson = new ConfigJson() { NgScoreThreshold = -999 };
            fileService.FileExist = false;
            var old = target.ConfigJson;

            await target.ReadConfigJsonAsync();

            Assert.IsFalse(notifiedProperies.Any());
            Assert.IsFalse(target.IsBusy);
            Assert.AreEqual(old, target.ConfigJson);
            Assert.IsNull(fileService.ReadAsyncPath);
            Assert.IsNull(errorEvent);
        }

        [TestMethod()]
        public async Task ReadConfigJsonTest_例外()
        {
            appSettings.ConfigJsonPath = @"C:\VirtualCast\config.json";
            fileService.FileExist = true;
            fileService.Exception = new FileNotFoundException();

            await target.ReadConfigJsonAsync();

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.ReadConfigJsonError, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);
        }

        [TestMethod()]
        public async Task WriteToConfigJsonAsyncTest()
        {
            appSettings.ConfigJsonPath = @"C:\VirtualCast\config.json";
            settingService.UserSettings.MergeUnknownJsonProperty = true;

            target.ConfigJson.CharacterModels.Add(10);

            await target.WriteToConfigJsonAsync();

            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsFalse(target.IsBusy);
            Assert.IsTrue(target.ConfigJson.IsChanged);
            Assert.AreEqual(appSettings.ConfigJsonPath, fileService.WriteAsyncPath);
            CollectionAssert.AreEqual(new[] { 10 }, fileService.ConfigJson.CharacterModels);
            Assert.IsNull(errorEvent);
        }

        [TestMethod()]
        public async Task WriteToConfigJsonAsyncTest_例外()
        {
            appSettings.ConfigJsonPath = @"C:\VirtualCast\config.json";
            settingService.UserSettings.MergeUnknownJsonProperty = true;
            fileService.Exception = new FileNotFoundException();

            await target.WriteToConfigJsonAsync();

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.WriteConfigJsonError, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);
        }

        [TestMethod()]
        public async Task SaveNewPresetTest_正常()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.MergeUnknownJsonProperty = true;
            target.ConfigJson.CharacterModels.Add(100);
            var name = "プリセットその1";

            await target.SaveNewPresetAsync(name);

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.CurrentPreset), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsTrue(Guid.TryParse(target.CurrentPreset.Id, out var _));
            Assert.IsFalse(target.ConfigJson.IsChanged);

            var preset = settingService.UserSettings.PresetInfos[0];
            Assert.AreEqual(name, preset.Name);
            Assert.AreEqual(target.CurrentPreset.Id, preset.Id);
            StringAssert.StartsWith(preset.FileName, name);
            StringAssert.EndsWith(preset.FileName, preset.Id + ".json");

            Assert.AreEqual(Path.Combine(appSettings.AppDataPath, preset.Name + "_" + preset.Id + ".json"), fileService.WriteAsyncPath);

            Assert.IsTrue(settingService.SaveAsyncCalled);
        }

        [TestMethod()]
        public async Task SaveNewPresetTest_ファイル名補正確認()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.MergeUnknownJsonProperty = true;
            target.ConfigJson.CharacterModels.Add(100);
            var name = "プリセット\\><ですよ??？";
            var filename = "プリセットですよ？";

            await target.SaveNewPresetAsync(name);

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.CurrentPreset), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsTrue(Guid.TryParse(target.CurrentPreset.Id, out var _));
            Assert.IsFalse(target.ConfigJson.IsChanged);

            var preset = settingService.UserSettings.PresetInfos[0];
            Assert.AreEqual(name, preset.Name);
            Assert.AreEqual(target.CurrentPreset.Id, preset.Id);
            StringAssert.StartsWith(preset.FileName, filename);
            StringAssert.EndsWith(preset.FileName, preset.Id + ".json");

            Assert.AreEqual(Path.Combine(appSettings.AppDataPath, filename + "_" + preset.Id + ".json"), fileService.WriteAsyncPath);
        }

        [TestMethod()]
        public async Task SaveNewPresetTest_例外()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.MergeUnknownJsonProperty = true;
            fileService.Exception = new FileNotFoundException();
            target.ConfigJson.CharacterModels.Add(100);
            var name = "プリセット";

            await target.SaveNewPresetAsync(name);

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.AreEqual("300", target.CurrentPreset.Id);
            Assert.IsFalse(settingService.UserSettings.PresetInfos.Any());
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.CreatePresetError, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);

        }

        [TestMethod()]
        public async Task SavePresetTest_正常()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            settingService.UserSettings.MergeUnknownJsonProperty = true;

            await target.SavePresetAsync("200");
            Assert.IsFalse(target.ConfigJson.IsChanged);

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.AreEqual(Path.Combine(appSettings.AppDataPath, "bar_200.json"), fileService.WriteAsyncPath);
        }

        [TestMethod()]
        public async Task SavePresetTest_ID無し()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            settingService.UserSettings.MergeUnknownJsonProperty = true;

            await target.SavePresetAsync("300");

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.WritePresetError, errorEvent.ErrorCause);
            Assert.IsInstanceOfType(errorEvent.Exception, typeof(FileNotFoundException));
        }

        [TestMethod()]
        public async Task SavePresetTest_例外()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            settingService.UserSettings.MergeUnknownJsonProperty = true;
            fileService.Exception = new IOException();

            await target.SavePresetAsync("200");

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.WritePresetError, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);
        }

        [TestMethod()]
        public async Task LoadPresetTest_正常()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            fileService.ConfigJson = new ConfigJson() { NgScoreThreshold = -999 };

            await target.LoadPresetAsync("200");

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(
                new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.ConfigJson), nameof(ConfigJsonService.CurrentPreset), nameof(ConfigJsonService.IsBusy) },
                notifiedProperies);
            Assert.AreEqual(Path.Combine(appSettings.AppDataPath, "bar_200.json"), fileService.ReadAsyncPath);
            Assert.AreEqual(fileService.ConfigJson, target.ConfigJson);
            Assert.AreEqual("200", target.CurrentPreset.Id);
        }

        [TestMethod()]
        public async Task LoadPresetTest_ID無し()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            fileService.ConfigJson = new ConfigJson() { NgScoreThreshold = -999 };

            await target.LoadPresetAsync("400");

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.ReadPresetError, errorEvent.ErrorCause);
            Assert.IsInstanceOfType(errorEvent.Exception, typeof(FileNotFoundException));
            Assert.AreEqual("300", target.CurrentPreset.Id);
        }

        [TestMethod()]
        public async Task LoadPresetTest_例外()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            fileService.ConfigJson = new ConfigJson() { NgScoreThreshold = -999 };
            fileService.Exception = new IOException();

            await target.LoadPresetAsync("200");

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(
                new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.ConfigJson), nameof(ConfigJsonService.CurrentPreset), nameof(ConfigJsonService.IsBusy) },
                notifiedProperies);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.ReadPresetError, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);
            Assert.AreEqual("200", target.CurrentPreset.Id);
        }

        [TestMethod()]
        public async Task DeletePresetAsyncTest_正常_CurrentPresetでない()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "300",
                Name = "baz",
                FileName = "baz_300.json",
            });

            await target.DeletePresetAsync("200");

            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsFalse(target.IsBusy);
            Assert.AreEqual("300", target.CurrentPreset.Id);
            Assert.AreEqual(Path.Combine(appSettings.AppDataPath, "bar_200.json"), fileService.DeletePath);
            CollectionAssert.AreEqual(new[] { "100", "300" }, settingService.UserSettings.PresetInfos.Select(e => e.Id).ToArray());
        }

        [TestMethod()]
        public async Task DeletePresetAsyncTest_正常_CurrentPreset()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "300",
                Name = "baz",
                FileName = "baz_300.json",
            });

            await target.DeletePresetAsync("300");

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.CurrentPreset), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsNull(target.CurrentPreset);
            Assert.AreEqual(Path.Combine(appSettings.AppDataPath, "baz_300.json"), fileService.DeletePath);
            CollectionAssert.AreEqual(new[] { "100", "200" }, settingService.UserSettings.PresetInfos.Select(e => e.Id).ToArray());
        }


        [TestMethod()]
        public async Task DeletePresetAsyncTest_対象プリセット無し()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "300",
                Name = "baz",
                FileName = "baz_300.json",
            });

            await target.DeletePresetAsync("400");

            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsFalse(target.IsBusy);
            Assert.AreEqual("300", target.CurrentPreset.Id);
            Assert.IsNull(fileService.DeletePath);
            CollectionAssert.AreEqual(new[] { "100", "200", "300" }, settingService.UserSettings.PresetInfos.Select(e => e.Id).ToArray());
        }

        [TestMethod()]
        public async Task DeletePresetAsyncTest_例外()
        {
            appSettings.AppDataPath = @"C:\AppData";
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "100",
                Name = "foo",
                FileName = "foo_100.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "200",
                Name = "bar",
                FileName = "bar_200.json",
            });
            settingService.UserSettings.PresetInfos.Add(new PresetInfo()
            {
                Id = "300",
                Name = "baz",
                FileName = "baz_300.json",
            });

            fileService.Exception = new IOException();
            await target.DeletePresetAsync("200");

            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsFalse(target.IsBusy);
            Assert.AreEqual("300", target.CurrentPreset.Id);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.DeletePresetError, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);
            CollectionAssert.AreEqual(new[] { "100", "200", "300" }, settingService.UserSettings.PresetInfos.Select(e => e.Id).ToArray());
        }

        [TestMethod()]
        public async Task ImportJsonAsyncTest_正常()
        {
            fileService.ConfigJson = new ConfigJson() { NgScoreThreshold = -999 };
            var path = @"C:\work\config.json";

            await target.ImportJsonAsync(path);

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(
                new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.ConfigJson), nameof(ConfigJsonService.CurrentPreset), nameof(ConfigJsonService.IsBusy) },
                notifiedProperies);
            Assert.AreEqual(path, fileService.ReadAsyncPath);
            Assert.AreEqual(fileService.ConfigJson, target.ConfigJson);
            Assert.IsNull(target.CurrentPreset);
            Assert.IsNull(errorEvent);
        }

        [TestMethod()]
        public async Task ImportJsonAsyncTest_Exception()
        {
            fileService.Exception = new UnauthorizedAccessException();
            var path = @"C:\work\config.json";

            await target.ImportJsonAsync(path);

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(
                new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) },
                notifiedProperies);
            Assert.AreEqual("300", target.CurrentPreset.Id);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.ImportJsonOpenError, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);
        }

        [TestMethod()]
        public async Task ImportJsonAsyncTest_JsonException()
        {
            fileService.Exception = new Newtonsoft.Json.JsonException();
            var path = @"C:\work\config.json";

            await target.ImportJsonAsync(path);

            Assert.IsFalse(target.IsBusy);
            CollectionAssert.AreEqual(
                new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) },
                notifiedProperies);
            Assert.AreEqual("300", target.CurrentPreset.Id);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.ImportJsonBadFormat, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);
        }

        [TestMethod()]
        public async Task ExportJsonAsyncTest_正常()
        {
            settingService.UserSettings.MergeUnknownJsonProperty = true;
            var path = @"C:\work\config.json";

            await target.ExportJsonAsync(path);

            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsFalse(target.IsBusy);
            Assert.AreEqual(path, fileService.WriteAsyncPath);
            Assert.AreEqual(fileService.ConfigJson, target.ConfigJson);
            Assert.IsNull(errorEvent);
        }

        [TestMethod()]
        public async Task ExportJsonAsyncTest_例外()
        {
            settingService.UserSettings.MergeUnknownJsonProperty = true;
            var path = @"C:\work\config.json";
            fileService.Exception = new System.Security.SecurityException();

            await target.ExportJsonAsync(path);

            CollectionAssert.AreEqual(new[] { nameof(ConfigJsonService.IsBusy), nameof(ConfigJsonService.IsBusy) }, notifiedProperies);
            Assert.IsFalse(target.IsBusy);
            Assert.AreEqual(ConfigJsonErrorEventArgs.Cause.ExoprtJsonError, errorEvent.ErrorCause);
            Assert.IsNotNull(errorEvent.Exception);
        }
    }
}
