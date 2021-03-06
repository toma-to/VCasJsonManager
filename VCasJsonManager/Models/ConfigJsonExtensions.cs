﻿//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace VCasJsonManager.Models
{
    /// <summary>
    /// ConfigJsonの拡張メソッド定義
    /// </summary>
    public static class ConfigJsonExtensions
    {
        /// <summary>
        /// デシリアライズされたconfig.json構造を平坦化する
        /// </summary>
        /// <param name="self">config.json</param>
        /// <returns>config.jsonを平坦化したオブジェクト</returns>
        public static ConfigJson Flatten(this ConfigJsonStructure self)
        {

            var ret = new ConfigJson()
            {
                CharacterModels = new ObservableCollection<int>(self.Niconico?.CharacterModels ?? new int[0]),
                BackgroundModels = new ObservableCollection<int>(self.Niconico?.BackgroundModels ?? new int[0]),
                MylistIds = new ObservableCollection<int>(self.Niconico?.MylistIds ?? new int[0]),
                BroadcasterComments = new ObservableCollection<string>(self.Niconico?.BroadcasterComments ?? new string[0]),
                NgScoreThreshold = self.Niconico?.NgScoreThreshold,
                BackgroundUrls = new ObservableCollection<Uri>(self.Background?.Panorama?.SourceUrls?.ToUriList() ?? new Uri[0]),
                ImageUrls = new ObservableCollection<Uri>(self.PersistentObject?.ImageUrls?.ToUriList() ?? new Uri[0]),
                DoubleSidedImageUrls = new ObservableCollection<ConfigJson.DoubleImageUrls>(
                                        self.PersistentObject?.DoubleSidedImageUrls?.ToDoubleImageUrl() ?? new ConfigJson.DoubleImageUrls[0]),
                HiddenImageUrls = new ObservableCollection<Uri>(self.PersistentObject?.HiddenImageUrls?.ToUriList() ?? new Uri[0]),
                HiddenDoubleSidedImageUrls = new ObservableCollection<ConfigJson.DoubleImageUrls>(
                                        self.PersistentObject?.HiddenDoubleSideImageUrls?.ToDoubleImageUrl() ?? new ConfigJson.DoubleImageUrls[0]),
                NicovideoIds = new ObservableCollection<string>(self.PersistentObject?.NicovideoIds ?? new string[0]),
                WhiteboardUrls = new ObservableCollection<Uri>(self.Item?.Whiteboard?.SourceUrls?.ToUriList() ?? new Uri[0]),
                CueCardUrls = new ObservableCollection<Uri>(self.Item?.CueCard?.SourceUrls?.ToUriList() ?? new Uri[0]),
                HideCameraFromViewers = self.Item?.HideCameraFromViewrs ?? false,
                DisplaycaptureChromaky = self.Item?.EnableDisplaycaptureChromarkey ?? false,
                NicovideoChromaky = self.Item?.EnableNicovideoChromakey ?? false,
                PngCaptureFormat = ConfigJsonStructure.CaptureFormatPngKey.Equals(self.Item?.CaptureFormat, StringComparison.OrdinalIgnoreCase),
                CaptureResolution = self.Item?.CaptureResolution.FromResolutionString() ?? CaptureResolution.FHD,
                AllowDirectView = self.Studio?.AllowDirectView ?? false,
                UseFastSpringBone = self.Humanoid?.UseFastSpringBone ?? false,
                DirectViewMode = self.Mode == ConfigJsonStructure.DirectViewModeKey,
                DirectViewTalk = self.DirectViewTalk,
                LookingGlass = self.EnableLookingGlass,
                VivesranipalEye = self.EnableVivesranipalEye,
                VivesranipalBlink = self.EnableVivesranipalBlink,
                VivesranipalX = self.VivesranipalEyeAdjustX,
                VivesranipalY = self.VivesranipalEyeAdjustY,
                VivesranipalEyeWithEmothion = self.EnableVivesranipalEyeWithEmothion,
                VivesranipalLip = self.EnableVivesranipalLip,
                ScriptWebSocketConsolePort = self.EmbeddedScript?.WebsocketConsolePort,
                ScriptVrDebug = self.EmbeddedScript?.VrDebug ?? false,
                ScriptMoonsharpDebuggerPort = self.EmbeddedScript?.MoonsharpDebuggerPort,
            };
            ret.IsChanged = false;
            return ret;
        }

        /// <summary>
        /// シリアライズ用のconfig.json構造を構築する
        /// </summary>
        /// <param name="self">構築するConfigJson情報</param>
        /// <returns>config.json</returns>
        public static ConfigJsonStructure ToStructure(this ConfigJson self)
        {
            var ret = new ConfigJsonStructure();
            ret.Niconico.CharacterModels = self.CharacterModels.ToArray();
            ret.Niconico.BackgroundModels = self.BackgroundModels.ToArray();
            ret.Niconico.MylistIds = self.MylistIds.ToArray();
            ret.Niconico.BroadcasterComments = self.BroadcasterComments.ToArray();
            ret.Niconico.NgScoreThreshold = self.NgScoreThreshold;
            ret.Background.Panorama.SourceUrls = self.BackgroundUrls.Select(e => e.ToString()).ToArray();
            ret.PersistentObject.ImageUrls = self.ImageUrls.Select(e => e.ToString()).ToArray();
            ret.PersistentObject.DoubleSidedImageUrls
                    = self.DoubleSidedImageUrls.Select(e => new[] { e.FrontSide.ToString(), e.BackSide.ToString() }).ToArray();
            ret.PersistentObject.HiddenImageUrls = self.HiddenImageUrls.Select(e => e.ToString()).ToArray();
            ret.PersistentObject.HiddenDoubleSideImageUrls
                    = self.HiddenDoubleSidedImageUrls.Select(e => new[] { e.FrontSide.ToString(), e.BackSide.ToString() }).ToArray();
            ret.PersistentObject.NicovideoIds = self.NicovideoIds.ToArray();
            ret.Item.Whiteboard.SourceUrls = self.WhiteboardUrls.Select(e => e.ToString()).ToArray();
            ret.Item.CueCard.SourceUrls = self.CueCardUrls.Select(e => e.ToString()).ToArray();
            ret.Item.HideCameraFromViewrs = self.HideCameraFromViewers;
            ret.Item.EnableDisplaycaptureChromarkey = self.DisplaycaptureChromaky;
            ret.Item.EnableNicovideoChromakey = self.NicovideoChromaky;
            ret.Item.CaptureFormat = self.PngCaptureFormat ? ConfigJsonStructure.CaptureFormatPngKey : null;
            ret.Item.CaptureResolution = self.CaptureResolution.ToResolutionString();
            ret.Studio.AllowDirectView = self.AllowDirectView;
            ret.Humanoid.UseFastSpringBone = self.UseFastSpringBone;
            ret.Mode = self.DirectViewMode ? ConfigJsonStructure.DirectViewModeKey : null;
            ret.DirectViewTalk = self.DirectViewTalk;
            ret.EnableLookingGlass = self.LookingGlass;
            ret.EnableVivesranipalEye = self.VivesranipalEye;
            ret.EnableVivesranipalBlink = self.VivesranipalBlink;
            ret.VivesranipalEyeAdjustX = self.VivesranipalX;
            ret.VivesranipalEyeAdjustY = self.VivesranipalY;
            ret.EnableVivesranipalEyeWithEmothion = self.VivesranipalEyeWithEmothion;
            ret.EnableVivesranipalLip = self.VivesranipalLip;
            ret.EmbeddedScript.WebsocketConsolePort = self.ScriptWebSocketConsolePort;
            ret.EmbeddedScript.VrDebug = self.ScriptVrDebug;
            ret.EmbeddedScript.MoonsharpDebuggerPort = self.ScriptMoonsharpDebuggerPort;

            return ret;
        }

        /// <summary>
        /// JSON文字列をパースして、ConfigJsonオブジェクトを生成する。
        /// </summary>
        /// <param name="self">JSON文字列</param>
        /// <returns>ConfigJsonオブジェクト</returns>
        /// <exception cref="JsonException"></exception>
        public static ConfigJson ParseConfigJson(this string self)
        {
            var json = JsonConvert.DeserializeObject<ConfigJsonStructure>(self);
            var ret = json.Flatten();
            ret.OriginalJson = self;
            return ret;
        }

        /// <summary>
        /// ConfigJsonオブジェクトをJSON文字列にシリアライズする
        /// </summary>
        /// <param name="self">ConfigJsonオブジェクト</param>
        /// <param name="mergeUnknown">不明なプロパティをマージする場合true</param>
        /// <returns>JSON文字列</returns>
        /// <exception cref="JsonException"></exception>
        public static string SerializeToJson(this ConfigJson self, bool mergeUnknown)
        {
            var obj = self.ToStructure();
            var jobj = JObject.Parse(JsonConvert.SerializeObject(obj));
            if (mergeUnknown && !string.IsNullOrEmpty(self.OriginalJson))
            {
                var org = JObject.Parse(self.OriginalJson);
                org.Merge(jobj, new JsonMergeSettings()
                {
                    MergeArrayHandling = MergeArrayHandling.Replace,
                    MergeNullValueHandling = MergeNullValueHandling.Merge,
                });
                jobj = org;
            }

            return jobj.ToString();
        }
    }
}
