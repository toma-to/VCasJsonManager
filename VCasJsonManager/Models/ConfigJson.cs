//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace VCasJsonManager.Models
{
    /// <summary>
    /// config.jsonのを平坦化してデータを保持するクラス
    /// </summary>
    public class ConfigJson : INotifyPropertyChanged
    {
        /// <summary>
        /// 両面画像のURIを保持するクラス
        /// </summary>
        public sealed class DoubleImageUrls
        {
            /// <summary>
            /// 前面画像URI
            /// </summary>
            public Uri FrontSide { get; }

            /// <summary>
            /// 背面画像URI
            /// </summary>
            public Uri BackSide { get; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="front">前面画像URI</param>
            /// <param name="back">背面画像URI</param>
            public DoubleImageUrls(Uri front, Uri back)
            {
                FrontSide = front;
                BackSide = back;
            }
        }

        /// <summary>
        /// PropertyChangedイベントハンドラ
        /// </summary>
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        /// <summary>
        /// niconico.charactr_models
        /// </summary>
        public ObservableCollection<int> CharacterModels { get; set; } = new ObservableCollection<int>();

        /// <summary>
        /// niconico.background_models
        /// </summary>
        public ObservableCollection<int> BackgroundModels { get; set; } = new ObservableCollection<int>();

        /// <summary>
        /// niconico.niconare_ids
        /// </summary>
        public ObservableCollection<int> NiconareIds { get; set; } = new ObservableCollection<int>();

        /// <summary>
        /// niconico.mylist_ids
        /// </summary>
        public ObservableCollection<int> MylistIds { get; set; } = new ObservableCollection<int>();

        /// <summary>
        /// niconico.broadcaster_comments
        /// </summary>
        public ObservableCollection<string> BroadcasterComments { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// niconico.ng_score_threshld
        /// </summary>
        public int? NgScoreThreshold { get; set; } = null;

        /// <summary>
        /// background.panorama.source_urls
        /// </summary>
        public ObservableCollection<Uri> BackgroundUrls { get; set; } = new ObservableCollection<Uri>();

        /// <summary>
        /// persistent_object.image_urls
        /// </summary>
        public ObservableCollection<Uri> ImageUrls { get; set; } = new ObservableCollection<Uri>();

        /// <summary>
        /// persistent_object.double_sided_image_urls
        /// </summary>
        public ObservableCollection<DoubleImageUrls> DoubleSidedImageUrls { get; set; } = new ObservableCollection<DoubleImageUrls>();

        /// <summary>
        /// persistent_object.hidden_image_urls
        /// </summary>
        public ObservableCollection<Uri> HiddenImageUrls { get; set; } = new ObservableCollection<Uri>();

        /// <summary>
        /// persisitent_object.hidden_double_sided_image_urls
        /// </summary>
        public ObservableCollection<DoubleImageUrls> HiddenDoubleSidedImageUrls { get; set; } = new ObservableCollection<DoubleImageUrls>();

        /// <summary>
        /// presistent_object.nicovideo_ids
        /// </summary>
        public ObservableCollection<string> NicovideoIds { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// item.whiteboard.source_urls
        /// </summary>
        public ObservableCollection<Uri> WhiteboardUrls { get; set; } = new ObservableCollection<Uri>();

        /// <summary>
        /// item.cue_card.source_urls
        /// </summary>
        public ObservableCollection<Uri> CueCardUrls { get; set; } = new ObservableCollection<Uri>();

        /// <summary>
        /// item.hide_camera_from_viewers
        /// </summary>
        public bool HideCameraFromViewers { get; set; }

        /// <summary>
        /// item.enable_displaycapture_chromaky
        /// </summary>
        public bool DisplaycaptureChromaky { get; set; }

        /// <summary>
        /// item.enable_nicovideo_chromaky
        /// </summary>
        public bool NicovideoChromaky { get; set; }

        /// <summary>
        /// item.capture_format
        /// </summary>
        public bool PngCaptureFormat { get; set; }

        /// <summary>
        /// studio.allow_direct_view
        /// </summary>
        public bool AllowDirectView { get; set; }

        /// <summary>
        /// humanoid.use_fast_spring_bone
        /// </summary>
        public bool UseFastSpringBone { get; set; }

        /// <summary>
        /// mode
        /// </summary>
        public bool DirectViewMode { get; set; }

        /// <summary>
        /// direct_view_talk
        /// </summary>
        public bool DirectViewTalk { get; set; }

        /// <summary>
        /// enable_looking_glass
        /// </summary>
        public bool LookingGlass { get; set; }

        /// <summary>
        /// embedded_script.websocket_console_port
        /// </summary>
        public int? ScriptWebSocketConsolePort { get; set; }

        /// <summary>
        /// embedded_script.vr_debug
        /// </summary>
        public bool ScriptVrDebug { get; set; }

        /// <summary>
        /// embedded_script.moonsharp_debugger_port
        /// </summary>
        public int? ScriptMoonsharpDebuggerPort { get; set; } = 41912;

        /// <summary>
        /// 編集前のJSONの文字列
        /// </summary>
        [DoNotNotify]
        [DoNotSetChanged]
        public string OriginalJson { get; set; }

        /// <summary>
        /// ダーティフラグ
        /// </summary>
        public bool IsChanged { get; set; } = false;
    }
}
