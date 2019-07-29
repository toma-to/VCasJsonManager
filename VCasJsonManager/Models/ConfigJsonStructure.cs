//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Newtonsoft.Json;

namespace VCasJsonManager.Models
{
    /// <summary>
    /// config.jsonの構造に合わせてデータを保持するクラス。
    /// config.jsonのシリアライズ/デシリアライズ用。
    /// </summary>
    public sealed class ConfigJsonStructure
    {
        public sealed class NiconicoStructure
        {
            [JsonProperty("character_models")]
            public int[] CharacterModels { get; set; }

            [JsonProperty("background_models")]
            public int[] BackgroundModels { get; set; }

            [JsonProperty("niconare_ids")]
            public int[] NiconareIds { get; set; }

            [JsonProperty("mylist_ids")]
            public int[] MylistIds { get; set; }
            
            [JsonProperty("broadcaster_comments")]
            public string[] BroadcasterComments { get; set; }

            [JsonProperty("ng_score_threshold")]
            public int? NgScoreThreshold { get; set; }
        }

        [JsonProperty("niconico")]
        public NiconicoStructure Niconico { get; set; } = new NiconicoStructure();

        public sealed class BackgroundStructure
        {
            public class PanoramaStructure
            {
                [JsonProperty("source_urls")]
                public string[] SourceUrls { get; set; }
            }
            [JsonProperty("panorama")]
            public PanoramaStructure Panorama { get; set; } = new PanoramaStructure();
        }

        [JsonProperty("background")]
        public BackgroundStructure Background { get; set; } = new BackgroundStructure();

        public sealed class PersistentObjectStructure
        {
            [JsonProperty("image_urls")]
            public string[] ImageUrls { get; set; }

            [JsonProperty("double_sided_image_urls")]
            public string[][] DoubleSidedImageUrls { get; set; }

            [JsonProperty("hidden_image_urls")]
            public string[] HiddenImageUrls { get; set; }

            [JsonProperty("hidden_double_sided_image_urls")]
            public string[][] HiddenDoubleSideImageUrls { get; set; }

            [JsonProperty("nicovideo_ids")]
            public string[] NicovideoIds { get; set; }
        }

        [JsonProperty("persistent_object")]
        public PersistentObjectStructure PersistentObject { get; set; } = new PersistentObjectStructure();

        public sealed class ItemStructure
        {
            public sealed class WhiteboardStructure
            {
                [JsonProperty("source_urls")]
                public string[] SourceUrls { get; set; }
            }

            [JsonProperty("whiteboard")]
            public WhiteboardStructure Whiteboard { get; set; } = new WhiteboardStructure();

            public sealed class CueCardStructure
            {
                [JsonProperty("source_urls")]
                public string[] SourceUrls { get; set; }
            }

            [JsonProperty("cue_card")]
            public CueCardStructure CueCard { get; set; } = new CueCardStructure();

            [JsonProperty("hide_camera_from_viewers")]
            public bool HideCameraFromViewrs { get; set; }

            [JsonProperty("enable_displaycapture_chromakey")]
            public bool EnableDisplaycaptureChromarkey { get; set; }

            [JsonProperty("enable_nicovideo_chromakey")]
            public bool EnableNicovideoChromakey { get; set; }

            [JsonProperty("capture_format")]
            public string CaptureFormat { get; set; }
        }

        [JsonIgnore]
        public static string CaptureFormatPngKey { get; } = "PNG";

        [JsonProperty("item")]
        public ItemStructure Item { get; set; } = new ItemStructure();

        public sealed class StudioStructure
        {
            [JsonProperty("allow_direct_view")]
            public bool AllowDirectView { get; set; }
        }

        [JsonProperty("studio")]
        public StudioStructure Studio { get; set; } = new StudioStructure();

        public sealed class HumanoidStructure
        {
            [JsonProperty("use_fast_spring_bone")]
            public bool UseFastSpringBone { get; set; }
        }

        [JsonProperty("humanoid")]
        public HumanoidStructure Humanoid { get; set; } = new HumanoidStructure();

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonIgnore]
        public static string DirectViewModeKey { get; } = "direct-view";

        [JsonProperty("direct_view_talk")]
        public bool DirectViewTalk { get; set; }

        [JsonProperty("enable_looking_glass")]
        public bool EnableLookingGlass { get; set; }

        [JsonProperty("enable_vivesranipal_eye")]
        public bool EnableVivesranipalEye { get; set; }

        [JsonProperty("enable_vivesranipal_blink")]
        public bool EnableVivesranipalBlink { get; set; }

        [JsonProperty("vivesranipal_eye_adjust_x")]
        public decimal? VivesranipalEyeAdjustX { get; set; }

        [JsonProperty("vivesranipal_eye_adjust_y")]
        public decimal? VivesranipalEyeAdjustY { get; set; }

        public sealed class EmbeddedScriptStructure
        {
            [JsonProperty("websocket_console_port")]
            public int? WebsocketConsolePort { get; set; }

            [JsonProperty("vr_debug")]
            public bool VrDebug { get; set; }

            [JsonProperty("moonsharp_debugger_port")]
            public int? MoonsharpDebuggerPort { get; set; }
        }

        [JsonProperty("embedded_script")]
        public EmbeddedScriptStructure EmbeddedScript { get; set; } = new EmbeddedScriptStructure();
    }
}
