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
using System.IO;
using System.Configuration;

namespace VCasJsonManager.Models.Settings.Tests
{
    [TestClass()]
    public class AppSettingsTests
    {
        [TestMethod()]
        public void ConstructorTest_デフォルトパス()
        {
            ConfigurationManager.AppSettings["ConfigJsonPath"] = @"";
            ConfigurationManager.AppSettings["VirtualCastExePath"] = null;
            var target = new AppSettings();

            Assert.IsTrue(Path.IsPathRooted(target.ModuleDirectory));
            var directory = new DirectoryInfo(target.ModuleDirectory);
            Assert.AreEqual(directory.Parent.FullName + @"\config.json", target.ConfigJsonPath);
            Assert.AreEqual(directory.Parent.FullName + @"\VirtualCast.exe", target.VirtualCastExePath);
            StringAssert.EndsWith(target.AppDataPath, @"\VCasJsonManager");
            Assert.IsTrue(Path.IsPathRooted(target.AppDataPath));

            Assert.IsTrue(Directory.Exists(target.AppDataPath));
        }

        [TestMethod()]
        public void ConstructorTest_指定パス()
        {
            ConfigurationManager.AppSettings["ConfigJsonPath"] = @"C:\Foo\bar.json";
            ConfigurationManager.AppSettings["VirtualCastExePath"] = @"C:\Foo\Vcas.com";

            var target = new AppSettings();

            Assert.AreEqual(@"C:\Foo\bar.json", target.ConfigJsonPath);
            Assert.AreEqual(@"C:\Foo\Vcas.com", target.VirtualCastExePath);
        }
    }
}
