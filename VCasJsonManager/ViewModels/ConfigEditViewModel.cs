//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet.EventListeners;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using VCasJsonManager.Models;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    public class ConfigEditViewModel : PropertyChangedMappingViewModelBase
    {
        /// <summary>
        /// ConfigJsonService
        /// </summary>
        private IConfigJsonService ConfigJsonService { get; }

        /// <summary>
        /// ConfigJson
        /// </summary>
        private ConfigJson ConfigJson => ConfigJsonService.ConfigJson;

        /// <summary>
        /// キャラクターモデル
        /// </summary>
        public ObservableCollection<int> CharacterModels => ConfigJson.CharacterModels;

        /// <summary>
        /// 背景モデル
        /// </summary>
        public ObservableCollection<int> BackgroundModels => ConfigJson.BackgroundModels;

        /// <summary>
        /// マイリストID
        /// </summary>
        public ObservableCollection<int> MylistIds => ConfigJson.MylistIds;

        /// <summary>
        /// 運営コメント
        /// </summary>
        public ObservableCollection<string> Comments => ConfigJson.BroadcasterComments;

        /// <summary>
        /// 背景画像
        /// </summary>
        public ObservableCollection<Uri> BackgroundImages => ConfigJson.BackgroundUrls;

        /// <summary>
        /// 表示画像
        /// </summary>
        public ObservableCollection<Uri> Images => ConfigJson.ImageUrls;

        /// <summary>
        /// 両面画像
        /// </summary>
        public ObservableCollection<ConfigJson.DoubleImageUrls> DoubleSided => ConfigJson.DoubleSidedImageUrls;

        /// <summary>
        /// 非表示画像
        /// </summary>
        public ObservableCollection<Uri> HiddenImages => ConfigJson.HiddenImageUrls;

        /// <summary>
        /// 非表示両面画像
        /// </summary>
        public ObservableCollection<ConfigJson.DoubleImageUrls> HiddenDouble => ConfigJson.HiddenDoubleSidedImageUrls;

        /// <summary>
        /// ニコニコ動画ID
        /// </summary>
        public ObservableCollection<string> NicovideoIds => ConfigJson.NicovideoIds;

        /// <summary>
        /// ホワイトボード画像
        /// </summary>
        public ObservableCollection<Uri> Whiteboard => ConfigJson.WhiteboardUrls;

        /// <summary>
        /// カンペ画像設定数
        /// </summary>
        public ObservableCollection<Uri> CueCard => ConfigJson.CueCardUrls;

        /// <summary>
        /// NG閾値
        /// </summary>
        [DoNotNotify]
        public string NgScoreThreshold
        {
            get => ConfigJson.NgScoreThreshold?.ToString();
            set
            {
                if (int.TryParse(value, out var i))
                {
                    ConfigJson.NgScoreThreshold = i;
                }
                else
                {
                    ConfigJson.NgScoreThreshold = null;
                }
            }
        }

        /// <summary>
        /// カメラ非表示
        /// </summary>
        [DoNotNotify]
        public bool HideCameraFromViewers
        {
            get => ConfigJson.HideCameraFromViewers;
            set => ConfigJson.HideCameraFromViewers = value;
        }

        /// <summary>
        /// ディスプレイクロマキー
        /// </summary>
        [DoNotNotify]
        public bool DisplaycaptureChromaky
        {
            get => ConfigJson.DisplaycaptureChromaky;
            set => ConfigJson.DisplaycaptureChromaky = value;
        }

        /// <summary>
        /// 動画プレーヤークロマキー
        /// </summary>
        [DoNotNotify]
        public bool NicovideoChromaky
        {
            get => ConfigJson.NicovideoChromaky;
            set => ConfigJson.NicovideoChromaky = value;
        }

        /// <summary>
        /// スプリングボーン高速化
        /// </summary>
        [DoNotNotify]
        public bool UseFastSpringBone
        {
            get => ConfigJson.UseFastSpringBone;
            set => ConfigJson.UseFastSpringBone = value;
        }

        /// <summary>
        /// キャプチャ画像のPNG保存
        /// </summary>
        [DoNotNotify]
        public bool PngCaptureFormat
        {
            get => ConfigJson.PngCaptureFormat;
            set => ConfigJson.PngCaptureFormat = value;
        }

        /// <summary>
        /// キャプチャ解像度
        /// </summary>
        [DoNotNotify]
        public CaptureResolution CaptureResolution
        {
            get => ConfigJson.CaptureResolution;
            set => ConfigJson.CaptureResolution = value;
        }

        /// <summary>
        /// VivesranipalEye
        /// </summary>
        [DoNotNotify]
        public bool VivesranipalEye { get => ConfigJson.VivesranipalEye; set => ConfigJson.VivesranipalEye = value; }

        /// <summary>
        /// VivesranipalBlink
        /// </summary>
        [DoNotNotify]
        public bool VivesranipalBlink { get => ConfigJson.VivesranipalBlink; set => ConfigJson.VivesranipalBlink = value; }

        /// <summary>
        /// VivesranipalX
        /// </summary>
        [DoNotNotify]
        public string VivesranipalX
        {
            get => ConfigJson.VivesranipalX?.ToString();
            set
            {
                if (decimal.TryParse(value, out var wk))
                {
                    ConfigJson.VivesranipalX = wk;
                }
                else
                {
                    ConfigJson.VivesranipalX = null;
                }
            }
        }

        /// <summary>
        /// VivesranipalY
        /// </summary>
        [DoNotNotify]
        public string VivesranipalY
        {
            get => ConfigJson.VivesranipalY?.ToString();
            set
            {
                if (decimal.TryParse(value, out var wk))
                {
                    ConfigJson.VivesranipalY = wk;
                }
                else
                {
                    ConfigJson.VivesranipalY = null;
                }
            }
        }

        /// <summary>
        /// VivesranipalEyeWithEmothion
        /// </summary>
        public bool VivesranipalEyeWithEmothion
        {
            get => ConfigJson.VivesranipalEyeWithEmothion;
            set => ConfigJson.VivesranipalEyeWithEmothion = value;
        }

        /// <summary>
        /// VivesranipalLip
        /// </summary>
        public bool VivesranipalLip { get => ConfigJson.VivesranipalLip; set => ConfigJson.VivesranipalLip = value; }

        /// <summary>
        /// デバッグモード
        /// </summary>
        public bool VrDebug => ConfigJson.ScriptVrDebug;

        /// <summary>
        /// ConfigJsonのイベントリスナ
        /// </summary>
        private PropertyChangedEventListener ConfigJsonListner { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configJsonService"></param>
        public ConfigEditViewModel(IConfigJsonService configJsonService)
        {
            ConfigJsonService = configJsonService;

            AddMapping(nameof(ConfigJson.CharacterModels), nameof(CharacterModels));
            AddMapping(nameof(ConfigJson.BackgroundModels), nameof(BackgroundModels));
            AddMapping(nameof(ConfigJson.MylistIds), nameof(MylistIds));
            AddMapping(nameof(ConfigJson.BroadcasterComments), nameof(Comments));
            AddMapping(nameof(ConfigJson.BackgroundUrls), nameof(BackgroundImages));
            AddMapping(nameof(ConfigJson.ImageUrls), nameof(Images));
            AddMapping(nameof(ConfigJson.DoubleSidedImageUrls), nameof(DoubleSided));
            AddMapping(nameof(ConfigJson.HiddenImageUrls), nameof(HiddenImages));
            AddMapping(nameof(ConfigJson.HiddenDoubleSidedImageUrls), nameof(HiddenDouble));
            AddMapping(nameof(ConfigJson.WhiteboardUrls), nameof(Whiteboard));
            AddMapping(nameof(ConfigJson.CueCardUrls), nameof(CueCard));
            AddMapping(nameof(ConfigJson.NgScoreThreshold));
            AddMapping(nameof(ConfigJson.HideCameraFromViewers));
            AddMapping(nameof(ConfigJson.DisplaycaptureChromaky));
            AddMapping(nameof(ConfigJson.NicovideoChromaky));
            AddMapping(nameof(ConfigJson.UseFastSpringBone));
            AddMapping(nameof(ConfigJson.PngCaptureFormat));
            AddMapping(nameof(ConfigJson.CaptureResolution));
            AddMapping(nameof(ConfigJson.VivesranipalEye));
            AddMapping(nameof(ConfigJson.VivesranipalBlink));
            AddMapping(nameof(ConfigJson.VivesranipalX));
            AddMapping(nameof(ConfigJson.VivesranipalY));
            AddMapping(nameof(ConfigJson.VivesranipalEyeWithEmothion));
            AddMapping(nameof(ConfigJson.VivesranipalLip));
            AddMapping(nameof(ConfigJson.ScriptVrDebug), nameof(VrDebug));

            Action addConfigJsonListner = () =>
            {
                ConfigJsonListner = new PropertyChangedEventListener(ConfigJson)
                    {
                        { (_, e) => MapPropertyChanged(e.PropertyName) },
                    };
                CompositeDisposable.Add(ConfigJsonListner);
            };

            CompositeDisposable.Add(new PropertyChangedEventListener(ConfigJsonService)
            {
                {
                    nameof(IConfigJsonService.ConfigJson), (_, __) =>
                    {
                        RaiseAllPropertyChanged();
                        CompositeDisposable.Remove(ConfigJsonListner);
                        ConfigJsonListner.Dispose();
                        addConfigJsonListner();
                    }
                },
            });
            addConfigJsonListner();
        }

    }
}
