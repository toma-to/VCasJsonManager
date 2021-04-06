//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCasJsonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

using VCasJsonManagerTests;

namespace VCasJsonManager.Models.Tests
{
    [TestClass()]
    public class ConfigJsonExtensionsTests
    {
        [TestMethod()]
        public void FlattenTest_正常()
        {
            var input = new ConfigJsonStructure();
            input.Niconico.CharacterModels = new[] { 111, 222, 333 };
            input.Niconico.BackgroundModels = new[] { 123, 456 };
            input.Niconico.MylistIds = new[] { 1, 2, 3, 4 };
            input.Niconico.BroadcasterComments = new[] { "放送者コメント", "Hogehoge" };
            input.Niconico.NgScoreThreshold = -100;
            input.Background.Panorama.SourceUrls = new[] { "http://background.foo", "http://background.bar" };
            input.PersistentObject.ImageUrls = new[] { "http://image.foo", "http://image.bar" };
            input.PersistentObject.DoubleSidedImageUrls = new[] { new[] { "http://dsfront.bar", "http://dsback.bar" } };
            input.PersistentObject.HiddenImageUrls = new[] { "http://hiddenimage.bar" };
            input.PersistentObject.HiddenDoubleSideImageUrls = new[] { new[] { "http://hdsfront.bar", "http://hdsback.bar" } };
            input.PersistentObject.NicovideoIds = new[] { "NicoVideo1", "NicoVideo2" };
            input.Item.Whiteboard.SourceUrls = new[] { "http://whiteboard.bar" };
            input.Item.CueCard.SourceUrls = new[] { "http://cuecard.bar" };
            input.Item.HideCameraFromViewrs = true;
            input.Item.EnableNicovideoChromakey = true;
            input.Item.EnableDisplaycaptureChromarkey = true;
            input.Item.CaptureFormat = "png";
            input.Item.CaptureResolution = "4KUHD";
            input.Studio.AllowDirectView = true;
            input.Humanoid.UseFastSpringBone = true;
            input.Mode = "direct-view";
            input.DirectViewTalk = true;
            input.EnableLookingGlass = true;
            input.EnableVivesranipalEye = true;
            input.EnableVivesranipalBlink = true;
            input.VivesranipalEyeAdjustX = (decimal)2.0;
            input.VivesranipalEyeAdjustY = (decimal)1.0;
            input.EnableVivesranipalEyeWithEmothion = true;
            input.EnableVivesranipalLip = true;
            input.EmbeddedScript.WebsocketConsolePort = 1111;
            input.EmbeddedScript.VrDebug = true;
            input.EmbeddedScript.MoonsharpDebuggerPort = 2222;

            var result = input.Flatten();

            Assert.IsFalse(result.IsChanged);
            CollectionAssert.AreEqual(new[] { 111, 222, 333 }, result.CharacterModels);
            CollectionAssert.AreEqual(new[] { 123, 456 }, result.BackgroundModels);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, result.MylistIds);
            CollectionAssert.AreEqual(new[] { "放送者コメント", "Hogehoge" }, result.BroadcasterComments);
            Assert.AreEqual(-100, result.NgScoreThreshold);
            CollectionAssert.AreEqual(new[] { new Uri("http://background.foo"), new Uri("http://background.bar") }, result.BackgroundUrls);
            CollectionAssert.AreEqual(new[] { new Uri("http://image.foo"), new Uri("http://image.bar") }, result.ImageUrls);
            CollectionAssert.AreEqual(new[] { new Uri("http://dsfront.bar") }, result.DoubleSidedImageUrls.Select(e => e.FrontSide).ToArray());
            CollectionAssert.AreEqual(new[] { new Uri("http://dsback.bar") }, result.DoubleSidedImageUrls.Select(e => e.BackSide).ToArray());
            CollectionAssert.AreEqual(new[] { new Uri("http://hiddenimage.bar") }, result.HiddenImageUrls);
            CollectionAssert.AreEqual(new[] { new Uri("http://hdsfront.bar") }, result.HiddenDoubleSidedImageUrls.Select(e => e.FrontSide).ToArray());
            CollectionAssert.AreEqual(new[] { new Uri("http://hdsback.bar") }, result.HiddenDoubleSidedImageUrls.Select(e => e.BackSide).ToArray());
            CollectionAssert.AreEqual(new[] { "NicoVideo1", "NicoVideo2" }, result.NicovideoIds);
            CollectionAssert.AreEqual(new[] { new Uri("http://whiteboard.bar") }, result.WhiteboardUrls);
            CollectionAssert.AreEqual(new[] { new Uri("http://cuecard.bar") }, result.CueCardUrls);
            Assert.IsTrue(result.HideCameraFromViewers);
            Assert.IsTrue(result.NicovideoChromaky);
            Assert.IsTrue(result.DisplaycaptureChromaky);
            Assert.IsTrue(result.PngCaptureFormat);
            Assert.AreEqual(CaptureResolution.UHD4K, result.CaptureResolution);
            Assert.IsTrue(result.AllowDirectView);
            Assert.IsTrue(result.UseFastSpringBone);
            Assert.IsTrue(result.DirectViewMode);
            Assert.IsTrue(result.DirectViewTalk);
            Assert.IsTrue(result.LookingGlass);
            Assert.IsTrue(result.VivesranipalEye);
            Assert.IsTrue(result.VivesranipalBlink);
            Assert.AreEqual((decimal)2.0, result.VivesranipalX);
            Assert.AreEqual((decimal)1.0, result.VivesranipalY);
            Assert.IsTrue(result.VivesranipalEyeWithEmothion);
            Assert.IsTrue(result.VivesranipalLip);
            Assert.AreEqual(1111, result.ScriptWebSocketConsolePort);
            Assert.IsTrue(result.ScriptVrDebug);
            Assert.AreEqual(2222, result.ScriptMoonsharpDebuggerPort);
        }

        [TestMethod()]
        public void FlattenTest_各項目null()
        {
            var input = new ConfigJsonStructure();
            var result = input.Flatten();
            Assert.IsFalse(result.CharacterModels.Any());
            Assert.IsFalse(result.BackgroundModels.Any());
            Assert.IsFalse(result.MylistIds.Any());
            Assert.IsFalse(result.BroadcasterComments.Any());
            Assert.IsNull(result.NgScoreThreshold);
            Assert.IsFalse(result.BackgroundUrls.Any());
            Assert.IsFalse(result.ImageUrls.Any());
            Assert.IsFalse(result.DoubleSidedImageUrls.Any());
            Assert.IsFalse(result.HiddenImageUrls.Any());
            Assert.IsFalse(result.HiddenDoubleSidedImageUrls.Any());
            Assert.IsFalse(result.NicovideoIds.Any());
            Assert.IsFalse(result.WhiteboardUrls.Any());
            Assert.IsFalse(result.CueCardUrls.Any());
            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.AreEqual(CaptureResolution.FHD, result.CaptureResolution);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsNull(result.VivesranipalX);
            Assert.IsNull(result.VivesranipalY);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsNull(result.ScriptWebSocketConsolePort);
            Assert.IsFalse(result.ScriptVrDebug);
            Assert.IsNull(result.ScriptMoonsharpDebuggerPort);

        }

        [TestMethod()]
        public void FlattenTest_ネストオブジェクトnull()
        {
            var input = new ConfigJsonStructure();
            input.Niconico = null;
            input.Background = null;
            input.PersistentObject = null;
            input.Item = null;
            input.Studio = null;
            input.Humanoid = null;
            input.EmbeddedScript = null;
            var result = input.Flatten();
            Assert.IsFalse(result.CharacterModels.Any());
            Assert.IsFalse(result.BackgroundModels.Any());
            Assert.IsFalse(result.MylistIds.Any());
            Assert.IsFalse(result.BroadcasterComments.Any());
            Assert.IsNull(result.NgScoreThreshold);
            Assert.IsFalse(result.BackgroundUrls.Any());
            Assert.IsFalse(result.ImageUrls.Any());
            Assert.IsFalse(result.DoubleSidedImageUrls.Any());
            Assert.IsFalse(result.HiddenImageUrls.Any());
            Assert.IsFalse(result.HiddenDoubleSidedImageUrls.Any());
            Assert.IsFalse(result.NicovideoIds.Any());
            Assert.IsFalse(result.WhiteboardUrls.Any());
            Assert.IsFalse(result.CueCardUrls.Any());
            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.AreEqual(CaptureResolution.FHD, result.CaptureResolution);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsNull(result.VivesranipalX);
            Assert.IsNull(result.VivesranipalY);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsNull(result.ScriptWebSocketConsolePort);
            Assert.IsFalse(result.ScriptVrDebug);
            Assert.IsNull(result.ScriptMoonsharpDebuggerPort);

        }

        [TestMethod()]
        public void FlattenTest_ネストオブジェクト2null()
        {
            var input = new ConfigJsonStructure();
            input.Background.Panorama = null;
            input.Item.Whiteboard = null;
            input.Item.CueCard = null;
            var result = input.Flatten();
            Assert.IsFalse(result.CharacterModels.Any());
            Assert.IsFalse(result.BackgroundModels.Any());
            Assert.IsFalse(result.MylistIds.Any());
            Assert.IsFalse(result.BroadcasterComments.Any());
            Assert.IsNull(result.NgScoreThreshold);
            Assert.IsFalse(result.BackgroundUrls.Any());
            Assert.IsFalse(result.ImageUrls.Any());
            Assert.IsFalse(result.DoubleSidedImageUrls.Any());
            Assert.IsFalse(result.HiddenImageUrls.Any());
            Assert.IsFalse(result.HiddenDoubleSidedImageUrls.Any());
            Assert.IsFalse(result.NicovideoIds.Any());
            Assert.IsFalse(result.WhiteboardUrls.Any());
            Assert.IsFalse(result.CueCardUrls.Any());
            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.AreEqual(CaptureResolution.FHD, result.CaptureResolution);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsNull(result.VivesranipalX);
            Assert.IsNull(result.VivesranipalY);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsNull(result.ScriptWebSocketConsolePort);
            Assert.IsFalse(result.ScriptVrDebug);
            Assert.IsNull(result.ScriptMoonsharpDebuggerPort);

        }

        [DataTestMethod()]
        public void FlattenTest_HideCameraFromViewers()
        {
            var input = new ConfigJsonStructure();
            input.Item.HideCameraFromViewrs = true;
            var result = input.Flatten();

            Assert.IsTrue(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_NicovideoChromaky()
        {
            var input = new ConfigJsonStructure();
            input.Item.EnableNicovideoChromakey = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsTrue(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_DisplaycaptureChromaky()
        {
            var input = new ConfigJsonStructure();
            input.Item.EnableDisplaycaptureChromarkey = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsTrue(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_AllowDirectView()
        {
            var input = new ConfigJsonStructure();
            input.Studio.AllowDirectView = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsTrue(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_UseFastSpringBone()
        {
            var input = new ConfigJsonStructure();
            input.Humanoid.UseFastSpringBone = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsTrue(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_DirectViewTalk()
        {
            var input = new ConfigJsonStructure();
            input.DirectViewTalk = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsTrue(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_LookingGlass()
        {
            var input = new ConfigJsonStructure();
            input.EnableLookingGlass = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsTrue(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_VivesranipalEye()
        {
            var input = new ConfigJsonStructure();
            input.EnableVivesranipalEye = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsTrue(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_VivesranipalBlink()
        {
            var input = new ConfigJsonStructure();
            input.EnableVivesranipalBlink = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsTrue(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_VivesranipalEyeWithEmothion()
        {
            var input = new ConfigJsonStructure();
            input.EnableVivesranipalEyeWithEmothion = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsTrue(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_VivesranipalLip()
        {
            var input = new ConfigJsonStructure();
            input.EnableVivesranipalLip = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsTrue(result.VivesranipalLip);
            Assert.IsFalse(result.ScriptVrDebug);
        }

        [DataTestMethod()]
        public void FlattenTest_ScriptVrDebug()
        {
            var input = new ConfigJsonStructure();
            input.EmbeddedScript.VrDebug = true;
            var result = input.Flatten();

            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsFalse(result.VivesranipalEyeWithEmothion);
            Assert.IsFalse(result.VivesranipalLip);
            Assert.IsTrue(result.ScriptVrDebug);
        }

        [TestMethod()]
        public void ToStructureTest_全項目()
        {
            var input = new ConfigJson()
            {
                CharacterModels = new ObservableCollection<int>(new[] { 111, 222 }),
                BackgroundModels = new ObservableCollection<int>(new[] { 123, 456 }),
                MylistIds = new ObservableCollection<int>(new[] { 11, 22 }),
                BroadcasterComments = new ObservableCollection<string>(new[] { "Foo", "Bar" }),
                NgScoreThreshold = -100,
                BackgroundUrls = new ObservableCollection<Uri>(new[] { new Uri("http://background.foo"), new Uri("http://background.bar") }),
                ImageUrls = new ObservableCollection<Uri>(new[] { new Uri("http://image.foo"), new Uri("http://image.bar") }),
                DoubleSidedImageUrls
                    = new ObservableCollection<ConfigJson.DoubleImageUrls>(new[] { new ConfigJson.DoubleImageUrls(new Uri("http://dsfrong.bar"), new Uri("http://dsback.bar")) }),
                HiddenImageUrls = new ObservableCollection<Uri>(new[] { new Uri("http://back.foo") }),
                HiddenDoubleSidedImageUrls
                    = new ObservableCollection<ConfigJson.DoubleImageUrls>(new[] { new ConfigJson.DoubleImageUrls(new Uri("http://hdsfront.bar"), new Uri("http://hdsback.bar")) }),
                NicovideoIds = new ObservableCollection<string>(new[] { "tdfoo" }),
                WhiteboardUrls = new ObservableCollection<Uri>(new[] { new Uri("http://white.foo") }),
                CueCardUrls = new ObservableCollection<Uri>(new[] { new Uri("http://cue.foo") }),
                PngCaptureFormat = false,
                CaptureResolution = CaptureResolution.WQHD,
                HideCameraFromViewers = false,
                DisplaycaptureChromaky = false,
                NicovideoChromaky = false,
                AllowDirectView = false,
                UseFastSpringBone = false,
                DirectViewMode = false,
                DirectViewTalk = false,
                LookingGlass = false,
                VivesranipalEye = false,
                VivesranipalBlink = false,
                VivesranipalX = (decimal)2.0,
                VivesranipalY = (decimal)1.0,
                VivesranipalEyeWithEmothion = false,
                VivesranipalLip = false,
                ScriptWebSocketConsolePort = 1111,
                ScriptVrDebug = false,
                ScriptMoonsharpDebuggerPort = 2222,
            };
            var result = input.ToStructure();

            CollectionAssert.AreEqual(new[] { 111, 222 }, result.Niconico.CharacterModels);
            CollectionAssert.AreEqual(new[] { 123, 456 }, result.Niconico.BackgroundModels);
            CollectionAssert.AreEqual(new[] { 11, 22 }, result.Niconico.MylistIds);
            CollectionAssert.AreEqual(new[] { "Foo", "Bar" }, result.Niconico.BroadcasterComments);
            Assert.AreEqual(-100, result.Niconico.NgScoreThreshold);
            CollectionAssert.AreEqual(new[] { "http://background.foo/", "http://background.bar/" }, result.Background.Panorama.SourceUrls);
            CollectionAssert.AreEqual(new[] { "http://image.foo/", "http://image.bar/" }, result.PersistentObject.ImageUrls);
            CollectionAssert.AreEqual(new[] { "http://dsfrong.bar/" }, result.PersistentObject.DoubleSidedImageUrls.Select(e => e[0]).ToArray());
            CollectionAssert.AreEqual(new[] { "http://dsback.bar/" }, result.PersistentObject.DoubleSidedImageUrls.Select(e => e[1]).ToArray());
            CollectionAssert.AreEqual(new[] { "http://back.foo/" }, result.PersistentObject.HiddenImageUrls);
            CollectionAssert.AreEqual(new[] { "http://hdsfront.bar/" }, result.PersistentObject.HiddenDoubleSideImageUrls.Select(e => e[0]).ToArray());
            CollectionAssert.AreEqual(new[] { "http://hdsback.bar/" }, result.PersistentObject.HiddenDoubleSideImageUrls.Select(e => e[1]).ToArray());
            CollectionAssert.AreEqual(new[] { "tdfoo" }, result.PersistentObject.NicovideoIds);
            CollectionAssert.AreEqual(new[] { "http://white.foo/" }, result.Item.Whiteboard.SourceUrls);
            CollectionAssert.AreEqual(new[] { "http://cue.foo/" }, result.Item.CueCard.SourceUrls);
            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.AreEqual("WQHD", result.Item.CaptureResolution);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.AreEqual((decimal)2.0, result.VivesranipalEyeAdjustX);
            Assert.AreEqual((decimal)1.0, result.VivesranipalEyeAdjustY);
            Assert.AreEqual(1111, result.EmbeddedScript.WebsocketConsolePort);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);
            Assert.AreEqual(2222, result.EmbeddedScript.MoonsharpDebuggerPort);

        }

        [TestMethod()]
        public void ToStructureTest_HideCameraFromViewers()
        {
            var input = new ConfigJson()
            {
                HideCameraFromViewers = true,
            };
            var result = input.ToStructure();

            Assert.IsTrue(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_DisplaycaptureChromaky()
        {
            var input = new ConfigJson()
            {
                DisplaycaptureChromaky = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsTrue(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_NicovideoChromaky()
        {
            var input = new ConfigJson()
            {
                NicovideoChromaky = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsTrue(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_PngCaptureFormat()
        {
            var input = new ConfigJson()
            {
                PngCaptureFormat = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.AreEqual("PNG", result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_AllowDirectView()
        {
            var input = new ConfigJson()
            {
                AllowDirectView = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsTrue(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_UseFastSpringBone()
        {
            var input = new ConfigJson()
            {
                UseFastSpringBone = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsTrue(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_DirectViewMode()
        {
            var input = new ConfigJson()
            {
                DirectViewMode = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.AreEqual(ConfigJsonStructure.DirectViewModeKey, result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_DirectViewTalk()
        {
            var input = new ConfigJson()
            {
                DirectViewTalk = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsTrue(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_LookingGlass()
        {
            var input = new ConfigJson()
            {
                LookingGlass = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsTrue(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_VivesranipalEye()
        {
            var input = new ConfigJson()
            {
                VivesranipalEye = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsTrue(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_VivesranipalBlink()
        {
            var input = new ConfigJson()
            {
                VivesranipalBlink = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsTrue(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_VivesranipalEyeWithEmothion()
        {
            var input = new ConfigJson()
            {
                VivesranipalEyeWithEmothion = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsTrue(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_VivesranipalLip()
        {
            var input = new ConfigJson()
            {
                VivesranipalLip = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsTrue(result.EnableVivesranipalLip);
            Assert.IsFalse(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ToStructureTest_ScriptVrDebug()
        {
            var input = new ConfigJson()
            {
                ScriptVrDebug = true,
            };
            var result = input.ToStructure();

            Assert.IsFalse(result.Item.HideCameraFromViewrs);
            Assert.IsFalse(result.Item.EnableDisplaycaptureChromarkey);
            Assert.IsFalse(result.Item.EnableNicovideoChromakey);
            Assert.IsNull(result.Item.CaptureFormat);
            Assert.IsFalse(result.Studio.AllowDirectView);
            Assert.IsFalse(result.Humanoid.UseFastSpringBone);
            Assert.IsNull(result.Mode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.EnableLookingGlass);
            Assert.IsFalse(result.EnableVivesranipalEye);
            Assert.IsFalse(result.EnableVivesranipalBlink);
            Assert.IsFalse(result.EnableVivesranipalEyeWithEmothion);
            Assert.IsFalse(result.EnableVivesranipalLip);
            Assert.IsTrue(result.EmbeddedScript.VrDebug);

        }

        [TestMethod()]
        public void ParseConfigJsonTest_全項目()
        {
            var result = JsonStrings.TemplateString.ParseConfigJson();

            Assert.IsFalse(result.IsChanged);
            CollectionAssert.AreEqual(new[] { 1, 11 }, result.CharacterModels);
            CollectionAssert.AreEqual(new[] { 2 }, result.BackgroundModels);
            CollectionAssert.AreEqual(new[] { 3 }, result.MylistIds);
            CollectionAssert.AreEqual(new[] { "コメント" }, result.BroadcasterComments);
            Assert.AreEqual(-50, result.NgScoreThreshold);
            CollectionAssert.AreEqual(new[] { new Uri("http://bg.foo") }, result.BackgroundUrls);
            CollectionAssert.AreEqual(new[] { new Uri("http://image.foo") }, result.ImageUrls);
            CollectionAssert.AreEqual(new[] { new Uri("http://dsfront.bar") }, result.DoubleSidedImageUrls.Select(e => e.FrontSide).ToArray());
            CollectionAssert.AreEqual(new[] { new Uri("http://dsback.bar") }, result.DoubleSidedImageUrls.Select(e => e.BackSide).ToArray());
            CollectionAssert.AreEqual(new[] { new Uri("http://hidden.foo") }, result.HiddenImageUrls);
            CollectionAssert.AreEqual(new[] { new Uri("http://hdsfront.bar") }, result.HiddenDoubleSidedImageUrls.Select(e => e.FrontSide).ToArray());
            CollectionAssert.AreEqual(new[] { new Uri("http://hdsback.bar") }, result.HiddenDoubleSidedImageUrls.Select(e => e.BackSide).ToArray());
            CollectionAssert.AreEqual(new[] { "sm6" }, result.NicovideoIds);
            CollectionAssert.AreEqual(new[] { new Uri("http://white.foo") }, result.WhiteboardUrls);
            CollectionAssert.AreEqual(new[] { new Uri("http://cue.foo") }, result.CueCardUrls);
            Assert.IsFalse(result.HideCameraFromViewers);
            Assert.IsFalse(result.NicovideoChromaky);
            Assert.IsFalse(result.DisplaycaptureChromaky);
            Assert.IsTrue(result.PngCaptureFormat);
            Assert.IsFalse(result.AllowDirectView);
            Assert.IsFalse(result.UseFastSpringBone);
            Assert.IsTrue(result.DirectViewMode);
            Assert.IsFalse(result.DirectViewTalk);
            Assert.IsFalse(result.LookingGlass);
            Assert.IsFalse(result.VivesranipalEye);
            Assert.IsFalse(result.VivesranipalBlink);
            Assert.IsNull(result.VivesranipalX);
            Assert.IsNull(result.VivesranipalY);
            Assert.AreEqual(8080, result.ScriptWebSocketConsolePort);
            Assert.IsFalse(result.ScriptVrDebug);
            Assert.AreEqual(8888, result.ScriptMoonsharpDebuggerPort);
            Assert.AreEqual(JsonStrings.TemplateString, result.OriginalJson);
        }

        [TestMethod()]
        public void ParseConfigJsonTest_bool()
        {
            var result = JsonStrings.TrueString.ParseConfigJson();

            Assert.IsFalse(result.IsChanged);

            Assert.IsTrue(result.HideCameraFromViewers);
            Assert.IsTrue(result.NicovideoChromaky);
            Assert.IsTrue(result.DisplaycaptureChromaky);
            Assert.IsFalse(result.PngCaptureFormat);
            Assert.IsTrue(result.AllowDirectView);
            Assert.IsTrue(result.UseFastSpringBone);
            Assert.IsFalse(result.DirectViewMode);
            Assert.IsTrue(result.DirectViewTalk);
            Assert.IsTrue(result.LookingGlass);
            Assert.IsTrue(result.VivesranipalEye);
            Assert.IsTrue(result.VivesranipalBlink);
            Assert.AreEqual((decimal)2.0, result.VivesranipalX);
            Assert.AreEqual((decimal)1.0, result.VivesranipalY);
            Assert.IsTrue(result.ScriptVrDebug);
        }

        [TestMethod()]
        [ExpectedException(typeof(JsonException), AllowDerivedTypes = true)]
        public void ParseConfigJsonTest_JsonException()
        {
            var _ = JsonStrings.BadString.ParseConfigJson();
        }

        [TestMethod()]
        public void SerializeToJsonTest_マージ無し()
        {
            var input = JsonStrings.TemplateString.ParseConfigJson();
            var result = input.SerializeToJson(false);

            var expected = Regex.Replace(JsonStrings.ExpectedString, @"\s", "");
            result = Regex.Replace(result, @"\s", "");
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void SerializeToJsonTest_マージ有り()
        {
            var input = JsonStrings.MergeString.ParseConfigJson();
            input.CharacterModels.Clear();
            input.CharacterModels.Add(222);
            input.CharacterModels.Add(333);
            input.BackgroundModels.Clear();
            var result = input.SerializeToJson(true);

            var expected = Regex.Replace(JsonStrings.MergeExpectedString, @"\s", "");
            result = Regex.Replace(result, @"\s", "");
            Assert.AreEqual(expected, result);
        }
    }
}
