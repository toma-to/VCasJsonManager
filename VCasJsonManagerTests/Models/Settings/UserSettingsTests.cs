//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCasJsonManager.Models.Settings;
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

namespace VCasJsonManager.Models.Settings.Tests
{
    [TestClass()]
    public class UserSettingsTests
    {
        AppSettingsStub appSettings = new AppSettingsStub();
        UserSettings target;

        [TestInitialize()]
        public void Setup()
        {
            appSettings.VirtualCastExePath = @"C:\VCas\VC.exe";
            appSettings.ConfigJsonPath = @"C:\VCas\\conf.json";

            target = new UserSettings(appSettings);
        }

        [TestMethod()]
        public void VirtualCastExePathTest_設定あり()
        {
            target.VirtualCastFolderPath = @"C:\work";

            Assert.AreEqual(@"C:\work\VirtualCast.exe", target.VirtualCastExePath);
        }

        [TestMethod()]
        public void VirtualCastExePathTest_設定なし()
        {
            target.VirtualCastFolderPath = null;

            Assert.AreEqual(@"C:\VCas\VC.exe", target.VirtualCastExePath);
        }

        [TestMethod()]
        public void ConfigJsonPathTest_設定あり()
        {
            target.VirtualCastFolderPath = @"C:\work";

            Assert.AreEqual(@"C:\work\config.json", target.ConfigJsonPath);
        }

        [TestMethod()]
        public void ConfigJsonPathTest_設定なし()
        {
            target.VirtualCastFolderPath = null;

            Assert.AreEqual(@"C:\VCas\\conf.json", target.ConfigJsonPath);
        }
    }
}
