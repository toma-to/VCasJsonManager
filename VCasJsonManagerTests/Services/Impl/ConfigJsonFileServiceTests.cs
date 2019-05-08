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

using VCasJsonManager.Models;
using VCasJsonManagerTests;

namespace VCasJsonManager.Services.Impl.Tests
{
    [TestClass()]
    public class ConfigJsonFileServiceTests
    {
        string jsonPath;

        [TestInitialize()]
        public void Setup()
        {
            jsonPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "test.json");
            File.Delete(jsonPath);
        }

        [TestMethod()]
        public async Task ReadAsyncTest()
        {
            var input = @"
{
  ""niconico"": {
    ""character_models"": [
      48419,
    ]
  },
  ""persistent_object"": {
    ""image_urls"": [
    ],
    ""nicovideo_ids"": [
    ]
  },
  ""item"": {
    ""whiteboard"": {
      ""source_urls"": [
        ""http://drive.google.com/uc?export=view&id=1fQC0vygVvYbaNa8ADBykcFNuDXWgWjGb""
      ]
    },
    ""cue_card"": {
      ""source_urls"": [
      ]
    },
    ""hide_camera_from_viewers"": true
  },
  ""humanoid"": {
    ""use_fast_spring_bone"": true
  },
  ""embedded_script_"": {
     ""websocket_console_port"": 8080,
     ""vr_debug"": true
  }
}";
            await TestUtility.CreateTestDataFileAsync(jsonPath, input);

            var target = new ConfigJsonFileService();
            var result = await target.ReadAsync(jsonPath);

            Assert.AreEqual(48419, result.CharacterModels.ElementAt(0));
            Assert.IsFalse(result.IsChanged);
        }

        [TestMethod()]
        public async Task WriteAsyncTest()
        {
            Assert.IsFalse(File.Exists(jsonPath));

            var input = new ConfigJson();

            var target = new ConfigJsonFileService();

            await target.WriteAsync(jsonPath, input, true);

            Assert.IsTrue(File.Exists(jsonPath));
        }

        [TestMethod()]
        public async Task IsFileExistTest()
        {
            var target = new ConfigJsonFileService();
            Assert.IsFalse(target.IsFileExist(jsonPath));

            await TestUtility.CreateTestDataFileAsync(jsonPath, "{}");
            Assert.IsTrue(target.IsFileExist(jsonPath));
        }

        [TestMethod()]
        public async Task DeleteFileTest()
        {
            await TestUtility.CreateTestDataFileAsync(jsonPath, "{}");
            Assert.IsTrue(File.Exists(jsonPath));

            new ConfigJsonFileService().DeleteFile(jsonPath);

            Assert.IsFalse(File.Exists(jsonPath));

        }
    }
}
